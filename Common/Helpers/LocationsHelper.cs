using System;
using System.Linq;
using System.Collections.Generic;
using Terraria.ModLoader;
using TerrariaFlagRandomizer.Common.Sets;
using TerrariaFlagRandomizer.Common.Configs;
using TerrariaFlagRandomizer.Common.Systems;

namespace TerrariaFlagRandomizer.Common.Helpers
{
    public class LocationsHelper
    {
        // TODO: Methods to remove checks based on type, or individual checks (based on name)
        // As well as methods to check if a check is accessible
        public static List<Location> allLocations;
        public static List<Location> defaultLocations;
        public static List<Location> chestLocations;
        public static List<string> inaccessible;
        public static List<Reward> rewards;
        public static int progressiveLevel;

        public static void Initialize()
        {
            allLocations = new List<Location>();
            defaultLocations = new List<Location>();
            chestLocations = new List<Location>();
            inaccessible = new List<string>() { "Skeletron", "Hardmode", "MechBosses", "PlantBoss", "GolemBoss" };
            rewards = new List<Reward>();
            progressiveLevel = 0;
            //MakeLocations();
        }

        public static void MakeLocations()
        {
            defaultLocations = LocationSets.GetAllLocations();
            if (ModContent.GetInstance<GenerationConfigs>().ChestsanityToggle && RandomizerSystem.chestsanityMaxCounts != null)
            {
                chestLocations = MakeChestLocations();
            }
            allLocations = defaultLocations.Concat(chestLocations).ToList();
        }

        public static void ResetLocations()
        {
            allLocations.Clear();
            allLocations = defaultLocations.Concat(chestLocations).ToList();
        }

        public static void RemoveInacessible(List<string> requirements)
        {
            if (allLocations == null) Initialize();
            bool progressiveFlags = ModContent.GetInstance<GenerationConfigs>().ProgressiveFlagsToggle;
            for (int i = 0; i < allLocations.Count; i++)
            {
                Location location = allLocations[i];
                if (progressiveFlags)
                {
                    if (location.progressionLevel > progressiveLevel)
                    {
                        allLocations.RemoveAt(i);
                        i--;
                    }
                }
                else
                {
                    if (requirements.Intersect(location.requirements).Any())
                    {
                        allLocations.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        public static string[] MakeRoute(int flag, string[] route = null, string[] flags = null, bool[] flagChecked = null)
        {
            if(route == null) route = new string[0];
            if(flags == null) flags = new string[0];
            if(flagChecked == null) flagChecked = new bool[0];
            string flagName = Flags.FlagNames[flag];
            Reward check = rewards.Find(r => r.id == flag);
            if (check != null)
            {
                if (check.location.requirements.Contains(flagName))
                {
                    // Reward locked behind itself
                    return null;
                }
                if (!route.Contains(check.location.name)) route = route.Prepend(check.location.name).ToArray();
                foreach (string req in check.location.requirements)
                {
                    if (!flags.Contains(req))
                    {
                        flags = flags.Append(req).ToArray();
                        flagChecked = flagChecked.Append(false).ToArray();
                    }
                }
                for(int i = 0; i < flags.Length; i++)
                {
                    if (!flagChecked[i])
                    {
                        flagChecked[i] = true;
                        string req = flags[i];
                        route = MakeRoute(Flags.FlagNames.IndexOf(req), route, flags, flagChecked);
                    }
                }
                return route;

            }
            else
            {
                // Item not placed
                return Array.Empty<string>();
            }
            throw new NotImplementedException();
        }

        public static List<Location> MakeChestLocations()
        {
            List<Location> list = new List<Location>();
            int[] chestCounts = RandomizerSystem.chestsanityMaxCounts;
            for(int chestType = 0; chestType < chestCounts.Length; chestType++)
            {
                for(int i = 0; i < chestCounts[chestType]; i++)
                {
                    Location location = RandomizerUtils.MakeChestLocation(chestType, i);
                    list.Add(location);
                }
            }
            return list;
        }

        public static void RemoveSelected()
        {
            foreach(Reward reward in rewards)
            {
                Location location = reward.location;
                allLocations.Remove(location);
            }
        }
    }
}
