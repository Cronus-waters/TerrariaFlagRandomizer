using System.Collections.Generic;

namespace TerrariaFlagRandomizer.Common.Sets
{
    public class Flags
    {
        public const int SkeletronFlag = 1;
        public const int HardmodeFlag = 2;
        public const int MechsFlag = 3;
        public const int PlanteraFlag = 4;
        public const int GolemFlag = 5;

        public static List<string> FlagNames = new List<string>() { "Loot Bag", "Skeletron", "Hardmode", "MechBosses", "PlantBoss", "GolemBoss" };
    }
}
