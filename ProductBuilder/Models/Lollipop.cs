namespace Lab5.ProductBuilder.Models;
public class Lollipop : Sweet
{
    public string Flavor { get; set; } = string.Empty;
    public string Shape { get; set; } = string.Empty;

    public override string GetInfo() =>
        $"[Леденец] {Name} | Вес: {Weight}г | Сахар: {SugarPer100g}г | Вкус: {Flavor} | Форма: {Shape}";
}
