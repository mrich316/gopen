using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Xunit2;

namespace Gopen.UnitTests
{
    public class GopenConventions : AutoDataAttribute
    {
        public GopenConventions()
            : base(new Fixture()
                .Customize(new GopenCustomization())
                .Customize(new AutoMoqCustomization()))
        {
        }
    }
}
