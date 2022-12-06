using Xunit;
using tdd_demo;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace tdd_demo.UnitTests.CSVFilter
{
    internal class LineBuilder
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

        public LineBuilder()
        {
            line = new CSVTestLine();
        }

        public LineBuilder SetTaxes(string iva, string igic)
        {
            this.line.iva = iva;
            this.line.igic = igic;
            return this;
        }
        public LineBuilder SetId(string cif, string nif)
        {
            this.line.cif = cif;
            this.line.nif = nif;
            return this;
        }

        public LineBuilder SetMoney(string bruto, string neto)
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

    public class CSVFilter_IsValidShould
    {
        private tdd_demo.CSVFilter filter;

        public CSVFilter_IsValidShould()
        {
            this.filter = new tdd_demo.CSVFilter();
        }

        [Fact]
        public void validate_correct_line()
        {
            string csv_line = new LineBuilder().Build();

            var is_valid = this.filter.IsValid(csv_line);

            Assert.True(is_valid);
        }

        [Theory]
        [InlineData("19","8")]
        public void invalidate_line_with_iva_and_igic(string iva, string igic)
        {
            string csv_line = new LineBuilder()
                .SetTaxes(iva, igic)
                .Build();

            var is_valid = this.filter.IsValid(csv_line);

            Assert.False(is_valid);
        }

        [Theory]
        [InlineData("B76430134","78544372A")]
        public void invalidate_line_with_cif_and_nif(string cif, string nif)
        {
            string csv_line = new LineBuilder()
                .SetId(cif, nif)
                .Build();

            var is_valid = this.filter.IsValid(csv_line);

            Assert.False(is_valid);
        }

        [Theory]
        [InlineData("1000","811","19","")]
        [InlineData("1000", "811", "", "19")]
        public void invalidate_line_with_wrong_neto(string bruto, string neto, string iva, string igic)
        {
            string csv_line = new LineBuilder()
                .SetMoney(bruto,neto)
                .SetTaxes(iva,igic)
                .Build();

            var is_valid = this.filter.IsValid(csv_line);

            Assert.False(is_valid);
        }

        [Fact]
        public void invalidate_duplicated_num_field()
        {
            string csv_line = new LineBuilder().Build();

            this.filter.IsValid(csv_line);
            var is_valid = this.filter.IsValid(csv_line);

            Assert.False(is_valid);
        }
    }
}
