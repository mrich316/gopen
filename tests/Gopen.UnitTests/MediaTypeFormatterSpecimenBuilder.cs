using System;
using System.Net.Http.Formatting;
using System.Reflection;
using Ploeh.AutoFixture.Kernel;

namespace Gopen.UnitTests
{
    // Implemetation credits goes to Mark Seemann
    // http://stackoverflow.com/a/16954699
    public class MediaTypeFormatterSpecimenBuilder : ISpecimenBuilder
    {
        private readonly MediaTypeFormatter _mediaTypeFormatter;

        public MediaTypeFormatterSpecimenBuilder(MediaTypeFormatter mediaTypeFormatter)
        {
            if (mediaTypeFormatter == null) throw new ArgumentNullException("mediaTypeFormatter");

            _mediaTypeFormatter = mediaTypeFormatter;
        }

        public object Create(object request, ISpecimenContext context)
        {
            var pi = request as ParameterInfo;
            if (pi == null
                || pi.ParameterType != typeof(MediaTypeFormatter)
                || pi.Name != "formatter")
            {
                return new NoSpecimen();
            }

            return _mediaTypeFormatter;
        }
    }
}
