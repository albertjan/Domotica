﻿namespace Nancy.Authentication.Basic.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using FakeItEasy;
    using Nancy.Security;
    using Nancy.Tests;
    using Xunit;
    using Nancy.Bootstrapper;
    using Nancy.Tests.Fakes;

    public class BasicAuthenticationFixture
    {
        private readonly BasicAuthenticationConfiguration config;
        private readonly IApplicationPipelines hooks;

        public BasicAuthenticationFixture()
        {
            this.config = new BasicAuthenticationConfiguration(A.Fake<IUserValidator>(), "realm");
            this.hooks = new FakeApplicationPipelines();
            BasicAuthentication.Enable(this.hooks, this.config);
        }

        [Fact]
        public void Should_add_a_pre_and_post_hook_in_application_when_enabled()
        {
            // Given
            var pipelines = A.Fake<IApplicationPipelines>();

            // When
            BasicAuthentication.Enable(pipelines, this.config);

            // Then
            A.CallTo(() => pipelines.BeforeRequest.AddItemToStartOfPipeline(A<Func<NancyContext, Response>>.Ignored))
                .MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void Should_add_both_basic_and_requires_auth_pre_and_post_hooks_in_module_when_enabled()
        {
            // Given
            var module = new FakeModule();

            // When
            BasicAuthentication.Enable(module, this.config);
            
            // Then
            module.Before.PipelineDelegates.ShouldHaveCount(2);
        }

        [Fact]
        public void Should_throw_with_null_config_passed_to_enable_with_application()
        {
            // Given, When
            var result = Record.Exception(() => BasicAuthentication.Enable(A.Fake<IApplicationPipelines>(), null));

            // Then
            result.ShouldBeOfType(typeof(ArgumentNullException));
        }

        [Fact]
        public void Should_throw_with_null_config_passed_to_enable_with_module()
        {
            // Given, When
            var result = Record.Exception(() => BasicAuthentication.Enable(new FakeModule(), null));

            // Then
            result.ShouldBeOfType(typeof(ArgumentNullException));
        }

        [Fact]
        public void Pre_request_hook_should_not_set_auth_details_with_no_auth_headers()
        {
            // Given
            var context = new NancyContext()
            {
                Request = new FakeRequest("GET", "/")
            };

            // When
            var result = this.hooks.BeforeRequest.Invoke(context);

            // Then
            result.ShouldBeNull();
            context.CurrentUser.ShouldBeNull();
        }

        [Fact]
        public void Post_request_hook_should_return_challenge_when_unauthorized_returned_from_route()
        {
            // Given
            var context = new NancyContext()
            {
                Request = new FakeRequest("GET", "/")
            };

            string wwwAuthenticate;
            context.Response = new Response { StatusCode = HttpStatusCode.Unauthorized };

            // When
            this.hooks.AfterRequest.Invoke(context);

            // Then
            context.Response.Headers.TryGetValue("WWW-Authenticate", out wwwAuthenticate);
            context.Response.StatusCode.ShouldEqual(HttpStatusCode.Unauthorized);
            context.Response.Headers.ContainsKey("WWW-Authenticate").ShouldBeTrue();
            context.Response.Headers["WWW-Authenticate"].ShouldContain("Basic");
            context.Response.Headers["WWW-Authenticate"].ShouldContain("realm=\"" + this.config.Realm + "\"");
        }

        [Fact]
        public void Pre_request_hook_should_not_set_auth_details_when_invalid_scheme_in_auth_header()
        {
            // Given
            var context = CreateContextWithHeader(
                "Authorization", new[] { "FooScheme" + " " + EncodeCredentials("foo", "bar") });

            // When
            var result = this.hooks.BeforeRequest.Invoke(context);

            // Then
            result.ShouldBeNull();
            context.CurrentUser.ShouldBeNull();
        }

        [Fact]
        public void Pre_request_hook_should_not_authenticate_when_invalid_encoded_username_in_auth_header()
        {
            // Given
            var context = CreateContextWithHeader(
               "Authorization", new[] { "Basic" + " " + "some credentials" });

            // When
            var result = this.hooks.BeforeRequest.Invoke(context);

            // Then
            result.ShouldBeNull();
            context.CurrentUser.ShouldBeNull();
        }

        [Fact]
        public void Pre_request_hook_should_call_user_validator_with_username_in_auth_header()
        {
            // Given
            var context = CreateContextWithHeader(
               "Authorization", new[] { "Basic" + " " + EncodeCredentials("foo", "bar") });

            // When
            this.hooks.BeforeRequest.Invoke(context);

            // Then
            A.CallTo(() => config.UserValidator.Validate("foo", "bar")).MustHaveHappened();
        }

        [Fact]
        public void Should_set_user_in_context_with_valid_username_in_auth_header()
        {
            // Given
            var fakePipelines = new FakeApplicationPipelines();

            var validator = A.Fake<IUserValidator>();
            var fakeUser = A.Fake<IUserIdentity>();
            A.CallTo(() => validator.Validate("foo", "bar")).Returns(fakeUser);

            var cfg = new BasicAuthenticationConfiguration(validator, "realm");

            var context = CreateContextWithHeader(
               "Authorization", new [] { "Basic" + " " + EncodeCredentials("foo", "bar") });

            BasicAuthentication.Enable(fakePipelines, cfg);

            // When
            fakePipelines.BeforeRequest.Invoke(context);

            // Then
            context.CurrentUser.ShouldBeSameAs(fakeUser);
        }

        private static NancyContext CreateContextWithHeader(string name, IEnumerable<string> values)
        {
            var header = new Dictionary<string, IEnumerable<string>>
            {
                { name, values }
            };

            return new NancyContext()
            {
                Request = new FakeRequest("GET", "/", header)
            };
        }

        private static string EncodeCredentials(string username, string password)
        {
            var credentials = string.Format("{0}:{1}", username, password);

            var encodedCredentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));

            return encodedCredentials;
        }

        class FakeModule : NancyModule
        {
        }

        public class FakeApplicationPipelines : IApplicationPipelines
        {
            public BeforePipeline BeforeRequest { get; set; }

            public AfterPipeline AfterRequest { get; set; }

            public ErrorPipeline OnError { get; set; }

            public FakeApplicationPipelines()
            {
                this.BeforeRequest = new BeforePipeline();
                this.AfterRequest = new AfterPipeline();
            }
        }
    }
}
