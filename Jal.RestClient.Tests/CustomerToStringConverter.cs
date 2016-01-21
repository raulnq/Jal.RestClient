using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jal.Converter.Impl;

namespace Jal.RestClient.Tests
{
    public class CustomerToStringConverter : AbstractConverter<Customer, string>
    {
        public override string Convert(Customer source)
        {
            return @"<CUSTOMER xmlns:xlink=""://www.w3.org/1999/xlink"">
  <ID>9996633</ID>
  <FIRSTNAME>COMPUWARE</FIRSTNAME>
  <LASTNAME>CHA_DEV</LASTNAME>
  <STREET>429 Seventh Av 4444.</STREET>
  <CITY>BEIJING-PANGU</CITY>
</CUSTOMER>";
        }
    }
}
