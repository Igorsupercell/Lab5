using Lab5.ProductBuilder.Models;

namespace Lab5.ProductBuilder.Services
{ 
    public class WeightComparer : IComparer<Sweet>
    {
        public int Compare(Sweet x, Sweet y) => x.Weight.CompareTo(y.Weight);
    }
}
