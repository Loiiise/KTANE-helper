namespace KTANE_helper.Logic.IO.Tests;

internal class MockIOHandler : IOHandler
{
    public override void Answer<T>(Answer<T> answer)
    {
        _outputQueue.Enqueue(answer);
    }

    public override string ReadLine() => _inputQueue.Dequeue();
    public override void Show(string message) { }
    public override void ShowLine(string message) { }

    public void EnqueueInputLine(string message) => _inputQueue.Enqueue(message);
    public IIOToken GetAnswer() => _outputQueue.Dequeue();

    private Queue<string> _inputQueue = new();
    private Queue<IIOToken> _outputQueue = new();
}
