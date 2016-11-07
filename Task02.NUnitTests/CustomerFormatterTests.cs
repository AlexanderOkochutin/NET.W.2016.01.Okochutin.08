using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Task02.Logic;

namespace Task02.NUnitTests
{
    [TestFixture]
    public class CustomerFormatterTests
    {

        [TestCase("NPR",ExpectedResult = "Customer record: Jeffrey Richter, +1 (425) 555-0100, 10000000,0")]
        public string ToString_WithOthersFormatString(string format)
        {
            Customer customer = new Customer("Jeffrey Richter", "+1 (425) 555-0100", 1000000);
            return customer.ToString(format);
            
        }
    }
}
