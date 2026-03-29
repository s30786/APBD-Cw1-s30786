using APBD_Cw1_s30786.Domain;

namespace APBD_Cw1_s30786;

public class CatalogServices
{
    private readonly List<User> _users = [];
    private readonly List<Equipment> _equipment = [];

    public IReadOnlyCollection<User> Users => _users.AsReadOnly();
    public IReadOnlyCollection<Equipment> Equipment => _equipment.AsReadOnly();

    public void AddUser(User user)
    {
        ArgumentNullException.ThrowIfNull(user);                                            //ale fajne XD

        if (_users.Any(u => u.Id == user.Id))
            throw new InvalidOperationException($"Użytkownik o ID {user.Id} już istnieje"); //zgodnie z moją najlepszą wiedzą to się nigdy nie wydaży ale cóż szkodzi dodać zbędny kod (p.s. miałem w planach zapis do pliku ale pozostało to w planach)

        _users.Add(user);
    }

    public void AddEquipment(Equipment equipment)
    {
        ArgumentNullException.ThrowIfNull(equipment);

        if (_equipment.Any(e => e.Id == equipment.Id))
            throw new InvalidOperationException($"Sprzęt o ID {equipment.Id} już istnieje");

        _equipment.Add(equipment);
    }

    public List<User> GetAllUsers()
    {
        return _users
            .OrderBy(u => u.Id)
            .ToList();
    }

    public List<Equipment> GetAllEquipment()
    {
        return _equipment
            .OrderBy(e => e.Id)
            .ToList();
    }

    public List<Equipment> GetAvailableEquipment()
    {
        return _equipment
            .Where(e => e.Status == EquipmentStatus.Available)
            .OrderBy(e => e.Id)
            .ToList();
    }

    public User GetUserById(int userId)
    {
        var user = _users.FirstOrDefault(u => u.Id == userId);

        return user ?? throw new InvalidOperationException($"Nie znaleziono użytkownika o ID {userId}");    //ciekawe może i nie rozumiem i nie odtworzę ale jest krócej
        
        // if (user is null)
        //     throw new InvalidOperationException($"Nie znaleziono użytkownika o ID {userId}.");
    }

    public Equipment GetEquipmentById(int equipmentId)
    {
        var equipment = _equipment.FirstOrDefault(e => e.Id == equipmentId);

        return equipment ?? throw new InvalidOperationException($"Nie znaleziono sprzętu o ID {equipmentId}"); // jak wyżej 
    }

    public void MarkEquipmentAsUnavailable(int equipmentId)
    {
        var equipment = GetEquipmentById(equipmentId);

        if (equipment.Status == EquipmentStatus.Rented)
            throw new InvalidOperationException(
                "Nie można oznaczyć jako niedostępnego sprzętu, który jest aktualnie wypożyczony");

        equipment.MarkAsUnavailable();
    }

    public void MarkEquipmentAsAvailable(int equipmentId)
    {
        var equipment = GetEquipmentById(equipmentId);

        if (equipment.Status == EquipmentStatus.Rented)
            throw new InvalidOperationException(
                "Nie można ręcznie ustawić jako dostępnego sprzętu, który jest aktualnie wypożyczony");

        equipment.MarkAsAvailable();
    }
}
