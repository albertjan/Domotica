﻿namespace Nancy.Tests.Unit.Extensions
{
    using System.Collections.Generic;

    using Nancy.Extensions;
    using Nancy.Tests.Fakes;

    using Xunit;

    public class ContextExtensionsFixture
    {
        [Fact]
        public static void IsAjaxRequest_should_return_true_if_request_is_ajax()
        {
            // Given 
            var headers =
                new Dictionary<string, IEnumerable<string>>
                    {
                        { "X-Requested-With", new[] { "XMLHttpRequest" } }
                    };

            // When
            var context = new NancyContext
                              {
                                  Request = new FakeRequest("POST", "/", headers)
                              };

            // Then
            Assert.True(context.IsAjaxRequest());
        }

        [Fact]
        public void IsAjaxRequest_should_return_false_if_request_is_null()
        {
            // Given when
            var context = new NancyContext();

            // Then
            Assert.False(context.IsAjaxRequest());
        }

        [Fact]
        public void IsAjaxRequest_should_return_false_if_request_is_not_ajax()
        {
            // Given when
            var context = new NancyContext
                              {
                                  Request = new FakeRequest("POST", "/")
                              };

            // Then
            Assert.False(context.IsAjaxRequest());
        }

        [Fact]
        public void Should_return_same_path_when_parsing_path_if_path_doesnt_contain_tilde()
        {
            const string input = "/scripts/test.js";
            var url = new Url
            {
                BasePath = "/base/path",
                Path = "/"
            };
            var request = new Request("GET", url);
            var nancyContext = new NancyContext { Request = request };

            var result = nancyContext.ToFullPath(input);

            result.ShouldEqual(input);
        }

        [Fact]
        public void Should_replace_tilde_with_base_path_when_parsing_path_if_one_present()
        {
            const string input = "~/scripts/test.js";
            var url = new Url
            {
                BasePath = "/base/path/",
                Path = "/"
            };
            var request = new Request("GET", url);
            var nancyContext = new NancyContext { Request = request };

            var result = nancyContext.ToFullPath(input);

            result.ShouldEqual("/base/path/scripts/test.js");
        }

        [Fact]
        public void Should_replace_tilde_with_nothing_when_parsing_path_if_one_present_and_base_path_is_null()
        {
            const string input = "~/scripts/test.js";
            var url = new Url
            {
                BasePath = null,
                Path = "/"
            };
            var request = new Request("GET", url);
            var nancyContext = new NancyContext { Request = request };

            var result = nancyContext.ToFullPath(input);

            result.ShouldEqual("/scripts/test.js");
        }

    }
}
