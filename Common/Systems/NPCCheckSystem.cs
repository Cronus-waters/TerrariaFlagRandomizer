using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace TerrariaFlagRandomizer.Common.Systems
{
    internal class NPCCheckSystem : ModSystem
    {
        public static bool firstSkeletronKill = false;
        public static bool firstWOFKill = false;
        public static bool firstMothronKill = false;
        public static bool firstHallowMimicKill = false;
        public static bool firstCorruptMimicKill = false;
        public static bool firstCrimsonMimicKill = false;
        public static bool firstEaterKill = false;
        public static bool firstBrainKill = false;
        public static bool firstTwinsKill = false;
        public static bool firstDestroyerKill = false;
        public static bool firstPrimeKill = false;
        public static bool firstPlantKill = false;
        public static bool firstGolemKill = false;

        public override void OnWorldLoad()
        {
            firstSkeletronKill = false;
            firstWOFKill = false;
            firstMothronKill = false;
            firstHallowMimicKill = false;
            firstCorruptMimicKill = false;
            firstCrimsonMimicKill = false;
            firstEaterKill = false;
            firstBrainKill = false;
            firstTwinsKill = false;
            firstDestroyerKill = false;
            firstPrimeKill = false;
            firstPlantKill = false;
            firstGolemKill = false;
    }

        public override void OnWorldUnload()
        {
            firstSkeletronKill = false;
            firstWOFKill = false;
            firstMothronKill = false;
            firstHallowMimicKill = false;
            firstCorruptMimicKill = false;
            firstCrimsonMimicKill = false;
            firstEaterKill = false;
            firstBrainKill = false;
            firstTwinsKill = false;
            firstDestroyerKill = false;
            firstPrimeKill = false;
            firstPlantKill = false;
            firstGolemKill = false;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            if (firstSkeletronKill) tag["firstSkeletronKill"] = true;
            if (firstWOFKill) tag["firstWOFKill"] = true;
            if (firstMothronKill) tag["firstMothronKill"] = true;
            if (firstHallowMimicKill) tag["firstHallowMimicKill"] = true;
            if (firstCorruptMimicKill) tag["firstCorruptMimicKill"] = true;
            if (firstCrimsonMimicKill) tag["firstCrimsonMimicKill"] = true;
            if (firstEaterKill) tag["firstEaterKill"] = true;
            if (firstBrainKill) tag["firstBrainKill"] = true;
            if (firstTwinsKill) tag["firstTwinsKill"] = true;
            if (firstDestroyerKill) tag["firstDestroyerKill"] = true;
            if (firstPrimeKill) tag["firstPrimeKill"] = true;
            if (firstPlantKill) tag["firstPlantKill"] = true;
            if (firstGolemKill) tag["firstGolemKill"] = true;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            firstSkeletronKill = tag.ContainsKey("firstSkeletronKill");
            firstWOFKill = tag.ContainsKey("firstWOFKill");
            firstMothronKill = tag.ContainsKey("firstMothronKill");
            firstHallowMimicKill = tag.ContainsKey("firstHallowMimicKill");
            firstCorruptMimicKill = tag.ContainsKey("firstCorruptMimicKill");
            firstCrimsonMimicKill = tag.ContainsKey("firstCrimsonMimicKill");
            firstEaterKill = tag.ContainsKey("firstEaterKill");
            firstBrainKill = tag.ContainsKey("firstBrainKill");
            firstTwinsKill = tag.ContainsKey("firstTwinsKill");
            firstDestroyerKill = tag.ContainsKey("firstDestroyerKill");
            firstPrimeKill = tag.ContainsKey("firstPrimeKill");
            firstPlantKill = tag.ContainsKey("firstPlantKill");
            firstGolemKill = tag.ContainsKey("firstGolemKill");
        }

        public override void NetSend(BinaryWriter writer)
        {
            var flags = new BitsByte(firstSkeletronKill, firstMothronKill, firstHallowMimicKill, firstCorruptMimicKill, firstCrimsonMimicKill, firstEaterKill, firstBrainKill, firstWOFKill);
            writer.Write(flags);

            flags = new BitsByte(firstTwinsKill, firstDestroyerKill, firstPrimeKill, firstPlantKill, firstGolemKill, false, false, false);
            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            firstSkeletronKill = flags[0];
            firstMothronKill = flags[1];
            firstHallowMimicKill = flags[2];
            firstCorruptMimicKill = flags[3];
            firstCrimsonMimicKill = flags[4];
            firstEaterKill = flags[5];
            firstBrainKill = flags[6];
            firstWOFKill = flags[7];

            flags = reader.ReadByte();
            firstTwinsKill = flags[0];
            firstDestroyerKill = flags[1];
            firstPrimeKill = flags[2];
            firstPlantKill = flags[3];
            firstGolemKill = flags[4];
        }
    }
}
