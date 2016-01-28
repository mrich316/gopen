using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;

namespace Gopen
{
    /// <summary>
    /// Formatter binding a model from a multipart/form-data request. Each text part is mapped
    /// to its corresponding action parameter property using a JSON.NET to read each part.
    /// </summary>
    public class MultipartJsonMediaTypeFormatter : MediaTypeFormatter
    {
        private const string SupportedMediaType = "multipart/form-data";

        private readonly DefaultContractResolver _contractResolver;

        public MultipartJsonMediaTypeFormatter(DefaultContractResolver contractResolver)
        {
            if (contractResolver == null) throw new ArgumentNullException("contractResolver");
            _contractResolver = contractResolver;

            SupportedMediaTypes.Add(new MediaTypeHeaderValue(SupportedMediaType));

            SupportedEncodings.Add(new UTF8Encoding(false, true));
            SupportedEncodings.Add(new UnicodeEncoding(false, true, true));
        }

        public override bool CanReadType(Type type)
        {
            return true;
        }

        /// <summary>
        /// Writes are not implemented.
        /// </summary>
        /// <param name="type">The type to serialize.</param>
        /// <returns>
        /// returns false, writes are not implemented
        /// </returns>
        public override bool CanWriteType(Type type)
        {
            return false;
        }

        public override Task<object> ReadFromStreamAsync(Type type,
            Stream readStream,
            HttpContent content,
            IFormatterLogger formatterLogger)
        {
            // Handle empty requests by returning default values.
            if (content == null || !content.Headers.ContentLength.HasValue || content.Headers.ContentLength == 0)
            {
                return Task.FromResult(GetDefaultValueForType(type));
            }

            // Guard clause, should not be hit because SupportedMediaTypes
            // is restricted to "multipart/form-data".
            if (!content.IsMimeMultipartContent())
            {
                throw new InvalidOperationException(string.Format(
                    "{0} must only be called in a \"{1}\" request.",
                    GetType().Name,
                    SupportedMediaType));
            }

            throw new NotImplementedException();
        }
    }
}
