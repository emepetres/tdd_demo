using Xunit;
using tdd_demo;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace tdd_demo.UnitTests
{
    public class Program_ParseInputsShould
    {
        [Theory]
        [InlineData(0, new string[] { } )]
        [InlineData(0, new string[] { "--csv" })]
        [InlineData(0, new string[] { "--other", "c:/test.csv" })]
        [InlineData(0, new string[] { "--csv", "c:/test.csv" })]
        [InlineData(0, new string[] { "--csv", "./Resources/bad_format.txt" })]
        public void ParseInputs_WrongInput_ReturnNull(int _, string[] args)
        {
            Assert.True(Program.ParseInputs(args) == null);
        }
    }
}