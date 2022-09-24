using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using TerrariaFlagRandomizer.Common.Sets;

namespace TerrariaFlagRandomizer.Common.Helpers
{
    public class RandomizerUtils
    {
        private static readonly List<int> DungeonWallSet = new List<int>{ 7, 8, 9, 94, 95, 96, 97, 98, 99 };

        public static int GetChestType(int x, int y, int type, int frameX, int wall)
        {
            int frame = frameX / 36;
            if(type == TileID.Containers)
            {
                if(frame == 1 || frame == 8 || frame == 32 || frame == 50 || frame == 51) // Common, Dungeon, Pyramid, or Sky (Drunk seed)
                {
                    if(frame == 1) // Gold chest: Common, Dungeon, Pyramid, or Sky (drunk seed)
                    {
                        if(wall == WallID.SandstoneBrick) // Behind Sandstone Brick wall: Pyramid
                        {
                            return (int)Enums.ChestTypes.Pyramid;
                        }
                        if(y > Main.worldSurface) // Underground: Common or Dungeon
                        {
                            if (DungeonWallSet.Contains(wall)) // Behind Dungoen wall: Dungeon
                            {
                                return (int)Enums.ChestTypes.Dungeon;
                            }
                            else // Common
                            {
                                return (int)Enums.ChestTypes.Common;
                            }
                        }
                        else // Above ground: Sky (drunk seed)
                        {
                            return (int)Enums.ChestTypes.Sky;
                        }
                    }
                    // Mahogany, Mushroom, Granite, Marble: Common
                    return (int)Enums.ChestTypes.Common;
                }
                if(frame == 11) // Ice
                {
                    return (int)Enums.ChestTypes.Ice;
                }
                if(frame == 10) // Ivy: Jungle
                {
                    return (int)Enums.ChestTypes.Jungle;
                }
                if(frame == 3) // Locked Shadow: Underworld
                {
                    return (int)Enums.ChestTypes.Shadow;
                }
                if(frame == 16) // Lihzahrd: Temple
                {
                    return (int)Enums.ChestTypes.Temple;
                }
                if(frame == 13) // Skyware: Sky (if above the surface)
                {
                    if(y < Main.worldSurface)
                    {
                        return (int)Enums.ChestTypes.Sky;
                    }
                }
                if(frame == 17) // Water: Ocean (if within bounds of the ocean)
                {
                    if(WorldGen.oceanDepths(x, y))
                    {
                        return (int)Enums.ChestTypes.Ocean;
                    }
                }
                if(frame >= 18 && frame <= 22) // Opened Biome chests (underground behind Dungeon wall)
                {
                    if(y > Main.worldSurface && DungeonWallSet.Contains(wall))
                    {
                        return (int)Enums.ChestTypes.Biome;
                    }
                }
            }
            else
            {
                if(frame == 10) // Sandstone: Desert
                {
                    return (int)Enums.ChestTypes.Desert;
                }
                if(frame == 12) // Opened Desert Biome chest (underground behind Dungeon wall)
                {
                    if(y > Main.worldSurface && DungeonWallSet.Contains(wall))
                    {
                        return (int)Enums.ChestTypes.Biome;
                    }
                }
            }
            return -1; // Either not a chest, or not a relevant chest type
        }

        public static Location MakeChestLocation(int type, int n)
        {
            Location location = null;
            switch (type)
            {
                case (int)Enums.ChestTypes.Common:
                    location = new Location("ChestCommon" + n, "Chest", Array.Empty<string>(), (int)LocationSets.ProgressionLevels.None);
                    break;
                case (int)Enums.ChestTypes.Ice:
                    location = new Location("ChestIce" + n, "Chest", Array.Empty<string>(), (int)LocationSets.ProgressionLevels.None);
                    break;
                case (int)Enums.ChestTypes.Desert:
                    location = new Location("ChestDesert" + n, "Chest", Array.Empty<string>(), (int)LocationSets.ProgressionLevels.None);
                    break;
                case (int)Enums.ChestTypes.Dungeon:
                    location = new Location("ChestDungeon" + n, "Chest", new string[] { "Skeletron" }, (int)LocationSets.ProgressionLevels.Skeletron);
                    break;
                case (int)Enums.ChestTypes.Jungle:
                    location = new Location("ChestJungle" + n, "Chest", Array.Empty<string>(), (int)LocationSets.ProgressionLevels.None);
                    break;
                case (int)Enums.ChestTypes.Shadow:
                    location = new Location("ChestShadow" + n, "Chest", new string[] { "Skeletron" }, (int)LocationSets.ProgressionLevels.Skeletron);
                    break;
                case (int)Enums.ChestTypes.Temple:
                    location = new Location("ChestTemple" + n, "Chest", new string[] { "PlantBoss" }, (int)LocationSets.ProgressionLevels.Plantera);
                    break;
                case (int)Enums.ChestTypes.Sky:
                    location = new Location("ChestSky" + n, "Chest", Array.Empty<string>(), (int)LocationSets.ProgressionLevels.None);
                    break;
                case (int)Enums.ChestTypes.Pyramid:
                    location = new Location("ChestPyramid" + n, "Chest", Array.Empty<string>(), (int)LocationSets.ProgressionLevels.None);
                    break;
                case (int)Enums.ChestTypes.Ocean:
                    location = new Location("ChestOcean" + n, "Chest", Array.Empty<string>(), (int)LocationSets.ProgressionLevels.None);
                    break;
                default:
                    location = new Location("ChestBiome" + n, "Chest", new string[] { "Skeletron", "Hardmode", "PlantBoss" }, (int)LocationSets.ProgressionLevels.Plantera);
                    break;
            }
            return location;
        }
    }
}
