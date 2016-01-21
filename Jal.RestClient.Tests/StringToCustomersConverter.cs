using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jal.Converter.Impl;

namespace Jal.RestClient.Tests
{
    public class StringToCustomersConverter : AbstractConverter<string, Customer[]>
    {
        public override Customer[] Convert(string source)
        {
            return new []{new Customer()};
        }
    }
}
