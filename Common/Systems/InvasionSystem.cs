using Terraria;
using System.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace TerrariaFlagRandomizer.Common.Systems
{
    public class InvasionSystem : ModSystem
    {
        public static bool defeatedGoblins = false;
        public static bool defeatedSnowmen = false;
        public static bool defeatedPirates = false;
        public static bool defeatedMartians = false;

        public override void OnWorldLoad()
        {
            defeatedGoblins = false;
            defeatedSnowmen = false;
            defeatedPirates = false;
            defeatedMartians = false;
        }

        public override void OnWorldUnload()
        {
            defeatedGoblins = false;
            defeatedSnowmen = false;
            defeatedPirates = false;
            defeatedMartians = false;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            if (defeatedGoblins) tag["defeatedGoblins"] = true;
            if (defeatedSnowmen) tag["defeatedSnowmen"] = true;
            if (defeatedPirates) tag["defeatedPirates"] = true;
            if (defeatedMartians) tag["defeatedMartians"] = true;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            defeatedGoblins = tag.ContainsKey("defeatedGoblins");
            defeatedSnowmen = tag.ContainsKey("defeatedSnowmen");
            defeatedPirates = tag.ContainsKey("defeatedPirates");
            defeatedMartians = tag.ContainsKey("defeatedMartians");
        }

        public override void NetSend(BinaryWriter writer)
        {
            var flags = new BitsByte(defeatedGoblins, defeatedSnowmen, defeatedPirates, defeatedMartians, false, false, false, false);
            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            defeatedGoblins = flags[0];
            defeatedSnowmen = flags[1];
            defeatedPirates = flags[2];
            defeatedMartians = flags[3];
        }

        public override void PreUpdateInvasions()
        {
            if(Main.invasionSize <= 0)
            {
                if(Main.invasionType == 1)
                  {
                    if (!defeatedGoblins)
                    {
                        RewardsHandler.SpawnReward(26);
                        defeatedGoblins = true;
                    }
                } else if(Main.invasionType == 2)
                {
                    if (!defeatedSnowmen)
                    {
                        RewardsHandler.SpawnReward(27);
                        defeatedSnowmen = true;
                    }
                } else if(Main.invasionType == 3)
                {
                     if (!defeatedPirates)
                    {
                        RewardsHandler.SpawnReward(28);
                        defeatedPirates = true;
                    }
                } else if(Main.invasionType == 4)
                {
                    if (!defeatedMartians)
                    {
                        RewardsHandler.SpawnReward(29);
                        defeatedMartians = true;
                    }
                }
            }
        }
    }
}
