using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jal.Converter.Impl;

namespace Jal.RestClient.Tests
{
    public class StringToCustomerConverter : AbstractConverter<string, Customer>
    {
        public override Customer Convert(string source)
        {
            return new Customer();
        }
    }
}
