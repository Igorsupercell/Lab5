using Lab5.ProductBuilder.Models;

namespace Lab5.ProductBuilder.Services
{
    public class SugarComparer : IComparer<Sweet>
    {
        public int Compare(Sweet x, Sweet y) => x.SugarPer100g.CompareTo(y.SugarPer100g);
    }
}
