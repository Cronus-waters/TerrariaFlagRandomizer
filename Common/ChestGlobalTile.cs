using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using TerrariaFlagRandomizer.Common.Helpers;
using TerrariaFlagRandomizer.Common.Sets;
using TerrariaFlagRandomizer.Common.Systems;

namespace TerrariaFlagRandomizer.Common
{
    internal class ChestGlobalTile : GlobalTile
    {
        public override void RightClick(int i, int j, int type)
        {
            int frameX = Main.tile[i, j].TileFrameX;
            int frameY = Main.tile[i, j].TileFrameY;
            int chestType = RandomizerUtils.GetChestType(i, j, type, frameX, Main.tile[i, j].WallType);
            Vector2 origin = new Vector2(i - ((frameX % 36) / 18), j - ((frameY % 36) / 18));
            bool sendReward = false;
            switch (chestType)
            {
                case (int)Enums.ChestTypes.Common:
                    Main.NewText("Common");
                    if (RandomizerSystem.commonChestLocations.Contains(origin))
                    {
                        Main.NewText("Chestsanity");
                        sendReward = true;
                        RandomizerSystem.commonChestLocations.Remove(origin);
                    }
                    break;
                case (int)Enums.ChestTypes.Ice:
                    Main.NewText("Ice");
                    if (RandomizerSystem.iceChestLocations.Contains(origin))
                    {
                        Main.NewText("Chestsanity");
                        sendReward = true;
                        RandomizerSystem.iceChestLocations.Remove(origin);
                    }
                    break;
                case (int)Enums.ChestTypes.Desert:
                    Main.NewText("Desert");
                    if (RandomizerSystem.desertChestLocations.Contains(origin))
                    {
                        Main.NewText("Chestsanity");
                        sendReward = true;
                        RandomizerSystem.desertChestLocations.Remove(origin);
                    }
                    break;
                case (int)Enums.ChestTypes.Dungeon:
                    Main.NewText("Dungeon");
                    if (RandomizerSystem.dungeonChestLocations.Contains(origin))
                    {
                        Main.NewText("Chestsanity");
                        sendReward = true;
                        RandomizerSystem.dungeonChestLocations.Remove(origin);
                    }
                    break;
                case (int)Enums.ChestTypes.Jungle:
                    Main.NewText("Jungle");
                    if (RandomizerSystem.jungleChestLocations.Contains(origin))
                    {
                        Main.NewText("Chestsanity");
                        sendReward = true;
                        RandomizerSystem.jungleChestLocations.Remove(origin);
                    }
                    break;
                case (int)Enums.ChestTypes.Shadow:
                    Main.NewText("Shadow");
                    if (RandomizerSystem.shadowChestLocations.Contains(origin))
                    {
                        Main.NewText("Chestsanity");
                        sendReward = true;
                        RandomizerSystem.shadowChestLocations.Remove(origin);
                    }
                    break;
                case (int)Enums.ChestTypes.Temple:
                    Main.NewText("Temple");
                    if (RandomizerSystem.templeChestLocations.Contains(origin))
                    {
                        Main.NewText("Chestsanity");
                        sendReward = true;
                        RandomizerSystem.templeChestLocations.Remove(origin);
                    }
                    break;
                case (int)Enums.ChestTypes.Sky:
                    Main.NewText("Sky");
                    if (RandomizerSystem.skyChestLocations.Contains(origin))
                    {
                        Main.NewText("Chestsanity");
                        sendReward = true;
                        RandomizerSystem.skyChestLocations.Remove(origin);
                    }
                    break;
                case (int)Enums.ChestTypes.Pyramid:
                    Main.NewText("Pyramid");
                    if (RandomizerSystem.pyramidChestLocations.Contains(origin))
                    {
                        Main.NewText("Chestsanity");
                        sendReward = true;
                        RandomizerSystem.pyramidChestLocations.Remove(origin);
                    }
                    break;
                case (int)Enums.ChestTypes.Ocean:
                    Main.NewText("Ocean");
                    if (RandomizerSystem.oceanChestLocations.Contains(origin))
                    {
                        Main.NewText("Chestsanity");
                        sendReward = true;
                        RandomizerSystem.oceanChestLocations.Remove(origin);
                    }
                    break;
                case (int)Enums.ChestTypes.Biome:
                    Main.NewText("Biome");
                    if (RandomizerSystem.biomeChestLocations.Contains(origin))
                    {
                        Main.NewText("Chestsanity");
                        sendReward = true;
                        RandomizerSystem.biomeChestLocations.Remove(origin);
                    }
                    break;
                default:
                    Main.NewText("Not a chest, or not a relevant chest type");
                    break;
            }
            if (sendReward)
            {
                int chest = RandomizerSystem.chestsanityCurrentCounts[chestType];
                if (chest < RandomizerSystem.chestsanityMaxCounts[chestType])
                {
                    string location = LocationSets.chestNames[chestType] + chest;
                    int reward = RandomizerSystem.locationRewardPairs[location];
                    RewardsHandler.SpawnRewardGeneric(reward);
                    RandomizerSystem.chestsanityCurrentCounts[chestType] += 1;
                }
            }
        }
    }
}
