using APBD_Cw1_s30786.Domain;
using APBD_Cw1_s30786.Services;

namespace APBD_Cw1_s30786;

public class RentalConsoleHandler
{
    private readonly CatalogServices _catalogServices;
    private readonly RentalServices _rentalServices;
    private readonly ReportServices _reportServices;
    private readonly ConsoleInput _input;

    public RentalConsoleHandler(
        CatalogServices catalogServices,
        RentalServices rentalServices,
        ReportServices reportServices,
        ConsoleInput input)
    {
        _catalogServices = catalogServices;
        _rentalServices = rentalServices;
        _reportServices = reportServices;
        _input = input;
    }

    public void RentEquipment()
    {
        int userId = _input.ReadInt("Podaj ID użytkownika: ");
        int equipmentId = _input.ReadInt("Podaj ID sprzętu: ");
        int rentalDays = _input.ReadInt("Podaj liczbę dni wypożyczenia: ");

        Rental rental = _rentalServices.RentEquipment(userId, equipmentId, rentalDays);

        Console.WriteLine("Wypożyczenie wykonane poprawnie:");
        Console.WriteLine(rental);
    }

    public void ReturnEquipment()
    {
        int equipmentId = _input.ReadInt("Podaj ID sprzętu do zwrotu: ");
        Rental rental = _rentalServices.ReturnEquipment(equipmentId);

        Console.WriteLine("Zwrot wykonany poprawnie:");
        Console.WriteLine(rental);
    }

    public void ShowActiveRentalsForUser()
    {
        int userId = _input.ReadInt("Podaj ID użytkownika: ");

        var rentals = _rentalServices.GetActiveRentalsForUser(userId);
        string result = _reportServices.FormatRentalsList(rentals);

        Console.WriteLine(result);
    }

    public void ShowOverdueRentals()
    {
        var rentals = _rentalServices.GetOverdueRentals();
        string result = _reportServices.FormatRentalsList(rentals);

        Console.WriteLine(result);
    }

    public void ShowSummaryReport()
    {
        string report = _reportServices.GenerateSummaryReport(
            _catalogServices.Users,
            _catalogServices.Equipment,
            _rentalServices.Rentals);

        Console.WriteLine(report);
    }

    public void RunDemoScenario()
    {
        Console.WriteLine("===== SCENARIUSZ DEMONSTRACYJNY =====");

        var student = new User("Dzień", "Dobry", UserType.Student);
        var employee = new User("Imię", "Nazwisko", UserType.Employee);
        var secondStudent = new User("Jan", "Kowalski", UserType.Student);

        _catalogServices.AddUser(student);
        _catalogServices.AddUser(employee);
        _catalogServices.AddUser(secondStudent);

        var laptop1 = new Laptop("Dell 14 Pro Plus Premium", 16, "Intel X9");
        var laptop2 = new Laptop("Lenovo ThinkPad E14", 32, "AMD AI5");
        var projector = new Projector("Epson SuperProjector", 4200, "1024x786");
        var camera = new Camera("Sony CyberShot", 8, true);

        _catalogServices.AddEquipment(laptop1);
        _catalogServices.AddEquipment(laptop2);
        _catalogServices.AddEquipment(projector);
        _catalogServices.AddEquipment(camera);

        Console.WriteLine("Dodano użytkowników i sprzęt");
        Console.WriteLine();

        var rental1 = _rentalServices.RentEquipment(student.Id, laptop1.Id, 7);
        Console.WriteLine("Poprawne wypożyczenie:");
        Console.WriteLine(rental1);
        Console.WriteLine();

        var rental2 = _rentalServices.RentEquipment(student.Id, projector.Id, 5);
        Console.WriteLine("Drugie poprawne wypożyczenie studenta:");
        Console.WriteLine(rental2);
        Console.WriteLine();

        try
        {
            _rentalServices.RentEquipment(student.Id, camera.Id, 3);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Próba niepoprawnej operacji:");
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine();

        var onTimeRental = _rentalServices.RentEquipment(employee.Id, laptop2.Id, 4);
        var returnedOnTime = _rentalServices.ReturnEquipment(laptop2.Id, onTimeRental.DueDate);
        Console.WriteLine("Zwrot w terminie:");
        Console.WriteLine(returnedOnTime);
        Console.WriteLine();

        var oldCamera = new Camera("Kodak jakiś", 20, true);
        _catalogServices.AddEquipment(oldCamera);

        var lateRental = _rentalServices.RentEquipment(employee.Id, oldCamera.Id, 2, DateTime.Now.AddDays(-7));
        var returnedLate = _rentalServices.ReturnEquipment(oldCamera.Id, DateTime.Now);
        Console.WriteLine("Zwrot opóźniony:");
        Console.WriteLine(returnedLate);
        Console.WriteLine();

        Console.WriteLine("Raport końcowy:");
        ShowSummaryReport();
    }
}
