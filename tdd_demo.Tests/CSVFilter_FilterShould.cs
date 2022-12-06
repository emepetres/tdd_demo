using Xunit;
using tdd_demo;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace tdd_demo.UnitTests.CSVFilter
{
    internal class Builder
    {
        struct CSVTestLine
        {
            //"1,02/05/2019,1000,810,19,,ACER Laptop,B76430134,"
            public string num = "1";
            public string fecha = "02/05/2019";
            public string bruto = "1000";
            public string neto = "810";
            public string iva = "19";
            public string igic = string.Empty;
            public string concepto = "ACER Laptop";
            public string cif = "B76430134";
            public string nif = string.Empty;

            public CSVTestLine() { }
        }

        private CSVTestLine line;

        public Builder()
        {
            line = new CSVTestLine();
        }

        public Builder SetTaxes(string iva, string igic)
        {
            this.line.iva = iva;
            this.line.igic = igic;
            return this;
        }
        public Builder SetId(string cif, string nif)
        {
            this.line.cif = cif;
            this.line.nif = nif;
            return this;
        }

        public Builder SetMoney(string bruto, string neto)
        {
            this.line.bruto = bruto;
            this.line.neto = neto;
            return this;
        }

        public string Build()
        {
            return $"{this.line.num},{this.line.fecha},{this.line.bruto},{this.line.neto},{this.line.iva},{this.line.igic},{this.line.concepto},{this.line.cif},{this.line.nif}";
        }
    }

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

        [Fact]
        public void IsValid_OneLine_IsValid()
        {
            string csv_line = new Builder().Build();

            var is_valid = tdd_demo.CSVFilter.IsValid(csv_line);

            Assert.True(is_valid);
        }

        [Theory]
        [InlineData("19","8")]
        public void IsValid_IVA_IGIC_Exclusive(string iva, string igic)
        {
            string csv_line = new Builder()
                .SetTaxes(iva, igic)
                .Build();

            var is_valid = tdd_demo.CSVFilter.IsValid(csv_line);

            Assert.False(is_valid);
        }

        [Theory]
        [InlineData("B76430134","78544372A")]
        public void IsValid_CIF_NIF_Exclusive(string cif, string nif)
        {
            string csv_line = new Builder()
                .SetId(cif, nif)
                .Build();

            var is_valid = tdd_demo.CSVFilter.IsValid(csv_line);

            Assert.False(is_valid);
        }

        [Theory]
        [InlineData("1000","811","19","")]
        [InlineData("1000", "811", "", "19")]
        public void IsValid_Bruto_Neto(string bruto, string neto, string iva, string igic)
        {
            string csv_line = new Builder()
                .SetMoney(bruto,neto)
                .SetTaxes(iva,igic)
                .Build();

            var is_valid = tdd_demo.CSVFilter.IsValid(csv_line);

            Assert.False(is_valid);
        }
    }
}
