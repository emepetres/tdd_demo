using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace tdd_demo
{
    struct CSVLine
    {
        public string num;
        public string fecha;
        public string bruto;
        public string neto;
        public string iva;
        public string igic;
        public string concepto;
        public string cif;
        public string nif;

        public CSVLine(string line)
        {
            var data = line.Split(",");
            num = data[0];
            fecha = data[1];
            bruto = data[2];
            neto = data[3];
            iva = data[4];
            igic = data[5];
            concepto = data[6];
            cif = data[7];
            nif = data[8];
        }
    }
    
    public static class CSVFilter
    {
        private static readonly Regex sWhitespace = new Regex(@"\s+");
        private static string HEADER = "Num_factura, Fecha, Bruto, Neto, IVA, IGIC, Concepto, CIF_cliente, NIF_cliente";
        
        public static string[]? FilterAll(string[] input)
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
            while(inputs.TryDequeue(out input_line!))
            {
                if (IsValid(input_line))
                {
                    filtered.Add(input_line);
                }
            }

            return filtered.ToArray();
        }

        public static bool IsValid(string line)
        {
            var data = new CSVLine(line);

            return
                (data.iva == String.Empty || data.igic == String.Empty)
                && (data.cif == String.Empty || data.nif == String.Empty);
        }

        private static string CleanHeader(string header)
        {
            string _header = String.Empty;
            sWhitespace.Replace(header.ToLower(), _header);
            return _header;
        }
    }
}
