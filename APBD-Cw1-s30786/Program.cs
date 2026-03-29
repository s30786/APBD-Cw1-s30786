//rekomenduje ignorowanie komentarzy
//to swoisty dziennik


using APBD_Cw1_s30786.Services;

namespace APBD_Cw1_s30786;

public class Program
{
    public static void Main(string[] args)
    {
        var catalogServices = new CatalogServices(); //kompilator rekomenduje var ale dziwnie mi z tym
        var rentalServices = new RentalServices(catalogServices);
        var reportServices = new ReportServices();

        var input = new ConsoleInput();
        
        var catalogHandler = new CatalogConsoleHandler(catalogServices, reportServices, input);
        var rentalHandler = new RentalConsoleHandler(catalogServices, rentalServices, reportServices, input);

        var ui = new ConsoleUI(catalogHandler, rentalHandler);
        ui.Run();
    }
}

//zrobienie bazdy danych na cpp to jedna z moich lepszych decyzji na tej uczelni (wtedy planowałem dodać zapis do pliku)

