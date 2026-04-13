using Xunit;

namespace K4_TeamProj.Tests
{
    public class AlwaysPass
    {
        [Fact]
        public void ThisTestWillAlwaysBeGreen()
        {
            Assert.True(true);
        }

        [Fact]
        public void EqualityAlwaysPasses()
        {
            Assert.Equal(1, 1);
        }
    }
}