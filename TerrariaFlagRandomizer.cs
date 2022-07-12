using System;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using Terraria;
using Terraria.ModLoader;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Packets;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Models;
using Archipelago.MultiClient.Net.Helpers;
using TerrariaFlagRandomizer.Common;
using TerrariaFlagRandomizer.Common.Helpers;
using TerrariaFlagRandomizer.Common.Archipelago;

namespace TerrariaFlagRandomizer
{
	public class TerrariaFlagRandomizer : Mod
	{
        //public const string AssetPath = $"{nameof(TerrariaFlagRandomizer)}/Assets/";
        public static TerrariaFlagRandomizer instance;
        public static ArchipelagoSession session;
        public static bool isArchipelago = false;

        public override void Load()
        {
            // IL hooks
            IL.Terraria.NPC.DoDeathEvents += RemoveWOF;
            IL.Terraria.NPC.DoDeathEvents += RemoveDestroyer;
            IL.Terraria.NPC.DoDeathEvents += RemovePlant;
            IL.Terraria.NPC.DoDeathEvents += RemoveSkeletron;
            IL.Terraria.NPC.DoDeathEvents += RemovePrime;
            IL.Terraria.NPC.DoDeathEvents += RemoveTwins;
            IL.Terraria.NPC.DoDeathEvents += RemoveGolem;

            // Archipelago hooks
            On.Terraria.WorldGen.SaveAndQuit += OnSaveAndQuit;

            isArchipelago = false;
        }

        public override void Unload()
        {
            // IL hooks
            IL.Terraria.NPC.DoDeathEvents -= RemoveWOF;
            IL.Terraria.NPC.DoDeathEvents -= RemoveDestroyer;
            IL.Terraria.NPC.DoDeathEvents -= RemovePlant;
            IL.Terraria.NPC.DoDeathEvents -= RemoveSkeletron;
            IL.Terraria.NPC.DoDeathEvents -= RemovePrime;
            IL.Terraria.NPC.DoDeathEvents -= RemoveTwins;
            IL.Terraria.NPC.DoDeathEvents -= RemoveGolem;

            // Archipelago hooks
            On.Terraria.WorldGen.SaveAndQuit -= OnSaveAndQuit;

            isArchipelago = false;
        }

        public static void OnSaveAndQuit(On.Terraria.WorldGen.orig_SaveAndQuit orig, Action callback)
        {
            orig(callback);
            DisconnectFromServer();
        }

        private static void RemoveWOF(ILContext il)
        {
            var c = new ILCursor(il);
            // Find the first first public method call that handles the aftermath of its defeat. In this case, it's StartHardmode
            if (!c.TryGotoNext(i => i.MatchCall<WorldGen>("StartHardmode"))) // Starts at IL_0941
            {
                throw new Exception("Hook location not found: call Terraria.NPC::StartHardmode");

            }
            // Remove the method call. Since this method requires no arguments, we don't need to add any Pop instructions
            c.Remove();

            // The Wall of Flesh can also display some extra text on defeat, in case the player defeats the Mechanical Bosses before it
            // so we need to remove that as well
            // First, we look for the first instruction that handles this (in this case, loading the text to be displayed)
            if (!c.TryGotoNext(i => i.MatchLdsfld<Lang>("misc")))
            {
                throw new Exception("Instruction not found: ldsfld Terraria.Lang::misc (NewText variant, WoF)");
            }
            // Then we remove the block of code
            c.RemoveRange(8);

            // However, this code is also present in another block, due to a different method being used if it's in multiplayer
            // So we move the cursor to the first instruction of that block, using the same method as before
            if (!c.TryGotoNext(i => i.MatchLdsfld<Lang>("misc")))
            {
                throw new Exception("Instruction not found: ldsfld Terraria.Lang::misc (NetworkText variant, Plantera)");
            }
            // Then we do the same thing as before with this block of code
            c.RemoveRange(12);

            // Finally, it calls the SetEventFlagCleared method again, so we remove that
            // First, we find the method call
            if (!c.TryGotoNext(i => i.MatchCall<NPC>("SetEventFlagCleared")))
            {
                throw new Exception("Instruction not found: call Terraria.NPC::SetEventFlagCleared (WoF)");
            }
            // And then we delete it
            c.Remove();
            // Since the arguments were already loaded into the stack, we also add two Pop instructions to remove them from the stack, to prevent any errors
            // We can't stop the arguments from being loaded, because other parts of the code jump to one of the load instructions, and removing it would cause IL to reject our code
            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Pop);
        }

        private static void RemoveTwins(ILContext il)
        {
            var c = new ILCursor(il);
            if (!c.TryGotoNext(i => i.MatchLdsflda<NPC>("downedMechBoss2"))) // Starts at IL_0913
            {
                throw new Exception("Hook location not found: ldsflda downedMechBoss2");
            }
            c.GotoNext();
            c.GotoNext();
            c.Remove();
            c.Remove();
            c.Remove();
            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Pop);
        }

        private static void RemovePrime(ILContext il)
        {
            var c = new ILCursor(il);
            if (!c.TryGotoNext(i => i.MatchLdsflda<NPC>("downedMechBoss3"))) // Starts at IL_08F1
            {
                throw new Exception("Hook location not found: ldsflda downedMechBoss3");
            }
            c.GotoNext();
            c.GotoNext();
            c.Remove();
            c.Remove();
            c.Remove();
            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Pop);
        }

        private static void RemoveSkeletron(ILContext il)
        {
            var c = new ILCursor(il);
            if (!c.TryGotoNext(i => i.MatchLdsflda<NPC>("downedBoss3"))) // Starts at IL_08D5
            {
                throw new Exception("Hook location not found: ldsflda downedBoss3");
            }
            c.GotoNext();
            c.GotoNext();
            c.Remove();
            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Pop);
        }

        private static void RemovePlant(ILContext il)
        {
            var c = new ILCursor(il);
            if (!c.TryGotoNext(i => i.MatchLdsfld<NPC>("downedPlantBoss"))) // Finds the instruction that loads the downedPlantBoss variable, for the code that handles its event (Starts at IL_0804)
            {
                throw new Exception("Hook location not found: ldsfld downedPlantBoss");
            }
            // Moves the cursor cursor down to the SetEventFlagCleared method call
            // (Note that the cursor is above the instructions that load the method's arguments, so we need to move down one further instruction)
            c.GotoNext();
            c.GotoNext();
            c.GotoNext();
            // Removes the method call
            c.Remove();
            // However, the arguments for the now removed method call were already loaded into the stack, so to prevent errors, we add two Pop instructions, to remove them
            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Pop);

            // Plantera also has some code to display some text on the first kill, so we also remove that
            // First, we move down to the first instruction that handles this, a "load field" instruction for the text, which is in Lang.misc
            if (!c.TryGotoNext(i => i.MatchLdsfld<Lang>("misc")))
            {
                throw new Exception("Instruction not found: ldsfld Terraria.Lang::misc (NewText variant, Plantera)");
            }
            // Then we remove the whole block of code with RemoveRange
            // We could also just use Remove to delete each instruction individually, but this is cleaner
            c.RemoveRange(8);

            // However, this code is also present in another block, due to a different method being used if it's in multiplayer
            // So we move the cursor to the first instruction of that block, using the same method as before
            if (!c.TryGotoNext(i => i.MatchLdsfld<Lang>("misc")))
            {
                throw new Exception("Instruction not found: ldsfld Terraria.Lang::misc (NetworkText variant, Plantera)");
            }
            // Then we do the same thing as before with this block of code
            c.RemoveRange(12);
        }

        private void RemoveGolem(ILContext il)
        {
            var c = new ILCursor(il);
            if (!c.TryGotoNext(i => i.MatchLdsflda<NPC>("downedGolemBoss"))) // Starts at IL_0618
            {
                throw new Exception("Hook location not found: ldsflda downedGolemBoss");
            }
            c.GotoNext();
            c.GotoNext();
            c.Remove();
            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Pop);
        }

        private void RemoveDestroyer(ILContext il)
        {
            var c = new ILCursor(il);
            if (!c.TryGotoNext(i => i.MatchLdsflda<NPC>("downedMechBoss1"))) // Finds the instruction that loads downedMechBoss1 as an argument for SetEventFlagCleared (Starts at IL_07ED)
            {
                throw new Exception("Hook location not found: ldsflda downedMechBoss1");
            }
            // Moves the cursor down to the SetEventFlagCleared call
            c.GotoNext();
            c.GotoNext();
            // Removes the call, plus the next few instructions that set downedMechBossAny to true
            c.Remove();
            c.Remove();
            c.Remove();
            // However, the arguments for the now removed method call were already loaded into the stack, so to prevent errors, we add two Pop instructions, to remove them
            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Pop);
        }

        public static void ConnectToServer(string input, string[] args)
        {
            if(args.Length != 3)
            {
                RandomizerUtils.SendText("Not enough arguments", 255, 0, 0);
                return;
            }
            if(session != null && session.Socket.Connected)
            {
                RandomizerUtils.SendText("Already connected to server", 255, 0, 0);
                return;
            }
            int port = Int32.Parse(args[2]);
            bool retry = true;
            while (retry)
            {
                try
                {
                    session = ArchipelagoSessionFactory.CreateSession(args[1], port);
                    LoginResult result = session.TryConnectAndLogin("Terraria", args[0], new Version(0, 3, 2), ItemsHandlingFlags.IncludeStartingInventory);
                    if (!result.Successful)
                    {
                        RandomizerUtils.SendText(result.ToString());
                        return;
                    }
                    retry = false;
                }
                catch (PlatformNotSupportedException)
                {

                } catch(Exception e)
                {
                    RandomizerUtils.SendText("Error connecting to Archipelago server: " + e.Message, 255, 0, 0);
                }
            }
            On.Terraria.Chat.ChatCommandProcessor.ProcessIncomingMessage += OnTerrariaChatMessage;
            session.Socket.PacketReceived += OnPacketReceived;
            ArchipelagoHelper.CompleteLocationChecks();
            RandomizerUtils.SendText("Connected to Archipelago server");
            isArchipelago = true;
        }

        public static void DisconnectFromServer()
        {
            if(session == null || !session.Socket.Connected)
            {
                RandomizerUtils.SendText("Not connected to Archipelago server", 255, 0, 0);
                return;
            }
            On.Terraria.Chat.ChatCommandProcessor.ProcessIncomingMessage -= OnTerrariaChatMessage;
            session.Socket.PacketReceived -= OnPacketReceived;
            session.Socket.Disconnect();
            RandomizerUtils.SendText("Disconnected from Archipelago server");
            isArchipelago = false;
        }

        public static void OnTerrariaChatMessage(On.Terraria.Chat.ChatCommandProcessor.orig_ProcessIncomingMessage orig, Terraria.Chat.ChatCommandProcessor self, Terraria.Chat.ChatMessage message, int clientID)
        {
            orig(self, message, clientID);
            session.Socket.SendPacket(new SayPacket() { Text = message.Text });
        }

        public static void OnPacketReceived(ArchipelagoPacketBase packet)
        {
            ArchipelagoPacketType type = packet.PacketType;
            if(type == ArchipelagoPacketType.Print)
            {
                PrintPacket received = (PrintPacket)packet;
                if (!received.Text.Contains("Space:"))
                {
                    RandomizerUtils.SendText(received.Text);
                }
                return;
            }
            if(type == ArchipelagoPacketType.RoomUpdate)
            {
                RoomUpdatePacket received = (RoomUpdatePacket)packet;
                return;
            }
            if(type == ArchipelagoPacketType.PrintJSON)
            {
                ItemPrintJsonPacket received = (ItemPrintJsonPacket)packet;
                int playerID = Int32.Parse(received.Data[0].Text);
                long itemID = Int64.Parse(received.Data[2].Text);
                long locationID = Int64.Parse(received.Data[4].Text);
                string text = session.Players.GetPlayerAlias(playerID) + received.Data[1].Text;
                text += session.Items.GetItemName(itemID) + received.Data[3].Text;
                text += session.Locations.GetLocationNameFromId(locationID) + received.Data[5].Text;
                RandomizerUtils.SendText(text);
                if(session.ConnectionInfo.Slot != received.ReceivingPlayer) return;
                int archipelagoItemID = received.Item.Item;
                RewardsHandler.ReceiveArchipelagoItem(archipelagoItemID);
                //ArchipelagoItemManager.ArchipelagoItemReceived(archipelagoItemID);
                return;
            }
            RandomizerUtils.SendText("Received unhandled packet type " + type, 255, 255, 0);
        }
    }
}