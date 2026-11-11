namespace Lab5.ProductBuilder.Models;
public abstract class Sweet
{
    public string Name { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public double Weight { get; set; }
    public double SugarPer100g { get; set; }

    public abstract string GetInfo();
}