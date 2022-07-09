
namespace TerrariaFlagRandomizer.Common
{
    public class Location
    {
        public string name { get; set; }
        public string type { get; set; }
        public string[] requirements { get; set; }

        public Location(string name, string type, string[] requirements)
        {
            this.name = name;
            this.type = type;
            this.requirements = requirements;
        }
    }
}
