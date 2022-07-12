using System.Collections.Generic;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Packets;
using Archipelago.MultiClient.Net.Exceptions;
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
                //if(ArchipelagoSets.LocationToArchipelagoID[location] == 7777422)
                if(location == "MoonLordReward")
                {
                    // Send goal completion to archipelago server
                    try
                    {
                        StatusUpdatePacket packet = new StatusUpdatePacket();
                        packet.Status = ArchipelagoClientState.ClientGoal;
                        TerrariaFlagRandomizer.session.Socket.SendPacket(packet);
                    } catch(ArchipelagoSocketClosedException e)
                    {
                        RandomizerUtils.SendText("Failed to send goal: " + e.Message, 255, 0, 0);
                        RandomizerUtils.SendText("Reconnect to try again", 255, 0, 0);
                        return;
                    }
                    // Complete all locations
                    /*foreach(int reward in ArchipelagoSets.ArchipelagoToLocationID.Values)
                    {
                        RewardsHandler.SpawnReward(reward);
                    }
                    return;*/
                }
                try
                {
                    TerrariaFlagRandomizer.session.Locations.CompleteLocationChecks(ArchipelagoSets.LocationToArchipelagoID[location]);
                }
                catch(ArchipelagoSocketClosedException e)
                {
                    RandomizerUtils.SendText("Failed to send checks: " + e.Message, 255, 0, 0);
                    RandomizerUtils.SendText("Reconnect to try again", 255, 0, 0);
                    return;
                }
                locationsCompleted.Remove(location);
            }
        }
    }
}
