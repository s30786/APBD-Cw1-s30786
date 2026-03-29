namespace APBD_Cw1_s30786.Domain;

public enum EquipmentStatus
{
    Available,
    Rented,
    Unavailable
}

public abstract class Equipment
{
    private static int _nextId = 1;

    public int Id { get; }
    public string Name { get; }
    public EquipmentStatus Status { get; private set; }

    protected Equipment(string name)                                            
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nazwa sprzętu nie może być pusta.");

        Id = _nextId++;
        Name = name;
        Status = EquipmentStatus.Available;
    }

    public void MarkAsAvailable()
    {
        Status = EquipmentStatus.Available;
    }

    public void MarkAsRented()
    {
        Status = EquipmentStatus.Rented;
    }

    public void MarkAsUnavailable()
    {
        Status = EquipmentStatus.Unavailable;
    }

    public override string ToString()
    {
        return $"{Id} | {Name} | {Status}";
    }
}

//klasa