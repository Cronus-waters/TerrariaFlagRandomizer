using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;
using Terraria.Utilities;
using TerrariaFlagRandomizer.Common.Helpers;
using TerrariaFlagRandomizer.Common.Sets;
using TerrariaFlagRandomizer.Common.Configs;

namespace TerrariaFlagRandomizer.Common.Systems
{
    public class RandomizerSystem : ModSystem
    {
        // Saved variables
        public static Dictionary<string, int> locationRewardPairs;
        public static int progressiveTier;

        // Variables used in worldgen, no need to save
        public static int numCommonChests = 0;
        public static int numIceChests = 0;
        public static int numDesertChests = 0;
        public static int numDungeonChests = 0;
        public static int numJungleChests = 0;
        public static int numShadowChests = 0;
        public static int numTempleChests = 0;
        public static int numSkyChests = 0;
        public static int numPyramidChests = 0;
        public static int numOceanChests = 0;
        public static int numBiomeChests = 0;

        public static List<Vector2> commonChestLocations;
        public static List<Vector2> iceChestLocations;
        public static List<Vector2> desertChestLocations;
        public static List<Vector2> dungeonChestLocations;
        public static List<Vector2> jungleChestLocations;
        public static List<Vector2> shadowChestLocations;
        public static List<Vector2> templeChestLocations;
        public static List<Vector2> skyChestLocations;
        public static List<Vector2> pyramidChestLocations;
        public static List<Vector2> oceanChestLocations;
        public static List<Vector2> biomeChestLocations;

        public static int[] chestsanityMaxCounts;
        public static int[] chestsanityCurrentCounts;

        public static void Initialize()
        {
            locationRewardPairs = null;
            progressiveTier = -1;
            numCommonChests = 0;
            numIceChests = 0;
            numDesertChests = 0;
            numDungeonChests = 0;
            numJungleChests = 0;
            numShadowChests = 0;
            numTempleChests = 0;
            numSkyChests = 0;
            numPyramidChests = 0;
            numOceanChests = 0;
            numBiomeChests = 0;
            commonChestLocations = null;
            iceChestLocations = null;
            desertChestLocations = null;
            dungeonChestLocations = null;
            jungleChestLocations = null;
            shadowChestLocations = null;
            templeChestLocations = null;
            skyChestLocations = null;
            pyramidChestLocations = null;
            oceanChestLocations = null;
            biomeChestLocations = null;
            chestsanityMaxCounts = null;
            chestsanityCurrentCounts = null;
        }
        public override void OnWorldLoad()
        {
            locationRewardPairs = new Dictionary<string, int>();
            progressiveTier = -1;
            commonChestLocations = null;
            iceChestLocations = null;
            desertChestLocations = null;
            dungeonChestLocations = null;
            jungleChestLocations = null;
            shadowChestLocations = null;
            templeChestLocations = null;
            skyChestLocations = null;
            pyramidChestLocations = null;
            oceanChestLocations = null;
            biomeChestLocations = null;
            chestsanityMaxCounts = null;
            chestsanityCurrentCounts = null;
        }

        public override void OnWorldUnload()
        {
            locationRewardPairs = null;
            progressiveTier = -1;
            commonChestLocations = null;
            iceChestLocations = null;
            desertChestLocations = null;
            dungeonChestLocations = null;
            jungleChestLocations = null;
            shadowChestLocations = null;
            templeChestLocations = null;
            skyChestLocations = null;
            pyramidChestLocations = null;
            oceanChestLocations = null;
            biomeChestLocations = null;
            chestsanityMaxCounts = null;
            chestsanityCurrentCounts = null;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            if (locationRewardPairs.Count != 0)
            {
                tag["rewards"] = locationRewardPairs.Select(pair => new TagCompound
                {
                    ["location"] = pair.Key,
                    ["reward"] = pair.Value
                }).ToList();
            }
            if(progressiveTier >= 0)
            {
                tag["progressiveTier"] = progressiveTier;
            }
            if(chestsanityMaxCounts != null)
            {
                tag.Add("chestsanityCommon", commonChestLocations);
                tag.Add("chestsanityIce", iceChestLocations);
                tag.Add("chestsanityDesert", desertChestLocations);
                tag.Add("chestsanityDungeon", dungeonChestLocations);
                tag.Add("chestsanityJungle", jungleChestLocations);
                tag.Add("chestsanityShadow", shadowChestLocations);
                tag.Add("chestsanityTemple", templeChestLocations);
                tag.Add("chestsanitySky", skyChestLocations);
                tag.Add("chestsanityPyramid", pyramidChestLocations);
                tag.Add("chestsanityOcean", oceanChestLocations);
                tag.Add("chestsanityBiome", biomeChestLocations);
                tag.Add("chestsanityMax", chestsanityMaxCounts);
                tag.Add("chestsanityCurrent", chestsanityCurrentCounts);
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
            if (tag.ContainsKey("progressiveTier"))
            {
                progressiveTier = tag.GetAsInt("progressiveTier");
            }
            if (tag.ContainsKey("chestsanityCommon"))
            {
                commonChestLocations = tag.Get<List<Vector2>>("chestsanityCommon");
                iceChestLocations = tag.Get<List<Vector2>>("chestsanityIce");
                desertChestLocations = tag.Get<List<Vector2>>("chestsanityDesert");
                dungeonChestLocations = tag.Get<List<Vector2>>("chestsanityDungeon");
                jungleChestLocations = tag.Get<List<Vector2>>("chestsanityJungle");
                shadowChestLocations = tag.Get<List<Vector2>>("chestsanityShadow");
                templeChestLocations = tag.Get<List<Vector2>>("chestsanityTemple");
                skyChestLocations = tag.Get<List<Vector2>>("chestsanitySky");
                pyramidChestLocations = tag.Get<List<Vector2>>("chestsanityPyramid");
                oceanChestLocations = tag.Get<List<Vector2>>("chestsanityOcean");
                biomeChestLocations = tag.Get<List<Vector2>>("chestsanityBiome");
                chestsanityMaxCounts = tag.Get<int[]>("chestsanityMax");
                chestsanityCurrentCounts = tag.Get<int[]>("chestsanityCurrent");
            }
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
            progress.Message = "Initializing randomizer";

            // Initialize locations list, world tweaks
            wasCrimson = WorldGen.crimson;
            Main.drunkWorld = true;
            Main.ActiveWorldFileData.DrunkWorld = true;
            RandomizerSystem.Initialize();
            LocationsHelper.Initialize();
            int numLocations;// = LocationsHelper.allLocations.Count;
            //LocationsHelper.RemoveInacessible(LocationsHelper.inaccessible);
            UnifiedRandom rand = new UnifiedRandom(Main.ActiveWorldFileData.Seed);
            RandomizerSystem.locationRewardPairs = new Dictionary<string, int>();
            var configInstance = ModContent.GetInstance<GenerationConfigs>();

            // Get initial list of locations
            Console.WriteLine("Calculating locations");
            progress.Message = "Calculating locations";
            //bool chestsanityFlag = ModContent.GetInstance<GenerationConfigs>().ChestsanityToggle;
            if (configInstance.ChestsanityToggle)
            {
                var chestFlags = new BitsByte
                (
                    configInstance.ChestsanityIncludeSkyIslands,
                    configInstance.ChestsanityIncludePyramids,
                    configInstance.ChestsanityIncludeOceanChests,
                    configInstance.ChestsanityIncludeBiomeChests
                );
                int[] chestRates = new int[]
                {
                    configInstance.ChestsanityUndergroundChestRate,
                    configInstance.ChestsanityIceChestRate,
                    configInstance.ChestsanityDesertChestRate,
                    configInstance.ChestsanityDungeonChestRate,
                    configInstance.ChestsanityJungleShrineChestRate,
                    configInstance.ChestsanityShadowChestRate,
                    configInstance.ChestsanityTempleChestRate
                };
                RandomizerSystem.commonChestLocations = new List<Vector2>();
                RandomizerSystem.iceChestLocations = new List<Vector2>();
                RandomizerSystem.desertChestLocations = new List<Vector2>();
                RandomizerSystem.dungeonChestLocations = new List<Vector2>();
                RandomizerSystem.jungleChestLocations = new List<Vector2>();
                RandomizerSystem.shadowChestLocations = new List<Vector2>();
                RandomizerSystem.templeChestLocations = new List<Vector2>();
                RandomizerSystem.skyChestLocations = new List<Vector2>();
                RandomizerSystem.pyramidChestLocations = new List<Vector2>();
                RandomizerSystem.oceanChestLocations = new List<Vector2>();
                RandomizerSystem.biomeChestLocations = new List<Vector2>();
                RandomizerSystem.chestsanityCurrentCounts = new int[11] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

                foreach (Chest chest in Main.chest)
                {
                    if (chest == null) break;
                    Tile t = Main.tile[chest.x, chest.y];
                    if (t.TileType == TileID.Containers)
                    {
                        int tileFrame = t.TileFrameX / 36;
                        // Common check: (unlocked) Gold, Mahogany, Mushroom, Marble, Granite; needs to be underground
                        if (tileFrame == 1 || tileFrame == 8 || tileFrame == 32 || tileFrame == 50 || tileFrame == 51)
                        {
                            if (chest.y >= WorldGen.worldSurfaceHigh)
                            {
                                if (t.WallType == WallID.SandstoneBrick) // Pyramid chest
                                {
                                    if (chestFlags[1]) // Only add if pyramids are enabled
                                    {
                                        RandomizerSystem.numPyramidChests += 1;
                                        RandomizerSystem.pyramidChestLocations.Add(new Vector2(chest.x, chest.y));
                                    }
                                }
                                else // Common chest
                                {
                                    RandomizerSystem.numCommonChests += 1;
                                    RandomizerSystem.commonChestLocations.Add(new Vector2(chest.x, chest.y));
                                }
                            }
                        }
                        // Ice check: Ice chest
                        if (tileFrame == 11)
                        {
                            RandomizerSystem.numIceChests += 1;
                            RandomizerSystem.iceChestLocations.Add(new Vector2(chest.x, chest.y));
                        }
                        // Dungeon/Sky check: Locked Gold chest (underground if Dungeon, above ground if Sky in drunk seeds)
                        if (tileFrame == 2)
                        {
                            if (chest.y > WorldGen.worldSurfaceHigh) // Dungeon
                            {
                                RandomizerSystem.numDungeonChests += 1;
                                RandomizerSystem.dungeonChestLocations.Add(new Vector2(chest.x, chest.y));
                            }
                            else // Sky, only if Sky Islands are enabled
                            {
                                if (chestFlags[0])
                                {
                                    RandomizerSystem.numSkyChests += 1;
                                    RandomizerSystem.skyChestLocations.Add(new Vector2(chest.x, chest.y));
                                }
                            }
                        }
                        // Jungle check: Ivy chest
                        if (tileFrame == 10)
                        {
                            RandomizerSystem.numJungleChests += 1;
                            RandomizerSystem.jungleChestLocations.Add(new Vector2(chest.x, chest.y));
                        }
                        // Shadow check: Locked Shadow chest in the underworld
                        if (tileFrame == 4)
                        {
                            if (chest.y >= Main.maxTilesY - 200) // Just in case something happens and a shadow chest generates outside of Hell
                            {
                                RandomizerSystem.numShadowChests += 1;
                                RandomizerSystem.shadowChestLocations.Add(new Vector2(chest.x, chest.y));
                            }
                        }
                        // Temple check: Lihzahrd chest
                        if (tileFrame == 16)
                        {
                            RandomizerSystem.numTempleChests += 1;
                            RandomizerSystem.templeChestLocations.Add(new Vector2(chest.x, chest.y));
                        }
                        // Sky check in normal seeds: Skyware chest
                        if(tileFrame == 13)
                        {
                            if (chestFlags[0])
                            {
                                RandomizerSystem.numSkyChests += 1;
                                RandomizerSystem.skyChestLocations.Add(new Vector2(chest.x, chest.y));
                            }
                        }
                        // Ocean check: Water chests near the surface, close to the world edge
                        if(tileFrame == 17 && WorldGen.oceanDepths(chest.x, chest.y))
                        {
                            if (chestFlags[2])
                            {
                                RandomizerSystem.numOceanChests += 1;
                                RandomizerSystem.oceanChestLocations.Add(new Vector2(chest.x, chest.y));
                            }
                        }
                        // Biome check 1: pre-1.4 Biome chests underground (should always be within the dungeon)
                        if(tileFrame >= 23 && tileFrame <= 27)
                        {
                            if (chestFlags[3])
                            {
                                RandomizerSystem.numBiomeChests += 1;
                                RandomizerSystem.biomeChestLocations.Add(new Vector2(chest.x, chest.y));
                            }
                        }
                    }
                    // Desert check: Sandstone chest
                    if (t.TileType == TileID.Containers2)
                    {
                        int tileFrame = t.TileFrameX / 36;
                        if (tileFrame == 10)
                        {
                            RandomizerSystem.numDesertChests += 1;
                            RandomizerSystem.desertChestLocations.Add(new Vector2(chest.x, chest.y));
                        }
                        // Biome check 2: Desert biome chest (should always be within the dungeon)
                        if(tileFrame == 13)
                        {
                            if (chestFlags[3])
                            {
                                RandomizerSystem.numBiomeChests += 1;
                                RandomizerSystem.biomeChestLocations.Add(new Vector2(chest.x, chest.y));
                            }
                        }
                    }
                }
                RandomizerSystem.chestsanityMaxCounts = new int[11]
                {
                    RandomizerSystem.numCommonChests,
                    RandomizerSystem.numIceChests,
                    RandomizerSystem.numDesertChests,
                    RandomizerSystem.numDungeonChests,
                    RandomizerSystem.numJungleChests,
                    RandomizerSystem.numShadowChests,
                    RandomizerSystem.numTempleChests,
                    RandomizerSystem.numSkyChests,
                    RandomizerSystem.numPyramidChests,
                    RandomizerSystem.numOceanChests,
                    RandomizerSystem.numBiomeChests
                };
                for(int i = 0; i < chestRates.Length; i++)
                {
                    RandomizerSystem.chestsanityMaxCounts[i] = (int)(RandomizerSystem.chestsanityMaxCounts[i] * ((float)chestRates[i] / 100));
                }
            }

            LocationsHelper.MakeLocations();
            numLocations = LocationsHelper.allLocations.Count;
            LocationsHelper.RemoveInacessible(LocationsHelper.inaccessible);


            // Get generator settings from mod config
            bool progressiveFlags = ModContent.GetInstance<GenerationConfigs>().ProgressiveFlagsToggle;
            if (progressiveFlags)
            {
                RandomizerSystem.progressiveTier = 0;
            }

            Location location = null;
            // Shuffle important flags
            Console.WriteLine("Shuffling boss flags");
            progress.Message = "Randomizing flags";
            Reward reward = null;
            int flagID = 0;
            for(int i = 1; i < 6; i++)
            {
                location = FindLocation(rand, i == 1);
                if (progressiveFlags)
                {
                    flagID = 6;
                    LocationsHelper.progressiveLevel += 1;
                }
                else
                {
                    flagID = i;
                    LocationsHelper.inaccessible.Remove(Flags.FlagNames[flagID]);
                }
                reward = new Reward(flagID, Flags.FlagNames[flagID], location);
                LocationsHelper.rewards.Add(reward);
                RandomizerSystem.locationRewardPairs.Add(location.name, reward.id);
                Console.WriteLine("Flag " + Flags.FlagNames[flagID] + " added at location " + location.name);
                numLocations--;
            }

            // Shuffle loot bags (and other junk items/traps)
            // TODO: Traps
            Console.WriteLine("Filling pool with junk items");
            progress.Message = "Adding junk items";
            LocationsHelper.ResetLocations();
            LocationsHelper.RemoveSelected();
            for(int i = 0; i < numLocations; i++)
            {
                //location = FindLocation(rand, false);
                location = LocationsHelper.allLocations[i];
                reward = new Reward(0, "Loot bag", location);
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
                location = LocationsHelper.allLocations[rand.Next(LocationsHelper.allLocations.Count)];
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

            // Write generator settings
            writer.WriteLine("Settings:\n{");
            writer.WriteLine("\tProgressive Flags: " + ModContent.GetInstance<GenerationConfigs>().ProgressiveFlagsToggle);
            writer.WriteLine("\tChestsanity: " + ModContent.GetInstance<GenerationConfigs>().ChestsanityToggle);
            if (ModContent.GetInstance<GenerationConfigs>().ChestsanityToggle)
            {
                writer.WriteLine("\t{");
                writer.WriteLine("\t\t% of common chests: " + ModContent.GetInstance<GenerationConfigs>().ChestsanityUndergroundChestRate + "%");
                writer.WriteLine("\t\t% of Underground Ice chests: " + ModContent.GetInstance<GenerationConfigs>().ChestsanityIceChestRate + "%");
                writer.WriteLine("\t\t% of Desert chests: " + ModContent.GetInstance<GenerationConfigs>().ChestsanityDesertChestRate + "%");
                writer.WriteLine("\t\t% of Dungeon chests: " + ModContent.GetInstance<GenerationConfigs>().ChestsanityDungeonChestRate + "%");
                writer.WriteLine("\t\t% of Jungle chests: " + ModContent.GetInstance<GenerationConfigs>().ChestsanityJungleShrineChestRate + "%");
                writer.WriteLine("\t\t% of Shadow chests: " + ModContent.GetInstance<GenerationConfigs>().ChestsanityShadowChestRate + "%");
                writer.WriteLine("\t\t% of Temple chests: " + ModContent.GetInstance<GenerationConfigs>().ChestsanityTempleChestRate + "%");
                writer.WriteLine("\t\tInclude Sky Islands: " + ModContent.GetInstance<GenerationConfigs>().ChestsanityIncludeSkyIslands);
                writer.WriteLine("\t\tInclude Pyramid chests: " + ModContent.GetInstance<GenerationConfigs>().ChestsanityIncludePyramids);
                writer.WriteLine("\t\tInclude Ocean chests: " + ModContent.GetInstance<GenerationConfigs>().ChestsanityIncludeOceanChests);
                writer.WriteLine("\t\tInclude Biome chests: " + ModContent.GetInstance<GenerationConfigs>().ChestsanityIncludeBiomeChests);
                writer.WriteLine("\t}");
            }
            writer.WriteLine("}\n");

            // Write rewards list
            writer.WriteLine("Locations: \n{");
            LocationsHelper.ResetLocations();
            foreach(Location location in LocationsHelper.allLocations)
            {
                Reward reward = LocationsHelper.rewards.Find(r => r.location.Equals(location) == true);
                if(reward != null)
                {
                    writer.WriteLine("\t" + location.name + ": " + reward.name);
                }
            }
            writer.WriteLine("}\n");

            writer.WriteLine("Debug");
            writer.WriteLine("Common " + RandomizerSystem.chestsanityMaxCounts[0]);
            writer.WriteLine("Ice " + RandomizerSystem.chestsanityMaxCounts[1]);
            writer.WriteLine("Desert " + RandomizerSystem.chestsanityMaxCounts[2]);
            writer.WriteLine("Dungeon " + RandomizerSystem.chestsanityMaxCounts[3]);
            writer.WriteLine("Jungle " + RandomizerSystem.chestsanityMaxCounts[4]);
            writer.WriteLine("Shadow " + RandomizerSystem.chestsanityMaxCounts[5]);
            writer.WriteLine("Temple " + RandomizerSystem.chestsanityMaxCounts[6]);
            writer.WriteLine("Sky " + RandomizerSystem.chestsanityMaxCounts[7]);
            writer.WriteLine("Pyramid " + RandomizerSystem.chestsanityMaxCounts[8]);
            writer.WriteLine("Ocean " + RandomizerSystem.chestsanityMaxCounts[9]);
            writer.WriteLine("Biome " + RandomizerSystem.chestsanityMaxCounts[10]);

            writer.WriteLine("Common locations:");
            writer.WriteLine("{");
            foreach(Vector2 v in RandomizerSystem.commonChestLocations)
            {
                writer.WriteLine("\t[" + v.X + ", " + v.Y + "]");
            }
            writer.WriteLine("}");
            writer.WriteLine("Ice locations:");
            writer.WriteLine("{");
            foreach (Vector2 v in RandomizerSystem.iceChestLocations)
            {
                writer.WriteLine("\t[" + v.X + ", " + v.Y + "]");
            }
            writer.WriteLine("}");
            writer.WriteLine("Desert locations:");
            writer.WriteLine("{");
            foreach (Vector2 v in RandomizerSystem.desertChestLocations)
            {
                writer.WriteLine("\t[" + v.X + ", " + v.Y + "]");
            }
            writer.WriteLine("}");
            writer.WriteLine("Dungeon locations:");
            writer.WriteLine("{");
            foreach (Vector2 v in RandomizerSystem.dungeonChestLocations)
            {
                writer.WriteLine("\t[" + v.X + ", " + v.Y + "]");
            }
            writer.WriteLine("}");
            writer.WriteLine("Jungle locations:");
            writer.WriteLine("{");
            foreach (Vector2 v in RandomizerSystem.jungleChestLocations)
            {
                writer.WriteLine("\t[" + v.X + ", " + v.Y + "]");
            }
            writer.WriteLine("}");
            writer.WriteLine("Shadow locations:");
            writer.WriteLine("{");
            foreach (Vector2 v in RandomizerSystem.shadowChestLocations)
            {
                writer.WriteLine("\t[" + v.X + ", " + v.Y + "]");
            }
            writer.WriteLine("}");
            writer.WriteLine("Temple locations:");
            writer.WriteLine("{");
            foreach (Vector2 v in RandomizerSystem.templeChestLocations)
            {
                writer.WriteLine("\t[" + v.X + ", " + v.Y + "]");
            }
            writer.WriteLine("}");
            writer.WriteLine("Sky locations:");
            writer.WriteLine("{");
            foreach (Vector2 v in RandomizerSystem.skyChestLocations)
            {
                writer.WriteLine("\t[" + v.X + ", " + v.Y + "]");
            }
            writer.WriteLine("}");
            writer.WriteLine("Pyramid locations:");
            writer.WriteLine("{");
            foreach (Vector2 v in RandomizerSystem.pyramidChestLocations)
            {
                writer.WriteLine("\t[" + v.X + ", " + v.Y + "]");
            }
            writer.WriteLine("}");
            writer.WriteLine("Ocean locations:");
            writer.WriteLine("{");
            foreach (Vector2 v in RandomizerSystem.oceanChestLocations)
            {
                writer.WriteLine("\t[" + v.X + ", " + v.Y + "]");
            }
            writer.WriteLine("}");
            writer.WriteLine("Biome locations:");
            writer.WriteLine("{");
            foreach (Vector2 v in RandomizerSystem.biomeChestLocations)
            {
                writer.WriteLine("\t[" + v.X + ", " + v.Y + "]");
            }
            writer.WriteLine("}");

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
