using System;
using System.IO;
using Xunit;

namespace tdd_demo.UnitTests
{
    public class Program_MainShould : IDisposable
    {
        private const string input_path = "./Resources/sample.csv";
        private const string output_expected_path = "./Resources/sample_filtered_expected.csv";
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
        public async void create_an_output_csv_file()
        {
            var args = new string[] { "--csv", input_path };
            await Program.Main(args);

            Assert.True(File.Exists(output_path));
        }

        [Fact]
        public async void filter_a_csv_file()
        {
            var args = new string[] { "--csv", input_path };
            await Program.Main(args);

            var filtered_output = File.ReadAllLines(output_path);
            var filtered_output_expected = File.ReadAllLines(output_expected_path);

            Assert.Equal(filtered_output_expected, filtered_output);
        }
    }
}
