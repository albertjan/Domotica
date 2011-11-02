namespace Nancy.Tests.Unit
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Nancy.IO;
    using Xunit;
    using Xunit.Extensions;

    public class RequestFixture
    {
        [Fact]
        public void Should_override_request_method_on_post()
        {
            // Given
            const string bodyContent = "_method=GET";
            var memory = CreateRequestStream();
            var writer = new StreamWriter(memory);
            writer.Write(bodyContent);
            writer.Flush();
            memory.Position = 0;

            var headers =
                new Dictionary<string, IEnumerable<string>> { { "content-type", new[] { "application/x-www-form-urlencoded" } } };

            // When
            var request = new Request("POST", "/", headers, memory, "http");

            // Then
            request.Method.ShouldEqual("GET");
        }

        [Theory]
        [InlineData("GET")]
        [InlineData("PUT")]
        [InlineData("DELETE")]
        [InlineData("HEAD")]
        public void Should_only_override_method_on_post(string method)
        {
            // Given
            const string bodyContent = "_method=TEST";
            var memory = CreateRequestStream();
            var writer = new StreamWriter(memory);
            writer.Write(bodyContent);
            writer.Flush();
            memory.Position = 0;

            var headers =
                new Dictionary<string, IEnumerable<string>> { { "content-type", new[] { "application/x-www-form-urlencoded" } } };

            // When
            var request = new Request(method, "/", headers, memory, "http");

            // Then
            request.Method.ShouldEqual(method);
        }

        [Fact]
        public void Should_throw_argumentoutofrangeexception_when_initialized_with_null_method()
        {
            // Given, When
            var exception =
                Record.Exception(() => new Request(null, "/", "http"));

            // Then
            exception.ShouldBeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void Should_throw_argumentoutofrangeexception_when_initialized_with_empty_method()
        {
            // Given, When
            var exception =
                Record.Exception(() => new Request(string.Empty, "/", "http"));

            // Then
            exception.ShouldBeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void Should_throw_argumentoutofrangeexception_when_initialized_with_null_uri()
        {
            // Given, When
            var exception =
                Record.Exception(() => new Request("GET", null, "http"));

            // Then
            exception.ShouldBeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void Should_throw_argumentoutofrangeexception_when_initialized_with_empty_uri()
        {
            // Given, When
            var exception =
                Record.Exception(() => new Request("GET", string.Empty, "http"));

            // Then
            exception.ShouldBeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void Should_set_method_parameter_value_to_method_property_when_initialized()
        {
            // Given
            const string method = "GET";

            // When
            var request = new Request(method, "/", "http");

            // Then
            request.Method.ShouldEqual(method);
        }

        [Fact]
        public void Should_set_uri_parameter_value_to_uri_property_when_initialized()
        {
            // Given
            const string path = "/";
            
            // When
            var request = new Request("GET", path, "http");

            // Then
            request.Path.ShouldEqual(path);
        }

        [Fact]
        public void Should_set_header_parameter_value_to_header_property_when_initialized()
        {
            // Given
            var headers = new Dictionary<string, IEnumerable<string>>()
                {
                    { "content-type", new[] {"foo"} }
                };

            // When
            var request = new Request("GET", "/", headers, CreateRequestStream(), "http");

            // Then
            request.Headers.ContentType.ShouldNotBeEmpty();
        }

        [Fact]
        public void Should_set_body_parameter_value_to_body_property_when_initialized()
        {
            // Given
            var body = CreateRequestStream();

            // When
            var request = new Request("GET", "/", new Dictionary<string, IEnumerable<string>>(), body, "http");

            // Then
            request.Body.ShouldBeSameAs(body);
        }

        [Fact]
        public void Should_set_extract_form_data_from_body_when_content_type_is_x_www_form_urlencoded()
        {
            // Given
            const string bodyContent = "name=John+Doe&gender=male&family=5&city=kent&city=miami&other=abc%0D%0Adef&nickname=J%26D";
            var memory = CreateRequestStream();
            var writer = new StreamWriter(memory);
            writer.Write(bodyContent);
            writer.Flush();
            memory.Position = 0;

            var headers = 
                new Dictionary<string, IEnumerable<string>>
                {
                    { "content-type", new[] { "application/x-www-form-urlencoded" } }
                };

            // When
            var request = new Request("POST", "/", headers, memory, "http");

            // Then
            ((string)request.Form.name).ShouldEqual("John Doe");
        }

        [Fact]
        public void Should_set_extract_form_data_from_body_when_content_type_is_x_www_form_urlencoded_with_character_set()
        {
            // Given
            const string bodyContent = "name=John+Doe&gender=male&family=5&city=kent&city=miami&other=abc%0D%0Adef&nickname=J%26D";
            var memory = CreateRequestStream();
            var writer = new StreamWriter(memory);
            writer.Write(bodyContent);
            writer.Flush();
            memory.Position = 0;

            var headers =
                new Dictionary<string, IEnumerable<string>>
                {
                    { "content-type", new[] { "application/x-www-form-urlencoded; charset=UTF-8" } }
                };

            // When
            var request = new Request("POST", "/", headers, memory, "http");

            // Then
            ((string)request.Form.name).ShouldEqual("John Doe");
        }

        [Fact]
        public void Should_set_extracted_form_data_from_body_when_content_type_is_multipart_form_data()
        {
            // Given
            var memory =
                new MemoryStream(BuildMultipartFormValues(new Dictionary<string, string>
                {
                    { "name", "John Doe"},
                    { "age", "42"}
                }));

            var headers =
                new Dictionary<string, IEnumerable<string>>
                {
                    { "content-type", new[] { "multipart/form-data; boundary=----NancyFormBoundary" } }
                };

            // When
            var request = new Request("POST", "/", headers, CreateRequestStream(memory), "http");

            // Then
            ((string)request.Form.name).ShouldEqual("John Doe");
            ((string)request.Form.age).ShouldEqual("42");
        }

        [Fact]
        public void Should_set_extracted_files_to_files_collection_when_body_content_type_is_multipart_form_data()
        {
            // Given
            var memory =
                new MemoryStream(BuildMultipartFileValues(new Dictionary<string, Tuple<string, string>>
                {
                    { "test", new Tuple<string, string>("content/type", "some test content")}
                }));

            var headers =
                new Dictionary<string, IEnumerable<string>>
                {
                    { "content-type", new[] { "multipart/form-data; boundary=----NancyFormBoundary" } }
                };

            // When
            var request = new Request("POST", "/", headers, CreateRequestStream(memory), "http");

            // Then
            request.Files.ShouldHaveCount(1);
        }

        [Fact]
        public void Should_set_content_type_on_file_extracted_from_multipart_form_data_body()
        {
            // Given
            var memory =
                new MemoryStream(BuildMultipartFileValues(new Dictionary<string, Tuple<string, string>>
                {
                    { "sample.txt", new Tuple<string, string>("content/type", "some test content")}
                }));

            var headers =
                new Dictionary<string, IEnumerable<string>>
                {
                    { "content-type", new[] { "multipart/form-data; boundary=----NancyFormBoundary" } }
                };

            // When
            var request = new Request("POST", "/", headers, CreateRequestStream(memory), "http");

            // Then
            request.Files.First().ContentType.ShouldEqual("content/type");
        }

        [Fact]
        public void Should_set_name_on_file_extracted_from_multipart_form_data_body()
        {
            // Given
            var memory =
                new MemoryStream(BuildMultipartFileValues(new Dictionary<string, Tuple<string, string>>
                {
                    { "sample.txt", new Tuple<string, string>("content/type", "some test content")}
                }));

            var headers =
                new Dictionary<string, IEnumerable<string>>
                {
                    { "content-type", new[] { "multipart/form-data; boundary=----NancyFormBoundary" } }
                };

            // When
            var request = new Request("POST", "/", headers, CreateRequestStream(memory), "http");

            // Then
            request.Files.First().Name.ShouldEqual("sample.txt");
        }

        [Fact]
        public void Should_value_on_file_extracted_from_multipart_form_data_body()
        {
            // Given
            var memory =
                new MemoryStream(BuildMultipartFileValues(new Dictionary<string, Tuple<string, string>>
                {
                    { "sample.txt", new Tuple<string, string>("content/type", "some test content")}
                }));

            var headers =
                new Dictionary<string, IEnumerable<string>>
                {
                    { "content-type", new[] { "multipart/form-data; boundary=----NancyFormBoundary" } }
                };

            // When
            var request = new Request("POST", "/", headers, CreateRequestStream(memory), "http");

            // Then
            GetStringValue(request.Files.First().Value).ShouldEqual("some test content");
        }

        private static string GetStringValue(Stream stream)
        {
            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

		[Fact]
		public void Should_be_able_to_invoke_form_repeatedly()
		{
			const string bodyContent = "name=John+Doe&gender=male&family=5&city=kent&city=miami&other=abc%0D%0Adef&nickname=J%26D";
			var memory = new MemoryStream();
			var writer = new StreamWriter(memory);
			writer.Write(bodyContent);
			writer.Flush();
			memory.Position = 0;

			var headers =
				new Dictionary<string, IEnumerable<string>>
                {
                    { "content-type", new[] { "application/x-www-form-urlencoded" } }
                };

			// When
            var request = new Request("POST", "/", headers, CreateRequestStream(memory), "http");
			request.Form.ToString();
			// Then
			((string)request.Form.name).ShouldEqual("John Doe");
		}

        [Fact]
        public void Should_throw_argumentnullexception_when_initialized_with_null_protocol()
        {
            // Given, When
            var exception =
                Record.Exception(() => new Request("GET", "/", null));

            // Then
            exception.ShouldBeOfType<ArgumentNullException>();
        }

        [Fact]
        public void Should_throw_argumentoutofrangeexception_when_initialized_with_an_empty_protocol()
        {
            // Given, When
            var exception =
                Record.Exception(() => new Request("GET", "/", string.Empty));

            // Then
            exception.ShouldBeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void Should_set_protocol_parameter_value_to_protocol_property_when_initialized()
        {
            // Given
            const string protocol = "http";

            // When
            var request = new Request("GET", "/", protocol);

            // Then
            request.Url.Scheme.ShouldEqual(protocol);
        }

		[Fact]
		public void Should_split_cookie_in_two_parts_only()
		{
			// Given, when
			var cookieName = "_nc";
			var cookieData = "Y+M3rcC/7ssXvHTx9pwCbwQVV4g=sp0hUZVApYgGbKZIU4bvXbBCVl9fhSEssEXSGdrt4jVag6PO1oed8lSd+EJD1nzWx4OTTCTZKjYRWeHE97QVND4jJIl+DuKRgJnSl3hWI5gdgGjcxqCSTvMOMGmW3NHLVyKpajGD8tq1DXhXMyXHjTzrCAYl8TGzwyJJGx/gd7VMJeRbAy9JdHOxEUlCKUnPneWN6q+/ITFryAa5hAdfcjXmh4Fgym75whKOMkWO+yM2icdsciX0ShcvnEQ/bXcTHTya6d7dJVfZl7qQ8AgIQv8ucQHxD3NxIvHNPBwms2ClaPds0HG5N+7pu7eMSFZjUHpDrrCnFvYN+JDiG3GMpf98LuCCvxemvipJo2MUkY4J1LvaDFoWA5tIxAfItZJkSIW2d8JPDwFk8OHJy8zhyn8AjD2JFqWaUZr4y9KZOtgI0V0Qlq0mS3mDSlLn29xapgoPHBvykwQjR6TwF2pBLpStsfZa/tXbEv2mc3VO3CnErIA1lEfKNqn9C/Dw6hqW";
			var headers = new Dictionary<string, IEnumerable<string>> ();
			var cookies = new List<string> ();
			cookies.Add (string.Format ("{0}={1}", cookieName, cookieData));
			headers.Add ("cookie", cookies);
			var newUrl = new Url
			{
				Path = "/"
			};
			var request = new Request ("GET", newUrl, null, headers);

			// Then
			request.Cookies[cookieName].ShouldEqual (cookieData);

		}

        private static RequestStream CreateRequestStream()
        {
            return CreateRequestStream(new MemoryStream());
        }

        private static RequestStream CreateRequestStream(Stream stream)
        {
            return RequestStream.FromStream(stream);
        }

        private static byte[] BuildMultipartFormValues(Dictionary<string, string> formValues)
        {
            var boundaryBuilder = new StringBuilder();

            foreach (var key in formValues.Keys)
            {
                boundaryBuilder.Append('\r');
                boundaryBuilder.Append('\n');
                boundaryBuilder.Append("--");
                boundaryBuilder.Append("----NancyFormBoundary");
                boundaryBuilder.Append('\r');
                boundaryBuilder.Append('\n');
                boundaryBuilder.AppendFormat("Content-Disposition: form-data; name=\"{0}\"", key);
                boundaryBuilder.Append('\r');
                boundaryBuilder.Append('\n');
                boundaryBuilder.Append('\r');
                boundaryBuilder.Append('\n');
                boundaryBuilder.Append(formValues[key]);
            }

            boundaryBuilder.Append('\r');
            boundaryBuilder.Append('\n');
            boundaryBuilder.Append("------NancyFormBoundary--");

            var bytes =
                Encoding.ASCII.GetBytes(boundaryBuilder.ToString());

            return bytes;
        }

        private static byte[] BuildMultipartFileValues(Dictionary<string, Tuple<string, string>> formValues)
        {
            var boundaryBuilder = new StringBuilder();

            foreach (var key in formValues.Keys)
            {
                boundaryBuilder.Append('\r');
                boundaryBuilder.Append('\n');
                boundaryBuilder.Append("--");
                boundaryBuilder.Append("----NancyFormBoundary");
                boundaryBuilder.Append('\r');
                boundaryBuilder.Append('\n');
                boundaryBuilder.AppendFormat("Content-Disposition: form-data; name=\"whatever\"; filename=\"{0}\"", key);
                boundaryBuilder.Append('\r');
                boundaryBuilder.Append('\n');
                boundaryBuilder.AppendFormat("Content-Type: {0}", formValues[key].Item1);
                boundaryBuilder.Append('\r');
                boundaryBuilder.Append('\n');
                boundaryBuilder.Append('\r');
                boundaryBuilder.Append('\n');
                boundaryBuilder.Append(formValues[key].Item2);
            }

            boundaryBuilder.Append('\r');
            boundaryBuilder.Append('\n');
            boundaryBuilder.Append("------NancyFormBoundary--");

            var bytes =
                Encoding.ASCII.GetBytes(boundaryBuilder.ToString());

            return bytes;
        }
    }
}
