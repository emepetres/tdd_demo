using System;
using System.IO;
using Xunit;

namespace tdd_demo.Tests
{
    public class Program_MainShould : IDisposable
    {
        private const string input_path = "./Resources/sample.csv";
        private const string output_path = "./Resources/sample_filtered.csv";
        public Program_MainShould()
        {
            File.Delete(output_path);
        }

        public void Dispose()
        {
            File.Delete(output_path);
        }

        [Fact]
        public void Main_ReturnCSV()
        {
            var args = new string[] { "--csv", input_path };
            Program.Main(args);

            Assert.True(File.Exists(output_path));
        }
    }
}
