using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Xunit;

namespace Gopen.UnitTests
{
    public class MultipartJsonMediaTypeFormatterTests
    {
        [Theory, GopenConventions]
        public void SupportedMediaTypes_Contains_MultipartFormdata(MultipartJsonMediaTypeFormatter sut)
        {
            var actual = sut.SupportedMediaTypes
                .SingleOrDefault(mediaType => mediaType.MediaType == "multipart/form-data");

            Assert.NotNull(actual);
        }

        [Theory, GopenConventions]
        public void SupportedMediaTypes_Contains_OneItem(MultipartJsonMediaTypeFormatter sut)
        {
            Assert.Equal(1, sut.SupportedMediaTypes.Count);
        }

        [Theory, GopenConventions]
        public void SupportedEncodings_Contains_Items(MultipartJsonMediaTypeFormatter sut)
        {
            Assert.NotEmpty(sut.SupportedEncodings);
        }

        [Theory, GopenConventions]
        public void SupportedEncodings_Contains_Utf8(MultipartJsonMediaTypeFormatter sut)
        {
            var actual = sut.SupportedEncodings
                .SingleOrDefault(encoding => encoding.WebName == "utf-8");

            Assert.NotNull(actual);
        }

        [Theory, GopenConventions]
        public void SupportedEncodings_Contains_Utf16(MultipartJsonMediaTypeFormatter sut)
        {
            var actual = sut.SupportedEncodings
                .SingleOrDefault(encoding => encoding.WebName == "utf-16");

            Assert.NotNull(actual);
        }

        [Theory, GopenConventions]
        public void CanReadType_JsonPrimitiveContract_ReturnsFalse(MultipartJsonMediaTypeFormatter sut)
        {
            var types = new[]
            {
                typeof(bool), typeof(string), typeof(int), typeof(long), typeof(int?), typeof(DateTime)
            };

            foreach (var t in types)
            {
                Assert.False(sut.CanReadType(t), string.Format("CanReadType must not support {0}", t.FullName));
            }
        }

        [Theory, GopenConventions]
        public void CanReadType_JsonObjectContract_ReturnsTrue(MultipartJsonMediaTypeFormatter sut)
        {
            var types = new[]
            {
                typeof(TestModel)
            };

            foreach (var t in types)
            {
                Assert.True(sut.CanReadType(t), string.Format("CanReadType must support {0}", t.FullName));
            }
        }

        [Theory, GopenConventions]
        public void CanReadType_JsonDictionaryContract_ReturnsTrue(MultipartJsonMediaTypeFormatter sut)
        {
            var types = new[]
            {
                typeof(Dictionary<string,TestModel>), typeof(Dictionary<int,TestModel>)
            };

            foreach (var t in types)
            {
                Assert.True(sut.CanReadType(t), string.Format("CanReadType must support {0}", t.FullName));
            }
        }

        [Theory, GopenConventions]
        public void CanWriteType_Any_ReturnsFalse(MultipartJsonMediaTypeFormatter sut)
        {
            var types = new[]
            {
                typeof(bool), typeof(int), typeof(Nullable<>), typeof(Dictionary<,>), typeof(List<>), typeof(object)
            };

            foreach (var t in types)
            {
                Assert.False(sut.CanWriteType(t), string.Format("CanWriteType supports {0}, but should not have.", t.FullName));
            }
        }

        [Theory, GopenConventions]
        public async Task ReadFromStreamAsync_ContentNotMultipartFormData_ThrowsInvalidOperationException(
            MultipartJsonMediaTypeFormatter sut,
            ObjectContent<string> content,
            IFormatterLogger formatterLogger)
        {
            Assert.False(content.IsMimeMultipartContent());

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                sut.ReadFromStreamAsync(content.ObjectType, null, content, formatterLogger));
        }

        [Theory, GopenConventions]
        public async Task ReadFromStreamAsync_NullContent_ReturnsDefault(
            MultipartJsonMediaTypeFormatter sut,
            Type type,
            IFormatterLogger formatterLogger)
        {
            var actual = await sut.ReadFromStreamAsync(type, null, null, formatterLogger);
            var expected = MediaTypeFormatter.GetDefaultValueForType(type);

            Assert.Equal(expected, actual);
        }

        [Theory, GopenConventions]
        public async Task ReadFromStreamAsync_NotJsonObjectContent_Throws(
            MultipartJsonMediaTypeFormatter sut,
            MultipartFormDataContent content,
            ObjectContent<string> stringContent,
            IFormatterLogger formatterLogger)
        {
            content.Add(stringContent, "StringValue");
            // HACK: ContentLength set just to bypass sanity checks in ReadFromStreamAsync.
            content.Headers.ContentLength = 1;

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                sut.ReadFromStreamAsync(typeof(string), null, content, formatterLogger));
        }
    }
}
