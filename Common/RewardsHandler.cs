using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.ModLoader;
using TerrariaFlagRandomizer.Content.Items;
using TerrariaFlagRandomizer.Common.Sets;
using TerrariaFlagRandomizer.Common.Systems;

namespace TerrariaFlagRandomizer.Common
{
    internal class RewardsHandler
    {
        public static void SpawnReward(NPC npc, int type)
        {
            string location = LocationSets.CheckToLocation[type];
            int reward = RandomizerSystem.locationRewardPairs[location];
            if (reward == 0) SpawnLootBagOnNPC(npc);
            else if(reward == 6)
            {
                SetFlag(++RandomizerSystem.progressiveTier, npc.boss);
            }
            else SetFlag(reward, npc.boss);
        }

        public static void SpawnRewardGeneric(int type)
        {
            if (type == 0) SpawnLootBagOnPlayers();
            else if (type == 6) SetFlag(++RandomizerSystem.progressiveTier, false);
            else SetFlag(type, false);
        }

        public static void SpawnLootBagOnNPC(NPC npc)
        {
            var source = npc.GetSource_Loot();
            int type;
            if (NPC.downedGolemBoss) type = ModContent.ItemType<LootBag6>();
            else if (NPC.downedPlantBoss) type = ModContent.ItemType<LootBag5>();
            else if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3) type = ModContent.ItemType<LootBag4>();
            else if (Main.hardMode) type = ModContent.ItemType<LootBag3>();
            else if (NPC.downedBoss3) type = ModContent.ItemType<LootBag2>();
            else type = ModContent.ItemType<LootBag1>();
            npc.DropItemInstanced(npc.position, npc.Size, type, interactionRequired: false);
        }

        public static void SpawnLootBagOnPlayers()
        {
            //var source = npc.GetSource_Loot();
            int type;
            if (NPC.downedGolemBoss) type = ModContent.ItemType<LootBag6>();
            else if (NPC.downedPlantBoss) type = ModContent.ItemType<LootBag5>();
            else if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3) type = ModContent.ItemType<LootBag4>();
            else if (Main.hardMode) type = ModContent.ItemType<LootBag3>();
            else if (NPC.downedBoss3) type = ModContent.ItemType<LootBag2>();
            else type = ModContent.ItemType<LootBag1>();
            foreach(Player player in Main.player)
            {
                if (!player.active/* || player.dead*/) continue;
                var source = player.GetSource_GiftOrReward();
                player.QuickSpawnItem(source, type);
            }
            //Item.NewItem(source, (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, type);
        }

        public static void TriggerHardmode(bool fromBoss = false)
        {
            bool hardmodeFlag = Main.hardMode;
            WorldGen.StartHardmode();
            if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && !hardmodeFlag)
            {
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Main.NewText(Lang.misc[32].Value, 50, byte.MaxValue, 130);
                }
                else if (Main.netMode == NetmodeID.Server)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.misc[32].Key), new Color(50, 255, 130));
                }
            }

            if(NPC.downedPlantBoss && !hardmodeFlag)
            {
                if(Main.netMode == NetmodeID.SinglePlayer)
                {
                    Main.NewText(Lang.misc[33].Value, 50, byte.MaxValue, 130);
                } else if(Main.netMode == NetmodeID.Server)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.misc[33].Key), new Color(50, 255, 130));
                }
            }

            if(NPC.downedBoss3 && NPC.downedGolemBoss && !hardmodeFlag)
            {
                if(Main.netMode == NetmodeID.SinglePlayer)
                {
                    Main.NewText("Cultists arrive at the dungeon!", 50, 255, 130);
                } else if(Main.netMode == NetmodeID.Server)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Cultists arrive at the dungeon!"), new Color(50, 255, 130));
                }
            }

            NPC.SetEventFlagCleared(ref hardmodeFlag, 19);
            if (!fromBoss && Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendData(MessageID.WorldData);
            }

            foreach(Player p in Main.player)
            {
                if (p.dead || !p.active) continue;
                var source = p.GetSource_GiftOrReward();
                int type;
                if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                {
                    type = Main.rand.NextBool(2) ? ItemID.PickaxeAxe : ItemID.Drax;
                }
                else type = ItemID.MoltenPickaxe;
                p.QuickSpawnItem(source, type);
                p.QuickSpawnItem(source, ItemID.Pwnhammer);
            }
        }

        public static void SetFlag(int flagID, bool fromBoss = false)
        {
            switch (flagID)
            {
                case Flags.SkeletronFlag:
                    bool skeletronFlag = NPC.downedBoss3;
                    NPC.SetEventFlagCleared(ref NPC.downedBoss3, 15);

                    if(Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Main.NewText("Skeletron's curse has dissipated!", 50, 255, 130);
                    } else if(Main.netMode == NetmodeID.Server)
                    {
                        ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Skeletron's curse has dissipated!"), new Color(50, 255, 130));
                    }

                    if(Main.hardMode && NPC.downedGolemBoss && !skeletronFlag)
                    {
                        if(Main.netMode == NetmodeID.SinglePlayer)
                        {
                            Main.NewText("Cultists arrive at the dungeon!", 50, 255, 130);
                        } else if(Main.netMode == NetmodeID.Server)
                        {
                            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Cultists arrive at the dungeon!"), new Color(50, 255, 130));
                        }
                    }

                    if (!fromBoss && Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendData(MessageID.WorldData);
                    }

                    break;
                case Flags.HardmodeFlag:
                    TriggerHardmode(fromBoss);
                    break;
                case Flags.MechsFlag:
                    bool mechFlag1 = NPC.downedMechBoss1;
                    bool mechFlag2 = NPC.downedMechBoss2;
                    bool mechFlag3 = NPC.downedMechBoss3;
                    NPC.SetEventFlagCleared(ref NPC.downedMechBoss1, 16);
                    NPC.SetEventFlagCleared(ref NPC.downedMechBoss2, 17);
                    NPC.SetEventFlagCleared(ref NPC.downedMechBoss3, 18);
                    NPC.downedMechBossAny = true;

                    if(Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Main.NewText("The Mechanical Bosses' souls leave this world!", 50, 255, 130);
                    } else if(Main.netMode == NetmodeID.Server)
                    {
                        ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("The Mechanical Bosses' sould leave this world!"), new Color(20, 255, 130));
                    }

                    if (Main.hardMode && !mechFlag1 && !mechFlag2 && !mechFlag3)
                    {
                        if (Main.netMode == NetmodeID.SinglePlayer)
                        {
                            Main.NewText(Lang.misc[32].Value, 50, byte.MaxValue, 130);
                        }
                        else if (Main.netMode == NetmodeID.Server)
                        {
                            ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.misc[32].Key), new Color(50, 255, 130));
                        }
                        foreach (Player p in Main.player)
                        {
                            if (p.dead || !p.active) continue;
                            var source = p.GetSource_GiftOrReward();
                            int type = Main.rand.NextBool(2) ? ItemID.PickaxeAxe : ItemID.Drax;
                            p.QuickSpawnItem(source, type);
                        }
                    }

                    if(!fromBoss && Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendData(MessageID.WorldData);
                    }
                    break;
                case Flags.PlanteraFlag:
                    bool planteraFlag = NPC.downedPlantBoss;
                    NPC.SetEventFlagCleared(ref NPC.downedPlantBoss, 12);

                    if(Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Main.NewText("Plantera's power dissipates!", 50, 255, 130);
                    } else if(Main.netMode == NetmodeID.Server)
                    {
                        ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Plantera's power dissipates!"), new Color(50, 255, 130));
                    }

                    if (Main.hardMode)
                    {
                        if(Main.netMode == NetmodeID.SinglePlayer)
                        {
                            Main.NewText(Lang.misc[33].Value, 50, 255, 130);
                        } else if(Main.netMode == NetmodeID.Server)
                        {
                            ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.misc[33].Key), new Color(50, 255, 130));
                        }
                    }

                    foreach(Player p in Main.player)
                    {
                        if (p.dead || !p.active) continue;
                        var source = p.GetSource_GiftOrReward();
                        p.QuickSpawnItem(source, ItemID.TempleKey);
                    }

                    if(!fromBoss && Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendData(MessageID.WorldData);
                    }
                    break;
                case Flags.GolemFlag:
                    bool golemFlag = NPC.downedGolemBoss;
                    NPC.SetEventFlagCleared(ref NPC.downedGolemBoss, 6);

                    if(Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Main.NewText("The Lihzahrd's guardian falls silent...", 50, 255, 130);
                    } else if(Main.netMode == NetmodeID.Server)
                    {
                        ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("The Lihzahrd's guardian falls silent..."), new Color(50, 255, 130));
                    }

                    if(Main.hardMode && NPC.downedBoss3 && !golemFlag)
                    {
                        if(Main.netMode == NetmodeID.SinglePlayer)
                        {
                            Main.NewText("Cultists arrive at the dungeon!", 50, 255, 130);
                        } else if(Main.netMode == NetmodeID.Server)
                        {
                            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Cultists arrive at the dungeon!"), new Color(50, 255, 130));
                        }
                    }

                    if(!fromBoss && Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendData(MessageID.WorldData);
                    }
                    break;
            }
        }
    }
}
