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
        public static Dictionary<string, bool> locationsSent = new Dictionary<string, bool>();
        public static void CompleteLocationChecks()
        {
            if(TerrariaFlagRandomizer.session == null || !TerrariaFlagRandomizer.session.Socket.Connected)
            {
                return;
            }
            foreach(string location in locationsCompleted)
            {
                /* Performance fix: don't try to send items that were already sent.
                 * If you lose connection then reconnect, the client tries to send every checked
                 * location every time there's a new check, causing the game to hang for a
                 * non-trivial amount of time.
                 */
                if (locationsSent.ContainsKey(location)) continue;
                //RandomizerUtils.SendText(location + " checked");
                //if(ArchipelagoSets.LocationToArchipelagoID[location] == 7777422)
                if(location == "MoonLordReward")
                {
                    // Feature adjustment: send goal completion to archipelago server (let the server handle forfeiting items instead of the client)
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
                locationsSent.Add(location, true);
            }
        }
    }
}
