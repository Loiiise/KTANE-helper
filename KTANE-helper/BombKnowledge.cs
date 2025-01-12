using KTANE_helper.IOHandler;
using System.Collections.Generic;

namespace KTANE_helper;

public class BombKnowledge
{
    public BombKnowledge(IIOHandler ioHandler)
    {
        _ioHandler = ioHandler;
    }

    private int? batteries;

    internal int Batteries() => batteries.HasValue
        ? batteries.Value
        : (batteries = _ioHandler.IntQuery("How many batteries are present on the bomb?")).Value;

    private bool? serialNumberLastDigitEven;

    internal bool SerialNumberLastDigitEven() => serialNumberLastDigitEven.HasValue
        ? serialNumberLastDigitEven.Value
        : (serialNumberLastDigitEven = _ioHandler.IntQuery("What is the last digit of the serial number?") % 2 == 0).Value;
    internal bool SerialNumberLastDigitOdd() => !this.SerialNumberLastDigitEven();

    private List<string> litIndicators = new();
    internal bool LitIndicator(string indicator)
    {
        indicator = indicator.ToUpper();
        if (litIndicators.Contains(indicator)) return true;

        bool present = _ioHandler.Ask($"Lit indicator \"{indicator}\"?");
        if (present) litIndicators.Add(indicator);

        return present;
    }

    private bool? serialNumberContainsVowel;
    internal bool SerialNumberContainsVowel() => serialNumberContainsVowel.HasValue
        ? serialNumberContainsVowel.Value
        : (serialNumberContainsVowel = _ioHandler.Ask("Does the serial number contain a vowel?")).Value;

    private bool? hasParallelPort;

    internal bool HasParallelPort() => hasParallelPort.HasValue
        ? hasParallelPort.Value
        : (hasParallelPort = _ioHandler.Ask("Does the bomb have a parallel port?")).Value;

    private readonly IIOHandler _ioHandler;
}
