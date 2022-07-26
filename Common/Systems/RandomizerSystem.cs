using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using TerrariaFlagRandomizer.Common.Helpers;
using TerrariaFlagRandomizer.Common.Sets;
using Terraria.Utilities;
using System;
using System.IO;
using Terraria.ModLoader.IO;

namespace TerrariaFlagRandomizer.Common.Systems
{
    public class RandomizerSystem : ModSystem
    {
        public static Dictionary<string, int> locationRewardPairs;

        public override void OnWorldLoad()
        {
            locationRewardPairs = new Dictionary<string, int>();
        }

        public override void OnWorldUnload()
        {
            locationRewardPairs = null;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            if(locationRewardPairs.Count != 0)
            {
                tag["rewards"] = locationRewardPairs.Select(pair => new TagCompound
                {
                    ["location"] = pair.Key,
                    ["reward"] = pair.Value
                }).ToList();
            }
        }

        public override void LoadWorldData(TagCompound tag)
        {
            Dictionary<string, int> list = new Dictionary<string, int>();
            foreach(var entry in tag.GetList<TagCompound>("rewards"))
            {
                list.Add(entry.GetString("location"), entry.GetInt("reward"));
            }
            locationRewardPairs = list;
        }
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int evilIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Corruption"));
            if(evilIndex != -1)
            {
                tasks.Insert(evilIndex + 1, new TurnOffDrunkGenAfterEvilBiome("More evil", 1f));
                tasks.Insert(evilIndex, new TurnOnDrunkGenBeforeEvilBiome("Tweaking evil", 1f));
            }
            int finalIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Final Cleanup"));
            if(finalIndex != -1) {
                tasks.Insert(finalIndex + 1, new RandomizeFlags("Randomizing", 1f));
            }
        }
    }

    public class RandomizeFlags : GenPass
    {
        private bool wasCrimson;
        public RandomizeFlags(string name, float loadWeight) : base(name, loadWeight) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            Console.WriteLine("Initializing randomizer");
            progress.Message = "Randomizing";

            // Initialize locations list, world tweaks
            wasCrimson = WorldGen.crimson;
            Main.drunkWorld = true;
            Main.ActiveWorldFileData.DrunkWorld = true;
            LocationsHelper.Initialize();
            int numLocations = LocationsHelper.locations.Count;
            LocationsHelper.RemoveInacessible(LocationsHelper.inaccessible);
            UnifiedRandom rand = new UnifiedRandom(Main.ActiveWorldFileData.Seed);
            RandomizerSystem.locationRewardPairs = new Dictionary<string, int>();

            Location location = null;
            // Shuffle important flags
            Console.WriteLine("Shuffling boss flags");
            progress.Message = "Randomizing flags";
            for(int i = 1; i < 6; i++)
            {
                location = FindLocation(rand, i == 1);
                Reward reward = new Reward(i, Flags.FlagNames[i], location);
                LocationsHelper.rewards.Add(reward);
                LocationsHelper.inaccessible.Remove(Flags.FlagNames[i]);
                RandomizerSystem.locationRewardPairs.Add(location.name, reward.id);
                Console.WriteLine("Flag " + Flags.FlagNames[i] + " added at location " + location.name);
                numLocations--;
            }

            // Shuffle loot bags (and other junk items/traps)
            // TODO: Traps
            Console.WriteLine("Filling pool with junk items");
            progress.Message = "Adding junk items";
            for(int i = 0; i < numLocations; i++)
            {
                location = FindLocation(rand, false);
                Reward reward = new Reward(0, "Loot bag", location);
                Console.WriteLine("Loot Bag added at location " + location.name);
                LocationsHelper.rewards.Add(reward);
                RandomizerSystem.locationRewardPairs.Add(location.name, reward.id);
            }

            // Generate spoiler log (TODO: add setting to disable)
            progress.Message = "Creating spoiler log";
            Console.WriteLine("Writing spoiler log");
            MakeSpoilerLog();

            Console.WriteLine("Randomization finished");
        }

        private Location FindLocation(UnifiedRandom rand, bool dontReset)
        {
            Location location = null;
            bool foundLocation = false;
            if (!dontReset)
            {
                LocationsHelper.ResetLocations();
                LocationsHelper.RemoveInacessible(LocationsHelper.inaccessible);
            }
            while (!foundLocation)
            {
                location = LocationsHelper.locations[rand.Next(LocationsHelper.locations.Count)];
                foundLocation = LocationsHelper.rewards.Find(r => r.location == location) == null;
            }
            return location;
        }

        private void MakeSpoilerLog()
        {
            string filename = "SPOILER_" + Main.worldName + "_" + Main.ActiveWorldFileData.Seed + ".txt";
            if(File.Exists(Main.WorldPath + Path.DirectorySeparatorChar + filename))
            {
                Console.WriteLine("Spoiler log already exists, overwriting");
            }
            string spoilerPath = Path.Combine(Main.WorldPath, filename);

            using FileStream file = new FileStream(spoilerPath, FileMode.Create);
            using StreamWriter writer = new StreamWriter(file);

            // Get world info from seed
            string seedText = Main.ActiveWorldFileData.GetFullSeedText();
            string[] seedParams = seedText.Split('.');
            string[] worldInfo = new string[3];

            switch (Main.maxTilesX)
            {
                case 4200:
                    worldInfo[0] = "Small";
                    break;
                case 6400:
                    worldInfo[0] = "Medium";
                    break;
                case 8400:
                    worldInfo[0] = "Large";
                    break;
                default:
                    worldInfo[0] = "Unknown";
                    break;
            }

            switch (seedParams[1])
            {
                case "1":
                    worldInfo[1] = "Classic";
                    break;
                case "2":
                    worldInfo[1] = "Expert";
                    break;
                case "3":
                    worldInfo[1] = "Master";
                    break;
                case "4":
                    worldInfo[1] = "Journey";
                    break;
                default:
                    worldInfo[1] = "Unknown";
                    break;
            }
            if (wasCrimson) worldInfo[2] = "Crimson";
            else worldInfo[2] = "Corruption";

            // Write seed metadata
            writer.WriteLine("Metadata: \n{");
            writer.WriteLine("\tSeed: " + seedParams[3]);
            writer.WriteLine("\tDifficulty: " + worldInfo[1]);
            writer.WriteLine("\tWorld Size: " + worldInfo[0]);
            writer.WriteLine("\tWorld Evil: " + worldInfo[2]);
            writer.WriteLine("}\n");

            // Write rewards list
            writer.WriteLine("Locations: \n{");
            LocationsHelper.ResetLocations();
            foreach(Location location in LocationsHelper.locations)
            {
                Reward reward = LocationsHelper.rewards.Find(r => r.location.Equals(location) == true);
                if(reward != null)
                {
                    writer.WriteLine("\t" + location.name + ": " + reward.name);
                }
            }
            writer.WriteLine("}\n");

            // TODO: write playthrough
        }
    }

    public class TurnOnDrunkGenBeforeEvilBiome : GenPass
    {
        public TurnOnDrunkGenBeforeEvilBiome(string name, float loadWeight) : base(name, loadWeight) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            Main.drunkWorld = true;
            WorldGen.drunkWorldGen = true;
        }
    }

    public class TurnOffDrunkGenAfterEvilBiome : GenPass
    {
        public TurnOffDrunkGenAfterEvilBiome(string name, float loadWeight) : base(name, loadWeight) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            if (WorldGen.crimson) progress.Message = "Making the world more bloody";
            else progress.Message = "Making the world more evil";
            Main.drunkWorld = false;
            WorldGen.drunkWorldGen = false;
        }
    }
}
