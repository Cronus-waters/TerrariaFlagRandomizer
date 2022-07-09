namespace TerrariaFlagRandomizer.Common
{
    public class Reward
    {
        public int id { get; set; }
        public string name { get; set; }
        public Location location { get; set; }

        public Reward(int id, string name, Location location)
        {
            this.id = id;
            this.name = name;
            this.location = location;
        }
    }
}
