using APBD_Cw1_s30786.Services;

namespace APBD_Cw1_s30786;

public class Program
{
    public static void Main(string[] args)
    {
        var catalogServices = new CatalogServices();
        var rentalServices = new RentalServices(catalogServices);
        var reportServices = new ReportServices();

        var input = new ConsoleInput();
        
        var catalogHandler = new CatalogConsoleHandler(catalogServices, reportServices, input);
        var rentalHandler = new RentalConsoleHandler(catalogServices, rentalServices, reportServices, input);

        var ui = new ConsoleUI(catalogHandler, rentalHandler);
        ui.Run();
    }
}


