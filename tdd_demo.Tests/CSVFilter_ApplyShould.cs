using Xunit;
using tdd_demo;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace tdd_demo.UnitTests.CSVFilter
{
    public class CSVFilter_ApplyShould
    {
        [Fact]
        public void discard_files_with_bad_format_header()
        {
            var csv_filtered = new tdd_demo.CSVFilter().Apply(new string[] { "bad header" });

            Assert.Null(csv_filtered);
        }

        [Fact]
        public void filter_invalid_lines_in_csv()
        {
            var csv_lines = new string[]
            {
                "Num_factura, Fecha, Bruto, Neto, IVA, IGIC, Concepto, CIF_cliente, NIF_cliente",
                "1,02/05/2019,1000,810,19,,ACER Laptop,B76430134,",
                "2,03/08/2019,2000,2000,,8,MacBook Pro,,78544372A",
                "3,03/12/2019,1000,2000,19,8,Lenovo Laptop,,78544372A"
            };
            var csv_filtered_expected = new string[]
            {
                "Num_factura, Fecha, Bruto, Neto, IVA, IGIC, Concepto, CIF_cliente, NIF_cliente",
                "1,02/05/2019,1000,810,19,,ACER Laptop,B76430134,"
            };

            var csv_filtered = new tdd_demo.CSVFilter().Apply(csv_lines);

            Assert.Equal(csv_filtered_expected, csv_filtered);
        }
    }
}
