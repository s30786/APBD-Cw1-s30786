using APBD_Cw1_s30786.Domain;

namespace APBD_Cw1_s30786.Services;

public class RentalServices
{
    private const int StudentRentalLimit = 2;
    private const int EmployeeRentalLimit = 5;
    private const decimal PenaltyPerDay = 10m;

    private readonly List<Rental> _rentals = [];
    private readonly CatalogServices _catalogServices;

    public IReadOnlyCollection<Rental> Rentals => _rentals.AsReadOnly();

    public RentalServices(CatalogServices catalogServices)
    {
        _catalogServices = catalogServices ?? throw new ArgumentNullException(nameof(catalogServices));
    }

    public Rental RentEquipment(int userId, int equipmentId, int rentalDays, DateTime? rentalDate = null)
    {
        if (rentalDays <= 0)
            throw new InvalidOperationException("Liczba dni wypożyczenia musi być większa od zera");

        var user = _catalogServices.GetUserById(userId);
        var equipment = _catalogServices.GetEquipmentById(equipmentId);

        switch (equipment.Status)
        {
            case EquipmentStatus.Unavailable:
                throw new InvalidOperationException("Sprzęt jest niedostępny");
            case EquipmentStatus.Rented:
                throw new InvalidOperationException("Sprzęt jest już wypożyczony");
        }

        int activeRentalsCount = GetActiveRentalsForUser(userId).Count;
        int userLimit = GetUserRentalLimit(user);

        if (activeRentalsCount >= userLimit)
            throw new InvalidOperationException(
                $"Użytkownik osiągnął limit aktywnych wypożyczeń: {userLimit}");

        var startDate = (rentalDate ?? DateTime.Now).Date;
        var dueDate = startDate.AddDays(rentalDays);

        var rental = new Rental(user, equipment, startDate, dueDate);

        _rentals.Add(rental);
        equipment.MarkAsRented();

        return rental;
    }

    public Rental ReturnEquipment(int equipmentId, DateTime? returnDate = null)
    {
        var activeRental = _rentals.FirstOrDefault(r =>
            r.Equipment.Id == equipmentId && !r.IsReturned);

        if (activeRental is null)
            throw new InvalidOperationException("Brak aktywnego wypożyczenia dla tego sprzętu");

        var actualReturnDate = (returnDate ?? DateTime.Now).Date;
        var penalty = CalculatePenalty(activeRental.DueDate, actualReturnDate);

        activeRental.Return(actualReturnDate, penalty);
        activeRental.Equipment.MarkAsAvailable();

        return activeRental;
    }

    public List<Rental> GetActiveRentalsForUser(int userId)
    {
        _catalogServices.GetUserById(userId);

        return _rentals
            .Where(r => r.User.Id == userId && !r.IsReturned)
            .OrderBy(r => r.DueDate)
            .ToList();
    }

    public List<Rental> GetAllActiveRentals()
    {
        return _rentals
            .Where(r => !r.IsReturned)
            .OrderBy(r => r.DueDate)
            .ToList();
    }

    public List<Rental> GetOverdueRentals(DateTime? asOfDate = null)
    {
        var date = (asOfDate ?? DateTime.Now).Date;

        return _rentals
            .Where(r => !r.IsReturned && r.DueDate.Date < date)
            .OrderBy(r => r.DueDate)
            .ToList();
    }

    public List<Rental> GetAllRentals()
    {
        return _rentals
            .OrderByDescending(r => r.RentalDate)
            .ToList();
    }

    private int GetUserRentalLimit(User user)
    {
        return user.UserType switch
        {
            UserType.Student => StudentRentalLimit,
            UserType.Employee => EmployeeRentalLimit,
            _ => throw new InvalidOperationException("Nieobsługiwany typ użytkownika.")
        };
    }

    private decimal CalculatePenalty(DateTime dueDate, DateTime returnDate)
    {
        if (returnDate.Date <= dueDate.Date)
            return 0m;

        int overdueDays = (returnDate.Date - dueDate.Date).Days;
        return overdueDays * PenaltyPerDay;
    }
}
