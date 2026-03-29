namespace APBD_Cw1_s30786.Domain;

public class Laptop : Equipment
{
    private int _RamGb { get; set; }
    private string Processor { get; set; }

    public Laptop(string name, int ramGb, string processor) : base(name)
    {
        if (ramGb <= 0)
            throw new ArgumentException("RAM musi być większy od zera.");

        if (string.IsNullOrWhiteSpace(processor))
            throw new ArgumentException("Procesor nie może być pusty.");

        _RamGb = ramGb;
        Processor = processor;
    }

    public override string ToString()
    {
        return base.ToString() + $" | RAM: {_RamGb} GB | CPU: {Processor}";
    }
}