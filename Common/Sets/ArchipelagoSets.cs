using System.Collections.Generic;

namespace TerrariaFlagRandomizer.Common.Sets
{
    internal class ArchipelagoSets
    {
        public static Dictionary<int, int> ArchipelagoToRewardID = new Dictionary<int, int>
        {
            { 7777001, 1 }, // Skeletron flag
            { 7777002, 2 }, // Hardmode flag
            { 7777003, 3 }, // Mechs flag
            { 7777004, 4 }, // Plantera flag
            { 7777005, 5 }, // Golem flag
            { 7777200, 0 } // Progressive Loot Bag
        };

        public static Dictionary<int, int> ArchipelagoToLocationID = new Dictionary<int, int>
        {
            { 7777400, 0 }, // King Slime
            { 7777401, 1 }, // Eye of Cthulhu
            { 7777402, 2 }, // Eater of Worlds
            { 7777403, 3 }, // Brain of Cthulhu
            { 7777404, 5 }, // Queen Bee
            { 7777405, 6 }, // Deerclops
            { 7777406, 4 }, // Skeletron
            { 7777407, 7 }, // Wall of Flesh
            { 7777408, 8 }, // Queen Slime
            { 7777409, 9 }, // Twins
            { 7777410, 10 }, // Destroyer
            { 7777411, 11 }, // Skeletron Prime
            { 7777412, 12 }, // Plantera
            { 7777413, 17 }, // Mourning Wood
            { 7777414, 18 }, // Pumpking
            { 7777415, 19 }, // Everscream
            { 7777416, 20 }, // Santa-NK1
            { 7777417, 21 }, // Ice Queen
            { 7777418, 15 }, // Empress of Light
            { 7777419, 13 }, // Golem
            { 7777420, 14 }, // Duke Fishron
            { 7777421, 16 }, // Lunatic Cultist
            { 7777422, 26 }, // Moon Lord
            { 7777500, 23 }, // Hallow Mimic
            { 7777501, 24 }, // Corrupt Mimic
            { 7777502, 25 }, // Crimson Mimic
            { 7777503, 22 } // Mothron
        };

        public static Dictionary<string, int> LocationToArchipelagoID = new Dictionary<string, int>()
        {
            { "KingSlimeReward", 7777400 },
            { "EoCReward", 7777401 },
            { "EoWReward", 7777402 },
            { "BoCReward", 7777403 },
            { "SkeletronReward", 7777406 },
            { "QueenBeeReward", 7777404 },
            { "DeerclopsReward", 7777405 },
            { "WoFReward", 7777407 },
            { "QueenSlimeReward", 7777408 },
            { "TwinsReward", 7777409 },
            { "DestroyerReward", 7777410 },
            { "PrimeReward", 7777411 },
            { "PlanteraReward", 7777412 },
            { "GolemReward", 7777419 },
            { "FishronReward", 7777420 },
            { "EmpressReward", 7777418 },
            { "CultistReward", 7777421 },
            { "MourningWoodReward", 7777413 },
            { "PumpkingReward", 7777414 },
            { "EverscreamReward", 7777415 },
            { "SantankReward", 7777416 },
            { "IceQueenReward", 7777417 },
            { "MothronReward", 7777503 },
            { "HallowMimicReward", 7777500 },
            { "CorruptMimicReward", 7777501 },
            { "CrimsonMimicReward", 7777502 },
            { "MoonLordReward", 7777422 }
        };
    }
}
