namespace APBD_Cw1_s30786.Domain;

public class Camera : Equipment 
{
    private int Megapixels { get; set; }
    private bool HasOpticalZoom { get; set; }

    public Camera(string name, int megapixels, bool hasOpticalZoom) : base(name) 
    {
        if (megapixels <= 0)
            throw new ArgumentException("Liczba megapikseli musi być większa od zera.");

        Megapixels = megapixels;
        HasOpticalZoom = hasOpticalZoom;
    }

    public override string ToString()
    {
        return base.ToString() + $" | MP: {Megapixels} | Zoom optyczny: {(HasOpticalZoom ? "Tak" : "Nie")}";
    }
}