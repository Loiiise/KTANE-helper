using System.Collections.Generic;

namespace KTANE_helper.Logic.IO;

public interface IIOHandler
{
    public void Prompt();
    public void PromptScopeAdd(string str);
    public void PromptScopeUp(string sc);
    public void PromptScopeDown();

    public void Answer<T>(Answer<T> answer);

    public void Show(string message);
    public void ShowLine(string message);
    public string ReadLine();

    public string Query(string message);
    public string Query(string message, IEnumerable<string> allowedValues);
    public IEnumerable<string> QueryMultiple(string message, int n);

    public int IntQuery(string message);
    public int IntQuery(string message, IEnumerable<int> allowedValues);

    public (int, int) CoordinateQuery(string message);
    public (int, int) CoordinateQuery(string message, int minimalValue, int maximalValue);

    public bool Ask(string question);

    public IEnumerable<string> GetLines(int lines);
}
