namespace Task_1._3
{
    public class Region : IRegion
    {
        public string Brand { get; }
        public string Country { get; }

        public Region(string brand, string country)
        {
            Brand = brand;
            Country = country;
        }
        public override int GetHashCode()
        {
            return Brand.GetHashCode() ^ Country.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            return Brand.Equals(((Region)obj)?.Brand) && Country.Equals(((Region)obj)?.Country);
        }

        public override string ToString() => Brand + " " + Country;
    }
}