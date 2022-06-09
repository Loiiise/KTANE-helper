using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KTANE_helper.IOHandler;

namespace KTANE_helper
{
    internal class Bomb
    {
        private int? batteries;

        internal int Batteries() => batteries.HasValue 
            ? batteries.Value 
            : (batteries = IntQuery("How many batteries are present on the bomb?")).Value;


        private bool? serialNumberLastDigitEven;

        internal bool SerialNumberLastDigitEven() => serialNumberLastDigitEven.HasValue
            ? serialNumberLastDigitEven.Value 
            : (serialNumberLastDigitEven = IntQuery("What is the last digit of the serial number?") % 2 == 0).Value;
        internal bool SerialNumberLastDigitOdd() => !this.SerialNumberLastDigitEven();

        private List<string> litIndicators = new();
        internal bool LitIndicator(string indicator)
        {
            indicator = indicator.ToUpper();    
            if (litIndicators.Contains(indicator)) return true;
         
            bool present = Ask($"Lit indicator \"{indicator}\"?");
            if (present) litIndicators.Add(indicator);

            return present;
        }
    }
}
