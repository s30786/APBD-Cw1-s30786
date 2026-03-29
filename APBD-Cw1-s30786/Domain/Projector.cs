namespace APBD_Cw1_s30786.Domain;

public class Projector : Equipment
{
    private int BrightnessLumens { get; set; }
    private string Resolution { get; set; }

    public Projector(string name, int brightnessLumens, string resolution) : base(name)
    {
        if (brightnessLumens <= 0)
            throw new ArgumentException("Jasność musi być większa od zera.");

        if (string.IsNullOrWhiteSpace(resolution))
            throw new ArgumentException("Rozdzielczość nie może być pusta.");

        BrightnessLumens = brightnessLumens;
        Resolution = resolution;
    }

    public override string ToString()
    {
        return base.ToString() + $" | Jasność: {BrightnessLumens} lm | Rozdzielczość: {Resolution}";
    }
}