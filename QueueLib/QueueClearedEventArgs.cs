namespace QueueLib;

public class QueueClearedEventArgs : EventArgs
{
    public QueueClearedEventArgs(int removedCount, DateTime clearedAt)
    {
        RemovedCount = removedCount;
        ClearedAt = clearedAt;
    }

    public int RemovedCount { get; }
    public DateTime ClearedAt { get; }
}