using KTANE_helper.IOHandler;

namespace SolverTests;

internal class MockIOHandler : IOHandler
{
    public override string ReadLine()
    {
        if (!_inputQueue.Any())
        {
            throw new ArgumentOutOfRangeException("No more messages in queue");
        }

        return _inputQueue.Dequeue();
    }

    public override void Show(string message) { }
    public override void ShowLine(string message) => _outputQueue.Enqueue(message);   

    public void EnqueueInputLine(string message) => _inputQueue.Enqueue(message);
    public string ReadOutputLine() => _outputQueue.Dequeue();

    private Queue<string> _inputQueue = new();
    private Queue<string> _outputQueue = new();
}