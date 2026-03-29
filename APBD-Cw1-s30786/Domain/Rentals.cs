namespace APBD_Cw1_s30786.Domain;

public class Rental
{
    public User User { get; }
    public Equipment Equipment { get; }
    public DateTime RentalDate { get; }
    public DateTime DueDate { get; }
    private DateTime? ReturnDate { get; set; } //gubię się kiedy używać _ a kiedy nie, pole jest prywatne ale kompilator mówi że tak się nie pisze xd
    public decimal Penalty { get; private set; }

    public bool IsReturned => ReturnDate.HasValue;
    public bool IsOverdue => !IsReturned && DateTime.Now.Date > DueDate.Date;
    public bool WasReturnedOnTime => ReturnDate is { } returnDate && returnDate.Date <= DueDate.Date;

    public Rental(User user, Equipment equipment, DateTime rentalDate, DateTime dueDate)
    {
        ArgumentNullException.ThrowIfNull(user); //jak rekomendują to git

        ArgumentNullException.ThrowIfNull(equipment); //as above

        if (dueDate.Date < rentalDate.Date)
            throw new ArgumentException("Termin zwrotu nie może być wcześniejszy niż data wypożyczenia.");

        User = user;
        Equipment = equipment;
        RentalDate = rentalDate.Date;
        DueDate = dueDate.Date;
        Penalty = 0m;
    }

    public void Return(DateTime returnDate, decimal penalty)
    {
        if (IsReturned)
            throw new InvalidOperationException("To wypożyczenie zostało już zakończone.");

        if (penalty < 0)
            throw new ArgumentException("Kara nie może być ujemna.");

        ReturnDate = returnDate.Date;
        Penalty = penalty;
    }

    public override string ToString()
    {
        string returnInfo = ReturnDate.HasValue
            ? ReturnDate.Value.ToString("yyyy-MM-dd")
            : "brak";

        return $"{User.FirstName} {User.LastName} | {Equipment.Name} | " +
               $"od {RentalDate:yyyy-MM-dd} do {DueDate:yyyy-MM-dd} | " +
               $"zwrot: {returnInfo} | kara: {Penalty:0.00} PLN";
    }
}