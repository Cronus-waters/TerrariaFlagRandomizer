using System;
using System.Collections.Generic;
using Terraria.ID;

namespace TerrariaFlagRandomizer.Common.Sets
{
    public class LocationSets
    {
        public static List<Location> BossLocations = new List<Location>
        {
            new Location("KingSlimeReward", "Boss", Array.Empty<string>()),
            new Location("EoCReward","Boss", Array.Empty<string>()),
            new Location("EoWReward", "Boss", Array.Empty<string>()),
            new Location("BoCReward", "Boss", Array.Empty<string>()),
            new Location("SkeletronReward", "Boss", Array.Empty<string>()),
            new Location("QueenBeeReward", "Boss", Array.Empty<string>()),
            new Location("DeerclopsReward", "Boss", Array.Empty<string>()),
            new Location("WoFReward", "Boss", Array.Empty<string>()),
            new Location("QueenSlimeReward", "Boss", new string[]{ "Hardmode" }),
            new Location("TwinsReward", "Boss", new string[]{ "Hardmode" }),
            new Location("DestroyerReward", "Boss", new string[]{ "Hardmode" }),
            new Location("PrimeReward", "Boss", new string[]{ "Hardmode" }),
            new Location("PlanteraReward", "Boss", new string[]{ "Hardmode", "MechBosses" }),
            new Location("GolemReward", "Boss", new string[]{ "Hardmode", "PlantBoss" }),
            new Location("FishronReward", "Boss", new string[]{ "Hardmode" }),
            new Location("EmpressReward", "Boss", new string[]{ "Hardmode", "PlantBoss" }),
            new Location("CultistReward", "Boss", new string[]{ "Skeletron", "Hardmode", "GolemBoss" }),
            new Location("MourningWoodReward", "Boss", new string[]{ "Skeletron", "Hardmode", "PlantBoss" }),
            new Location("PumpkingReward", "Boss", new string[]{ "Skeletron", "Hardmode", "PlantBoss" }),
            new Location("EverscreamReward", "Boss", new string[]{ "Skeletron", "Hardmode", "PlantBoss" }),
            new Location("SantankReward", "Boss", new string[]{ "Skeletron", "Hardmode", "PlantBoss" }),
            new Location("IceQueenReward", "Boss", new string[]{ "Skeletron", "Hardmode", "PlantBoss" })
        };

        public static List<Location> MinibossLocations = new List<Location>
        {
            new Location("MothronReward", "Miniboss", new string[]{ "Hardmode", "PlantBoss" }),
            new Location("HallowMimicReward", "Miniboss", new string[]{ "Hardmode" }),
            new Location("CorruptMimicReward", "Miniboss", new string[]{ "Hardmode" }),
            new Location("CrimsonMimicReward", "Miniboss", new string[]{ "Hardmode" })
        };

        public static Dictionary<int, string> CheckToLocation = new Dictionary<int, string>
        {
            { 0, "KingSlimeReward" },
            { 1, "EoCReward" },
            { 2, "EoWReward" },
            { 3, "BoCReward" },
            { 4, "SkeletronReward" },
            { 5, "QueenBeeReward" },
            { 6, "DeerclopsReward" },
            { 7, "WoFReward" },
            { 8, "QueenSlimeReward" },
            { 9, "TwinsReward" },
            { 10, "DestroyerReward" },
            { 11, "PrimeReward" },
            { 12, "PlanteraReward" },
            { 13, "GolemReward" },
            { 14, "FishronReward" },
            { 15, "EmpressReward" },
            { 16, "CultistReward" },
            { 17, "MourningWoodReward" },
            { 18, "PumpkingReward" },
            { 19, "EverscreamReward" },
            { 20, "SantankReward" },
            { 21, "IceQueenReward" },
            { 22, "MothronReward" },
            { 23, "HallowMimicReward" },
            { 24, "CorruptMimicReward" },
            { 25, "CrimsonMimicReward" }
        };

        public static List<Location> GetAllLocations()
        {
            List<Location> list = new List<Location>();
            BossLocations.ForEach(location => list.Add(location));
            MinibossLocations.ForEach(location => list.Add(location));
            return list;
        }
    }
}
