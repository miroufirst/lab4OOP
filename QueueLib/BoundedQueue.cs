namespace QueueLib;

public class BoundedQueue
{
    private readonly int[] _items; 
    private int _head;           
    private int _count;         

    public event EventHandler<QueueClearedEventArgs>? QueueCleared;

    public BoundedQueue(int capacity)
    {
        if (capacity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity), "Ємність має бути більшою за 0.");
        }

        _items = new int[capacity];
        _head = 0;
        _count = 0;
    }

    public int Capacity => _items.Length;

    public int Count => _count;

    public bool IsEmpty => _count == 0;

    public bool IsFull => _count == _items.Length;

    public bool TryEnqueue(int value)
    {
        if (IsFull)
        {
            return false;
        }

        int tail = (_head + _count) % _items.Length;
        _items[tail] = value;
        _count++;
        return true;
    }

    public bool TryDequeue(out int value)
    {
        if (IsEmpty)
        {
            value = 0;
            return false;
        }

        value = _items[_head];
        _head = (_head + 1) % _items.Length;
        _count--;
        return true;
    }

    public int GetItem(int index)
    {
        if (index < 0 || index >= _count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        return _items[(_head + index) % _items.Length];
    }

    public void Clear()
    {
        int removed = _count;

        Array.Clear(_items, 0, _items.Length);
        _head = 0;
        _count = 0;

        OnQueueCleared(new QueueClearedEventArgs(removed, DateTime.Now));
    }
    protected virtual void OnQueueCleared(QueueClearedEventArgs e)
    {
        QueueCleared?.Invoke(this, e);
    }
}