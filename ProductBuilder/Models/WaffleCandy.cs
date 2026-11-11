namespace Lab5.ProductBuilder.Models;
public class WaffleCandy : Sweet
{
    public string WaffleType { get; set; } = string.Empty;

    public override string GetInfo() =>
        $"[Вафля] {Name} | Вес: {Weight}г | Сахар: {SugarPer100g}г | Тип вафли: {WaffleType}";
}
