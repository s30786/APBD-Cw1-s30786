namespace APBD_Cw1_s30786.Domain;

public enum UserType
{
    Student,
    Employee
}

public class User
{
    private static int _nextId = 1;

    public int Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserType UserType { get; }

    public User(string firstName, string lastName, UserType userType)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("Imię nie może być puste.");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Nazwisko nie może być puste.");

        Id = _nextId++;
        FirstName = firstName;
        LastName = lastName;
        UserType = userType;
    }

    public override string ToString()
    {
        return $"{Id} | {FirstName} {LastName} | {UserType}";
    }
}