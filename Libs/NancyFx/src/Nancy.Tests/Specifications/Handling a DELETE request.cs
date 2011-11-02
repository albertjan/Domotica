namespace Nancy.Tests.Specifications
{
    using Machine.Specifications;
    using Nancy.Tests.Extensions;

    [Subject("Handling a DELETE request")]
    public class when_delete_request_matched_existing_route : RequestSpec
    {
        Establish context = () =>
            request = ManufactureDELETERequestForRoute("/");

        Because of = () =>
            response = engine.HandleRequest(request).Response;

        It should_set_status_code_to_ok = () =>
            response.StatusCode.ShouldEqual(HttpStatusCode.OK);

        It should_set_content_type_to_text_html = () =>
            response.ContentType.ShouldEqual("text/html");

        It should_set_content = () =>
            response.GetStringContentsFromResponse().ShouldEqual("Default delete root");
    }

    [Subject("Handling a DELETE request")]
    public class when_delete_request_does_not_matched_existing_route : RequestSpec
    {
        Establish context = () =>
            request = ManufactureDELETERequestForRoute("/invalid");

        Because of = () =>
            response = engine.HandleRequest(request).Response;

        It should_set_status_code_to_not_found = () =>
            response.StatusCode.ShouldEqual(HttpStatusCode.NotFound);
    }
}