using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;

namespace Gopen
{
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
        /// Queries whether this <see cref="T:System.Net.Http.Formatting.MediaTypeFormatter" /> can serializean object of the
        /// specified type.
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
            IFormatterLogger formatterLogger,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
