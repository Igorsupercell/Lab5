namespace Lab5.ProductBuilder.Models;
public class Gift
{
    public List<Sweet> Sweets { get; } = new();

    public void Add(Sweet sweet) => Sweets.Add(sweet);

    public double GetTotalWeight() => Sweets.Sum(s => s.Weight);

    public void Sort(IComparer<Sweet> comparer) => Sweets.Sort(comparer);

    public IEnumerable<Sweet> FindBySugar(double min, double max)
    {
        if (min > max)
        {
            (min, max) = (max, min);
        }

        return Sweets.Where(s => s.SugarPer100g >= min && s.SugarPer100g <= max);
    }
}
