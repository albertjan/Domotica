﻿namespace Nancy.Testing
{
    /// <summary>
    /// Defines extensions for the <see cref="BrowserContext"/> type.
    /// </summary>
    public static class BrowserContextExtensions
    {
        /// <summary>
        /// Adds a multipart/form-data encoded request body to the <see cref="Browser"/>, using the default boundary name.
        /// </summary>
        /// <param name="browserContext">The <see cref="BrowserContext"/> that the data should be added to.</param>
        /// <param name="multipartFormData">The multipart/form-data encoded data that should be added.</param>
        public static void MultiPartFormData(this BrowserContext browserContext, BrowserContextMultipartFormData multipartFormData)
        {
            MultiPartFormData(browserContext, multipartFormData, BrowserContextMultipartFormData.DefaultBoundaryName);
        }

        /// <summary>
        /// Adds a multipart/form-data encoded request body to the <see cref="Browser"/>.
        /// </summary>
        /// <param name="browserContext">The <see cref="BrowserContext"/> that the data should be added to.</param>
        /// <param name="multipartFormData">The multipart/form-data encoded data that should be added.</param>
        /// <param name="boundaryName">The name of the boundary to be used</param>
        public static void MultiPartFormData(this BrowserContext browserContext, BrowserContextMultipartFormData multipartFormData, string boundaryName)
        {
            var contextValues =
                (IBrowserContextValues)browserContext;

            contextValues.Body = multipartFormData.Body;
            contextValues.Headers["Content-Type"] = new[] { "multipart/form-data; boundary=" + boundaryName };
        }
    }
}