using System.Text;
using QueueLib;

namespace QueueApp;

public class ConsoleUI
{
    public ConsoleUI()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
    }

    public void ShowMenu()
    {
        Console.WriteLine();
        Console.WriteLine("===== МЕНЮ =====");
        Console.WriteLine("1 - Додати число в чергу");
        Console.WriteLine("2 - Видалити число з черги");
        Console.WriteLine("3 - Показати чергу");
        Console.WriteLine("4 - Очистити чергу (генерується подія)");
        Console.WriteLine("5 - Перевірити рядок на наявність літер (делегати)");
        Console.WriteLine("0 - Вихід");
    }

    public string ReadLine(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? string.Empty;
    }

    public int ReadInt(string prompt)
    {
        while (true)
        {
            string text = ReadLine(prompt).Trim();

            if (int.TryParse(text, out int value))
            {
                return value;
            }

            ShowError("Це не ціле число. Спробуйте ще раз.");
        }
    }

    public int ReadPositiveInt(string prompt)
    {
        while (true)
        {
            int value = ReadInt(prompt);

            if (value > 0)
            {
                return value;
            }

            ShowError("Число має бути більшим за 0. Спробуйте ще раз.");
        }
    }

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void ShowError(string message)
    {
        WriteColored(message, ConsoleColor.Red);
    }

    public void ShowEvent(string message)
    {
        WriteColored(message, ConsoleColor.Yellow);
    }

    public void ShowQueue(BoundedQueue queue)
    {
        Console.WriteLine($"Черга: {queue.Count} з {queue.Capacity}.");

        if (queue.IsEmpty)
        {
            Console.WriteLine("Черга порожня.");
            return;
        }

        var builder = new StringBuilder("Початок -> ");
        for (int i = 0; i < queue.Count; i++)
        {
            if (i > 0)
            {
                builder.Append(", ");
            }

            builder.Append(queue.GetItem(i));
        }

        builder.Append(" <- Кінець");
        Console.WriteLine(builder.ToString());
    }

    private static void WriteColored(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}