using Xunit;
using tdd_demo;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace tdd_demo.UnitTests
{
    public class Program_MainShould
    {
        [Fact]
        public void Main_NoInputs_ExitNonZero()
        {
            Assert.True(Program.Main(new string[] { }) != 0);
        }

        [Fact]
        public void Main_NoCSVInput_ExitNonZero()
        {
            var args = new string[] { "--other", "c:/test.csv" };
            Assert.True(Program.Main(args) != 0);
        }

        [Fact]
        public void Main_CSVMissing_ExitNonZero()
        {
            var args = new string[] { "--csv" };
            Assert.True(Program.Main(args) != 0);
        }

        [Fact]
        public void Main_CSVNotExists_ExitNonZero()
        {
            var args = new string[] { "--csv", "c:/test.csv" };
            Assert.True(Program.Main(args) != 0);
        }

        [Fact]
        public void Main_InputIsNotCSV_ExitNonZero()
        {
            var args = new string[] { "--csv", "./Resources/bad_format.txt" };
            Assert.True(Program.Main(args) != 0);
        }
    }
}