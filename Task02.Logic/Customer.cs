using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task02.Logic
{
    public sealed class Customer : IFormattable
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public decimal Revenue { get; set; }

        public Customer(string name, string phone, decimal revenue)
        {
            Name = name;
            Phone = phone;
            Revenue = revenue;
        }

        public override string ToString() => ToString("NPR", CultureInfo.CurrentCulture);

        public string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format))
            {
                format = "NPR";
            }
            if (formatProvider == null)
            {
                formatProvider = CultureInfo.CurrentCulture;
            }
            switch (format.ToUpperInvariant())
            {
                case "G":
                case "NPR":
                    return $"Customer record: {Name}, {Phone}, {Revenue.ToString("F1", formatProvider)}";

                case "NP":
                    return $"Customer record: {Name}, {Phone}";

                case "NR":
                    return $"Customer record: {Name}, {Revenue.ToString("F1", formatProvider)}";

                case "PR":
                    return $"Customer record: {Phone}, {Revenue.ToString("F1", formatProvider)}";

                case "P":
                    return $"Customer record: {Phone}";

                case "N":
                    return $"Customer record: {Name}";

                case "R":
                    return $"Customer record: {Revenue.ToString("F1", formatProvider)}";

                default:
                    throw new FormatException("such format string not supported");
            }
        }
    }
}
