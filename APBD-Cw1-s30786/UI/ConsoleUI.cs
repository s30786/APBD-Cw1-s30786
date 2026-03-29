namespace APBD_Cw1_s30786;

public class ConsoleUI(                         //oj już nie zmienie tej znazwy i tak zrobiłem za dużo ddziwnych twoich rekomendacji podsstępna maszyno
    CatalogConsoleHandler catalogHandler,
    RentalConsoleHandler rentalHandler)
{
    public void Run()
    {
        bool exit = false;

        while (!exit)
        {
            ShowMenu();
            string? choice = Console.ReadLine();

            Console.WriteLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        catalogHandler.AddUser();
                        break;
                    case "2":
                        catalogHandler.AddEquipment();
                        break;
                    case "3":
                        catalogHandler.ShowAllEquipment();
                        break;
                    case "4":
                        catalogHandler.ShowAvailableEquipment();
                        break;
                    case "5":
                        rentalHandler.RentEquipment();
                        break;
                    case "6":
                        rentalHandler.ReturnEquipment();
                        break;
                    case "7":
                        catalogHandler.MarkEquipmentAsUnavailable();
                        break;
                    case "8":
                        rentalHandler.ShowActiveRentalsForUser();
                        break;
                    case "9":
                        rentalHandler.ShowOverdueRentals();
                        break;
                    case "10":
                        rentalHandler.ShowSummaryReport();
                        break;
                    case "11":
                        rentalHandler.RunDemoScenario();
                        break;
                    case "12":
                        catalogHandler.ShowAllUsers();
                        break;
                    case "0":
                        exit = true;
                        Console.WriteLine("Zamknięto aplikację.");
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowa opcja.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }

            if (exit) continue;
            Console.WriteLine();
            Console.WriteLine("Naciśnij dowolny klawisz, aby wrócić do menu...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    private void ShowMenu()
    {
        Console.WriteLine("===== UCZELNIANA WYPOŻYCZALNIA SPRZĘTU =====");
        Console.WriteLine("1. Dodaj użytkownika");
        Console.WriteLine("2. Dodaj sprzęt");
        Console.WriteLine("3. Wyświetl cały sprzęt");
        Console.WriteLine("4. Wyświetl dostępny sprzęt");
        Console.WriteLine("5. Wypożycz sprzęt");
        Console.WriteLine("6. Zwróć sprzęt");
        Console.WriteLine("7. Oznacz sprzęt jako niedostępny");
        Console.WriteLine("8. Wyświetl aktywne wypożyczenia użytkownika");
        Console.WriteLine("9. Wyświetl przeterminowane wypożyczenia");
        Console.WriteLine("10. Wygeneruj raport");
        Console.WriteLine("11. Uruchom scenariusz demonstracyjny");
        Console.WriteLine("12. Wyświetl użytkowników");
        Console.WriteLine("ctrl + c. Wyjście"); //może i switch dalej jest na 0 ale no, hihi
        Console.Write("Wybierz opcję: ");
    }
}
