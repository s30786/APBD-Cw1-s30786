namespace APBD_Cw1_s30786;

public class ConsoleInput
{
    public string ReadRequiredString(string prompt)
    {
        Console.Write(prompt);
        string? value = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException("Wartość nie może być pusta");

        return value.Trim();
    }

    public int ReadInt(string prompt)
    {
        Console.Write(prompt);
        string? input = Console.ReadLine();

        if (!int.TryParse(input, out int value))
            throw new InvalidOperationException("Podano nieprawidłową liczbę");

        return value;
    }

    public bool ReadBool(string prompt)
    {
        string input = ReadRequiredString(prompt).ToLower();

        return input switch
        {
            "t" => true,
            "tak" => true,
            "y" => true,
            "yes" => true,
            "si" => true,
            "da" => true,
            "n" => false,
            "nie" => false,
            "no" => false,
            "niet" => false,
            _ => throw new InvalidOperationException("Podaj t/tak albo n/nie.")
        };
    }
}