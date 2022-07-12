using System;
using System.Linq;
using System.Collections.Generic;
using TerrariaFlagRandomizer.Common.Sets;

namespace TerrariaFlagRandomizer.Common.Helpers
{
    public class LocationsHelper
    {
        // TODO: Methods to remove checks based on type, or individual checks (based on name)
        // As well as methods to check if a check is accessible
        public static List<Location> locations;
        public static List<string> inaccessible /*= new List<string>() { "Skeletron", "Hardmode", "MechBosses", "PlantBoss", "GolemBoss" }*/;
        public static List<Reward> rewards/* = new List<Reward>()
        // Test data
        {
            new Reward(1, "Skeletron", LocationSets.BossLocations[12]),
            new Reward(3, "MechBosses", LocationSets.MinibossLocations[2]),
            new Reward(2, "Hardmode", LocationSets.BossLocations[5])
        }*/;

        public static void Initialize()
        {
            locations = LocationSets.GetAllLocations();
            inaccessible = new List<string>() { "Skeletron", "Hardmode", "MechBosses", "PlantBoss", "GolemBoss" };
            rewards = new List<Reward>();
        }

        public static void ResetLocations()
        {
            locations.Clear();
            locations = LocationSets.GetAllLocations();
        }

        public static void RemoveInacessible(List<string> requirements)
        {
            if (locations == null) Initialize();
            for(int i = 0; i < locations.Count; i++)
            {
                Location location = locations[i];
                if (requirements.Intersect(location.requirements).Any())
                {
                    locations.RemoveAt(i);
                    i--;
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
        }
    }
}
