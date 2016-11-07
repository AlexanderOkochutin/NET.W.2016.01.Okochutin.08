using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task02.Logic
{
    public class CustomerFormatter : IFormatProvider, ICustomFormatter
    {
        private delegate string CustomerFormat(Customer customer);
        private readonly IFormatProvider parentProvider;

        #region Constructors

        public CustomerFormatter() : this(CultureInfo.CurrentCulture)
        {
        }

        public CustomerFormatter(IFormatProvider parent)
        {
            parentProvider = parent;
        }

        #endregion

        #region Public Methods

        public object GetFormat(Type formatType)
            => formatType == typeof (ICustomFormatter) ? this : parentProvider.GetFormat(formatType);

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            Customer customer = arg as Customer;
            if (customer == null)
            {
                throw new ArgumentException("Wrong argument type", nameof(arg));
            }
            if (string.IsNullOrEmpty(format))
            {
                format = "g";
            }

            Dictionary<string, CustomerFormat> outputFormts = new Dictionary<string, CustomerFormat>()
            {
                {"WT", new CustomerFormat(FormatCustomerWithTab)},
                {"WNL", new CustomerFormat(FormatCustomerWithNewLine)},
                {"WSQ", new CustomerFormat(FormatCustomerWithSingleQuote)},
                {"WDQ", new CustomerFormat(FormatCustomerWithDoubleQuote)}
            };

            format = format.ToUpper();
            if (outputFormts.ContainsKey(format))
            {
                return outputFormts[format](customer);
            }
            else
            {
                return customer.ToString(format, formatProvider);
            }

        }

        #endregion

        #region Private methods
        private string FormatCustomerWithTab(Customer customer)
        {
            return $"Name: {customer.Name}\t ContactPhone: {customer.Phone}\t Revenue: {customer.Revenue}\t";
        }

        private string FormatCustomerWithNewLine(Customer customer)
        {
            return $"Name: {customer.Name}\n ContactPhone: {customer.Phone}\n Revenue: {customer.Revenue}\n";
        }

        private string FormatCustomerWithSingleQuote(Customer customer)
        {
            return $"Name: '{customer.Name}' ContactPhone: '{customer.Phone}' Revenue: '{customer.Revenue}'";
        }

        private string FormatCustomerWithDoubleQuote(Customer customer)
        {
            return $"Name: \"{customer.Name}\" ContactPhone: \"{customer.Phone}\" Revenue: \"{customer.Revenue}\"";
        }
        #endregion
    }

}
