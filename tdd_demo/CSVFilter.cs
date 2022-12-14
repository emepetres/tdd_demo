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
    
    public class CSVFilter
    {
        private static readonly Regex sWhitespace = new Regex(@"\s+");
        private static string HEADER = "Num_factura, Fecha, Bruto, Neto, IVA, IGIC, Concepto, CIF_cliente, NIF_cliente";
        
        private HashSet<int> line_nums;

        public CSVFilter()
        {
            line_nums = new HashSet<int>();
        }

        public string[]? Apply(string[] input)
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

        public bool IsValid(string line)
        {
            var data = new CSVLine(line);

            if ((data.iva == String.Empty) == (data.igic == String.Empty)
                || (data.cif == String.Empty) == (data.nif == String.Empty))
            {
                return false;
            }

            int imp, bruto, neto, num;
            if (!int.TryParse(data.iva, out imp) && !int.TryParse(data.igic, out imp))
            {
                return false;
            }

            if (!int.TryParse(data.bruto, out bruto) || !int.TryParse(data.neto, out neto))
            {
                return false;
            }

            if ((bruto * (100 - imp) / 100) != neto)
            {
                return false;
            }

            if (!int.TryParse(data.num, out num) || this.line_nums.Contains(num))
            {
                return false;
            }
            this.line_nums.Add(num);

            return true;
        }

        private static string CleanHeader(string header)
        {
            return sWhitespace.Replace(header.ToLower(), String.Empty);
        }
    }
}
