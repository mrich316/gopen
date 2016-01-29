using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using Newtonsoft.Json.Serialization;
using Ploeh.AutoFixture;

namespace Gopen.UnitTests
{
    public class GopenCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<HttpConfiguration>(c => c
                .OmitAutoProperties());
            
            fixture.Customize<HttpRequestMessage>(c => c
                .Do(x => x.Properties.Add(
                    HttpPropertyKeys.HttpConfigurationKey,
                    fixture.Create<HttpConfiguration>())));
            
            fixture.Customize<HttpRequestContext>(c => c
                .Without(x => x.ClientCertificate));

            fixture.Customize<IContractResolver>(c => c
                .FromSeed(x => new DefaultContractResolver()));

            // TODO: Investigate why ContentLength is not set automatically.
            fixture.Customize<ObjectContent<string>>(c => c
                .Do(x => x.Headers.ContentLength = ((string)x.Value).Length));

            fixture.Customizations.Add(
                new MediaTypeFormatterSpecimenBuilder(new JsonMediaTypeFormatter()));
        }
    }
}
