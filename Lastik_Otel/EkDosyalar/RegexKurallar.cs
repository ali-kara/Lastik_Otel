using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lastik_Otel.EkDosyalar
{
    public static class RegexKurallar
    {
        public static bool SadeceSayimi(string text)
        {
            Regex regex = new Regex("[^0-9]+"); //regex that matches disallowed text
            return regex.IsMatch(text);
        }
    }
}
