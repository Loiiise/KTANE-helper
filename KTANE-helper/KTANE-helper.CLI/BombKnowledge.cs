using System.Collections.Generic;
using static KTANE_helper.IOHandler;

namespace KTANE_helper
{
    internal class BombKnowledge
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

        private bool? serialNumberContainsVowel;
        internal bool SerialNumberContainsVowel() => serialNumberContainsVowel.HasValue
            ? serialNumberContainsVowel.Value
            : (serialNumberContainsVowel = Ask("Does the serial number contain a vowel?")).Value;

        private bool? hasParallelPort;
        internal bool HasParallelPort() => hasParallelPort.HasValue
            ? hasParallelPort.Value
            : (hasParallelPort = Ask("Does the bomb have a parallel port?")).Value;

    }
}
