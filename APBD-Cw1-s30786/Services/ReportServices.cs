using System.Text;
using APBD_Cw1_s30786.Domain;

namespace APBD_Cw1_s30786.Services;

public class ReportServices
{
    public string GenerateSummaryReport(
        IEnumerable<User> users,
        IEnumerable<Equipment> equipment,
        IEnumerable<Rental> rentals,
        DateTime? asOfDate = null)
    {
        var reportDate = (asOfDate ?? DateTime.Now).Date;

        var userList = users.ToList();
        var equipmentList = equipment.ToList();
        var rentalList = rentals.ToList();

        var activeRentals = rentalList.Where(r => !r.IsReturned).ToList();
        var overdueRentals = rentalList
            .Where(r => !r.IsReturned && r.DueDate.Date < reportDate)
            .ToList();

        var totalPenalties = rentalList.Sum(r => r.Penalty);

        var sb = new StringBuilder();

        sb.AppendLine("===== RAPORT WYPOŻYCZALNI =====");
        sb.AppendLine($"Data raportu: {reportDate:yyyy-MM-dd}");
        sb.AppendLine();

        sb.AppendLine("UŻYTKOWNICY");
        sb.AppendLine($"Wszyscy użytkownicy: {userList.Count}");
        sb.AppendLine($"Studenci: {userList.Count(u => u.UserType == UserType.Student)}");
        sb.AppendLine($"Pracownicy: {userList.Count(u => u.UserType == UserType.Employee)}");
        sb.AppendLine();

        sb.AppendLine("SPRZĘT");
        sb.AppendLine($"Wszystkie egzemplarze: {equipmentList.Count}");
        sb.AppendLine($"Dostępny: {equipmentList.Count(e => e.Status == EquipmentStatus.Available)}");
        sb.AppendLine($"Wypożyczony: {equipmentList.Count(e => e.Status == EquipmentStatus.Rented)}");
        sb.AppendLine($"Niedostępny: {equipmentList.Count(e => e.Status == EquipmentStatus.Unavailable)}");
        sb.AppendLine($"Laptopy: {equipmentList.Count(e => e is Laptop)}");
        sb.AppendLine($"Projektory: {equipmentList.Count(e => e is Projector)}");
        sb.AppendLine($"Kamery: {equipmentList.Count(e => e is Camera)}");
        sb.AppendLine();

        sb.AppendLine("WYPOŻYCZENIA");
        sb.AppendLine($"Wszystkie wypożyczenia: {rentalList.Count}");
        sb.AppendLine($"Aktywne wypożyczenia: {activeRentals.Count}");
        sb.AppendLine($"Przeterminowane wypożyczenia: {overdueRentals.Count}");
        sb.AppendLine($"Suma naliczonych kar: {totalPenalties:0.00} PLN");
        sb.AppendLine();

        if (overdueRentals.Count != 0)
        {
            sb.AppendLine("LISTA PRZETERMINOWANYCH WYPOŻYCZEŃ");

            foreach (var rental in overdueRentals)
            {
                sb.AppendLine(
                    $"{rental.User.FirstName} {rental.User.LastName} | " +
                    $"{rental.Equipment.Name} | termin zwrotu: {rental.DueDate:yyyy-MM-dd}");
            }

            sb.AppendLine();
        }

        sb.AppendLine("===== KONIEC RAPORTU =====");

        return sb.ToString();
    }

    public string FormatEquipmentList(IEnumerable<Equipment> equipment)
    {
        var equipmentList = equipment.ToList();

        if (equipmentList.Count == 0)
            return "Brak sprzętu w systemie";

        var sb = new StringBuilder();
        sb.AppendLine("===== LISTA SPRZĘTU =====");

        foreach (var item in equipmentList.OrderBy(e => e.Id))
        {
            sb.AppendLine(item.ToString());
        }

        return sb.ToString();
    }

    public string FormatRentalsList(IEnumerable<Rental> rentals)
    {
        var rentalList = rentals.ToList();

        if (rentalList.Count == 0)
            return "Brak wypożyczeń";

        var sb = new StringBuilder();
        sb.AppendLine("===== LISTA WYPOŻYCZEŃ =====");

        foreach (var rental in rentalList.OrderBy(r => r.DueDate))
        {
            sb.AppendLine(rental.ToString());
        }

        return sb.ToString();
    }

    public string FormatUsersList(IEnumerable<User> users)
    {
        var userList = users.ToList();

        if (userList.Count == 0)
            return "Brak użytkowników w systemie";

        var sb = new StringBuilder();
        sb.AppendLine("===== LISTA UŻYTKOWNIKÓW =====");

        foreach (var user in userList.OrderBy(u => u.Id))
        {
            sb.AppendLine(user.ToString());
        }

        return sb.ToString();
    }
}
