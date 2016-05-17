using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bit32
{
    public class Class1
    {
        private UInt32 current;
        public void MyFunction(int number)
        {

            //this.current = number;
            try
            {
                Console.WriteLine(string.Format
                (CultureInfo.InvariantCulture, "My name {0}", number.ToString(CultureInfo.InvariantCulture)));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public UInt32 Return()
        {
            return current;
        }
       

    }
}
