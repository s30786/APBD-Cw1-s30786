namespace APBD_Cw1_s30786.Domain;

public class Laptop : Equipment
{
    private int RamGb { get;}
    private string Processor { get; }

    public Laptop(string name, int ramGb, string processor) : base(name)
    {
        if (ramGb <= 0)
            throw new ArgumentException("RAM musi być większy od zera.");

        if (string.IsNullOrWhiteSpace(processor))
            throw new ArgumentException("Procesor nie może być pusty.");

        RamGb = ramGb;
        Processor = processor;
    }

    public override string ToString()
    {
        return base.ToString() + $" | RAM: {RamGb} GB | CPU: {Processor}";
    }
}