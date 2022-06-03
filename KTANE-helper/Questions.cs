using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KTANE_helper.IOHandler;

namespace KTANE_helper
{
    internal static class Questions
    {
        internal static bool SerialNumberLastIsEven()
        {
            int digit = IntQuery("What is the last digit of the serial number?");
            return digit % 2 == 0;
        }

        internal static bool SerialNumberLastIsOdd() => !SerialNumberLastIsEven();

        internal static int Batteries() => IntQuery("How many batteries are present on the bomb?");

        internal static bool LitIndicator(string indicator) => Ask($"Lit indicator \"{indicator}\"?");
    }
}
