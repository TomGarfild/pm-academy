namespace Task_1._3
{
    public class RegionSettings : IRegionSettings
    {
        public string WebSite { get; }
        public RegionSettings(string webSite) => WebSite = webSite;
        public override string ToString() => WebSite;
    }
}