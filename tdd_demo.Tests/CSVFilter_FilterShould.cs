using Xunit;
using tdd_demo;
using System.Collections.Generic;

namespace tdd_demo.UnitTests.CSVFilter
{
    public class CSVFilter_FilterShould
    {
        private static string[] BuildCSV(string line)
        {
            var csv = new List<string>();
            csv.Add("Num_factura, Fecha, Bruto, Neto, IVA, IGIC, Concepto, CIF_cliente, NIF_cliente");

            csv.Add(line);

            return csv.ToArray();
        }

        private static string[] BuildCSV(string[] lines)
        {
            var csv = new List<string>();
            csv.Add("Num_factura, Fecha, Bruto, Neto, IVA, IGIC, Concepto, CIF_cliente, NIF_cliente");
            
            foreach(var line in lines)
            {
                csv.Add(line);
            }
            
            return csv.ToArray();
        }

        [Fact]
        public void FilterAll_HeaderNotPresent_ReturnNull()
        {
            tdd_demo.CSVFilter.FilterAll(new string[] { "bad header" });
        }

        [Theory]
        [InlineData("1,02/05/2019,1000,810,19,,ACER Laptop,B76430134,")]
        [InlineData("2,03/08/2019,2000,2000,,8,MacBook Pro,,78544372A")]
        public void IsValid_OneLine_IsValid(string csv_line)
        {
            var is_valid = tdd_demo.CSVFilter.IsValid(csv_line);

            Assert.True(is_valid);
        }

        [Theory]
        [InlineData("1,02/05/2019,1000,810,19,8,ACER Laptop,B76430134,")]
        public void IsValid_IVA_IGIC_Exclusive(string csv_line)
        {
            var is_valid = tdd_demo.CSVFilter.IsValid(csv_line);

            Assert.False(is_valid);
        }

        [Theory]
        [InlineData("1,02/05/2019,1000,810,19,,ACER Laptop,B76430134,78544372A")]
        public void IsValid_CIF_NIF_Exclusive(string csv_line)
        {
            var is_valid = tdd_demo.CSVFilter.IsValid(csv_line);

            Assert.False(is_valid);
        }
    }
}
