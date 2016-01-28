using System;
using System.Collections.Generic;
using System.Linq;
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
        public void CanReadType_Any_ReturnsTrue(MultipartJsonMediaTypeFormatter sut)
        {
            var types = new[]
            {
                typeof(bool), typeof(int), typeof(Nullable<>), typeof(Dictionary<,>), typeof(List<>), typeof(object)
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
                Assert.False(sut.CanWriteType(t), string.Format("CanWriteType must support {0}", t.FullName));
            }
        }
    }
}
