﻿namespace KTANE_helper.Logic.IO.Tests;

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
    public string ReadOutputLine(int skip = 0)
    {
        for (int i = 0; i < skip; ++i)
        {
            _outputQueue.Dequeue();
        }
        return _outputQueue.Dequeue();
    }

    private Queue<string> _inputQueue = new();
    private Queue<string> _outputQueue = new();
}
