using QueueLib;

namespace QueueApp;

public class QueueEventListener
{
    private readonly ConsoleUI _ui;

    public QueueEventListener(ConsoleUI ui)
    {
        _ui = ui;
    }

    public void Subscribe(BoundedQueue queue)
    {
        queue.QueueCleared += OnQueueCleared;
    }

    public void Unsubscribe(BoundedQueue queue)
    {
        queue.QueueCleared -= OnQueueCleared;
    }

    public void OnQueueCleared(object? sender, QueueClearedEventArgs e)
    {
        string source = "невідомий об'єкт";

        if (sender is BoundedQueue queue)
        {
            source = $"{nameof(BoundedQueue)} (ємність {queue.Capacity})";
        }

        _ui.ShowEvent($"ПОДІЯ: чергу очищено. Джерело: {source}. " +
                      $"Видалено елементів: {e.RemovedCount}. Час: {e.ClearedAt:HH:mm:ss}.");
    }
}