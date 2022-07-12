using System.Collections.Generic;
using TerrariaFlagRandomizer.Common.Helpers;
using TerrariaFlagRandomizer.Common.Sets;

namespace TerrariaFlagRandomizer.Common.Archipelago
{
    internal class ArchipelagoHelper
    {
        public static List<string> locationsCompleted = new List<string>();
        public static void CompleteLocationChecks()
        {
            if(TerrariaFlagRandomizer.session == null || !TerrariaFlagRandomizer.session.Socket.Connected)
            {
                return;
            }
            foreach(string location in locationsCompleted)
            {
                RandomizerUtils.SendText(location + " checked");
                if(ArchipelagoSets.LocationToArchipelagoID[location] == 7777422)
                {
                    // Complete all locations
                    foreach(int reward in ArchipelagoSets.ArchipelagoToLocationID.Values)
                    {
                        RewardsHandler.SpawnReward(reward);
                    }
                    return;
                }
                TerrariaFlagRandomizer.session.Locations.CompleteLocationChecks(ArchipelagoSets.LocationToArchipelagoID[location]);
            }
        }
    }
}
