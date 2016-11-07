using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Globalization;
using NUnit.Framework;
using Task02.Logic;

namespace Task02.NUnitTests
{
    [TestFixture]
    public class CustomerFormatterTests
    {

        public class MyDataClass
        {

            public static IEnumerable FormatCases
            {
                get
                {
                    yield return
                        new TestCaseData("NPR").Returns(
                            "Customer record: Jeffrey Richter, +1 (425) 555-0100, 1000000,0");
                    yield return
                        new TestCaseData("NP").Returns(
                            "Customer record: Jeffrey Richter, +1 (425) 555-0100");
                    yield return
                       new TestCaseData("NR").Returns(
                           "Customer record: Jeffrey Richter, 1000000,0");
                    yield return
                       new TestCaseData("N").Returns(
                           "Customer record: Jeffrey Richter");

                }
            }

            public static IEnumerable ExtensionFormatter
            {
                get
                {
                    yield return
                        new TestCaseData("WT").Returns(
                           "Name: Jeffrey Richter\t ContactPhone: +1 (425) 555-0100\t Revenue: 1000000\t");
                    yield return
                       new TestCaseData("WNL").Returns(
                           "Name: Jeffrey Richter\n ContactPhone: +1 (425) 555-0100\n Revenue: 1000000\n");
                }
            }
        }

        [Test, TestCaseSource(typeof(MyDataClass), nameof(MyDataClass.FormatCases))]
            public string ToString_Format(string format)
        {
            Customer customer = new Customer("Jeffrey Richter", "+1 (425) 555-0100", 1000000);
            string test =  customer.ToString(format);
            return test;
        }

        [Test, TestCaseSource(typeof(MyDataClass), nameof(MyDataClass.ExtensionFormatter))]
        public string Extension_Format(string format)
        {
            Customer customer = new Customer("Jeffrey Richter", "+1 (425) 555-0100", 1000000);
            CustomerFormatter cf = new CustomerFormatter();
            return cf.Format(format, customer, CultureInfo.CurrentCulture);
        }
    }
}
