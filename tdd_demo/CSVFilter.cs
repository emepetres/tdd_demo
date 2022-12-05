using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace tdd_demo
{
    public static class CSVFilter
    {
        private static readonly Regex sWhitespace = new Regex(@"\s+");
        private static string HEADER = "Num_factura, Fecha, Bruto, Neto, IVA, IGIC, Concepto, CIF_cliente, NIF_cliente";
        
        public static string[]? Filter(string[] input)
        {
            var inputs = new Queue<string>(input);

            if (inputs.Count == 0)
            {
                return null;
            }

            // check header
            string input_line;
            if (!inputs.TryDequeue(out input_line!)
                || CleanHeader(HEADER) != CleanHeader(input_line))
            {
                return null;
            }

            var filtered = new List<string>();
            filtered.Add(HEADER);
            // filter each line


            return filtered.ToArray();
        }

        private static string CleanHeader(string header)
        {
            string _header = String.Empty;
            sWhitespace.Replace(header.ToLower(), _header);
            return _header;
        }
    }
}
