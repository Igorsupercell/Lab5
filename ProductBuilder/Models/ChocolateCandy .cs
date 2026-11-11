namespace Lab5.ProductBuilder.Models;
public class ChocolateCandy : Sweet
{
    public string ChocolateType { get; set; } = string.Empty;
    public bool HasFilling { get; set; }

    public override string GetInfo() =>
        $"[Шоколад] {Name} | Вес: {Weight}г | Сахар: {SugarPer100g}г | Тип: {ChocolateType} | Начинка: {(HasFilling ? "имеется" : "нет")}";
}
