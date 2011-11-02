﻿namespace Nancy.Hosting.Wcf.Tests
{
    using System.IO;

    public class TestModule : NancyModule
    {
        public TestModule()
        {
            Get["/rel"] = parameters => {
                return "This is the site route";
            };

            Get["/rel/header"] = parameters =>
                {
                    var response = new Response();
                    response.Headers["X-Some-Header"] = "Some value";

                    return response;
                };

            Post["/rel"] = parameters => {
                return new StreamReader(Request.Body).ReadToEnd();
            };
        }
    }
}