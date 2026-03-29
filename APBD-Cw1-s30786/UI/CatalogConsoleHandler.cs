using APBD_Cw1_s30786.Domain;
using APBD_Cw1_s30786.Services;

namespace APBD_Cw1_s30786;

public class CatalogConsoleHandler(
    CatalogServices catalogServices,
    ReportServices reportServices,
    ConsoleInput input)
{
    public void AddUser()
    {
        string firstName = input.ReadRequiredString("Podaj imię: ");
        string lastName = input.ReadRequiredString("Podaj nazwisko: ");

        Console.WriteLine("Typ użytkownika:");
        Console.WriteLine("1. Student");
        Console.WriteLine("2. Employee");

        string userTypeChoice = input.ReadRequiredString("Wybór: ");

        UserType userType = userTypeChoice switch
        {
            "1" => UserType.Student,
            "2" => UserType.Employee,
            _ => throw new InvalidOperationException("Nieprawidłowy typ użytkownika")
        };

        var user = new User(firstName, lastName, userType);
        catalogServices.AddUser(user);

        Console.WriteLine("Dodano użytkownika:");
        Console.WriteLine(user);
    }

    public void AddEquipment()
    {
        Console.WriteLine("Typ sprzętu:");
        Console.WriteLine("1. Laptop");
        Console.WriteLine("2. Projector");
        Console.WriteLine("3. Camera");

        string equipmentChoice = input.ReadRequiredString("Wybór: ");
        string name = input.ReadRequiredString("Podaj nazwę sprzętu: ");

        Equipment equipment = equipmentChoice switch
        {
            "1" => CreateLaptop(name),
            "2" => CreateProjector(name),
            "3" => CreateCamera(name),
            _ => throw new InvalidOperationException("Nieprawidłowy typ sprzętu")
        };

        catalogServices.AddEquipment(equipment);

        Console.WriteLine("Dodano sprzęt:");
        Console.WriteLine(equipment);
    }

    public void ShowAllEquipment()
    {
        string result = reportServices.FormatEquipmentList(catalogServices.GetAllEquipment());
        Console.WriteLine(result);
    }

    public void ShowAvailableEquipment()
    {
        string result = reportServices.FormatEquipmentList(catalogServices.GetAvailableEquipment());
        Console.WriteLine(result);
    }

    public void ShowAllUsers()
    {
        string result = reportServices.FormatUsersList(catalogServices.GetAllUsers());
        Console.WriteLine(result);
    }

    public void MarkEquipmentAsUnavailable()
    {
        int equipmentId = input.ReadInt("Podaj ID sprzętu: ");
        catalogServices.MarkEquipmentAsUnavailable(equipmentId);

        Console.WriteLine("Sprzęt oznaczono jako niedostępny");
    }

    private Laptop CreateLaptop(string name)
    {
        int ramGb = input.ReadInt("Podaj ilość RAM (GB): ");
        string processor = input.ReadRequiredString("Podaj procesor: ");

        return new Laptop(name, ramGb, processor);
    }

    private Projector CreateProjector(string name)
    {
        int brightness = input.ReadInt("Podaj jasność (lumens): ");
        string resolution = input.ReadRequiredString("Podaj rozdzielczość: ");

        return new Projector(name, brightness, resolution);
    }

    private Camera CreateCamera(string name)
    {
        int megapixels = input.ReadInt("Podaj megapiksele: ");
        bool hasOpticalZoom = input.ReadBool("Czy ma zoom optyczny? (t/n): ");

        return new Camera(name, megapixels, hasOpticalZoom);
    }
}