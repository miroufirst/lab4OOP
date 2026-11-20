using QueueLib;

namespace QueueApp;

internal static class Program
{
    private static void Main()
    {
        var ui = new ConsoleUI();

        ui.ShowMessage("Лабораторна робота 3.4. Делегати, події, лямбда-вирази. Варіант 6.");

        int capacity = ui.ReadPositiveInt("Введіть ємність черги (ціле число більше 0): ");
        var queue = new BoundedQueue(capacity);

        var listener = new QueueEventListener(ui);
        listener.Subscribe(queue);

        ui.ShowMessage($"Чергу створено. Ємність: {capacity}.");

        bool running = true;
        while (running)
        {
            ui.ShowMenu();
            string choice = ui.ReadLine("Ваш вибір: ").Trim();

            switch (choice)
            {
                case "1":
                    AddItem(ui, queue);
                    break;
                case "2":
                    RemoveItem(ui, queue);
                    break;
                case "3":
                    ui.ShowQueue(queue);
                    break;
                case "4":
                    queue.Clear();
                    break;
                case "5":
                    CheckLetters(ui);
                    break;
                case "0":
                    running = false;
                    break;
                default:
                    ui.ShowError("Невідомий пункт меню. Введіть число від 0 до 5.");
                    break;
            }
        }

        listener.Unsubscribe(queue);
        ui.ShowMessage("До побачення!");
    }

    private static void AddItem(ConsoleUI ui, BoundedQueue queue)
    {
        int value = ui.ReadInt("Введіть ціле число: ");

        if (queue.TryEnqueue(value))
        {
            ui.ShowMessage($"Число {value} додано в чергу.");
        }
        else
        {
            ui.ShowError("Черга повна, додати число неможливо.");
        }
    }

    private static void RemoveItem(ConsoleUI ui, BoundedQueue queue)
    {
        if (queue.TryDequeue(out int value))
        {
            ui.ShowMessage($"З черги видалено число {value}.");
        }
        else
        {
            ui.ShowError("Черга порожня, видаляти нічого.");
        }
    }

    private static void CheckLetters(ConsoleUI ui)
    {
        string text = ui.ReadLine("Введіть рядок: ");

        LetterCheckHandler anonymousCheck = LetterChecks.AnonymousMethod;
        LetterCheckHandler lambdaCheck = LetterChecks.Lambda;

        bool anonymousResult = anonymousCheck(text);
        bool lambdaResult = lambdaCheck(text);

        string anonymousAnswer = anonymousResult ? "літери є" : "літер немає";
        string lambdaAnswer = lambdaResult ? "літери є" : "літер немає";

        ui.ShowMessage($"Анонімний метод: {anonymousAnswer}.");
        ui.ShowMessage($"Лямбда-вираз: {lambdaAnswer}.");
    }
}