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
            return String.Format("Name: {0}\t ContactPhone: {1}\t Revenue: {2}\t",
                customer.Name, customer.Phone, customer.Revenue);
        }

        private string FormatCustomerWithNewLine(Customer customer)
        {
            return String.Format("Name: {0}\n ContactPhone: {1}\n Revenue: {2}\n",
                customer.Name, customer.Phone, customer.Revenue);
        }

        private string FormatCustomerWithSingleQuote(Customer customer)
        {
            return String.Format("Name: '{0}' ContactPhone: '{1}' Revenue: '{2}'",
                customer.Name, customer.Phone, customer.Revenue);
        }

        private string FormatCustomerWithDoubleQuote(Customer customer)
        {
            return String.Format("Name: \"{0}\" ContactPhone: \"{1}\" Revenue: \"{2}\"",
                customer.Name, customer.Phone, customer.Revenue);
        }
        #endregion
    }

}
