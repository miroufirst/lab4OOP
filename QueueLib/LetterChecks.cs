namespace QueueLib;

public delegate bool LetterCheckHandler(string text);

public static class LetterChecks
{
    public static readonly LetterCheckHandler AnonymousMethod = delegate (string text)
    {
        foreach (char symbol in text)
        {
            if (char.IsLetter(symbol))
            {
                return true;
            }
        }

        return false;
    };

    public static readonly LetterCheckHandler Lambda = text => text.Any(char.IsLetter);
}