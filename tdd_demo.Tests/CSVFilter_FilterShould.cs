using Xunit;
using tdd_demo;

namespace tdd_demo.UnitTests.CSVFilter
{
    public class CSVFilter_FilterShould
    {
        [Fact]
        public void Filter_HeaderNotPresent_ReturnNull()
        {
            tdd_demo.CSVFilter.Filter(new string[] { "bad header" });
        }
    }
}
