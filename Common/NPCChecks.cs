using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaFlagRandomizer.Common.Systems;
using TerrariaFlagRandomizer.Common.Sets;
using TerrariaFlagRandomizer.Common.Archipelago;

namespace TerrariaFlagRandomizer.Common
{
    internal class NPCChecks : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            switch (npc.type)
            {
                case NPCID.KingSlime:
                    if (!NPC.downedSlimeKing)
                    {
                        if (TerrariaFlagRandomizer.isArchipelago)
                        {
                            string locationName = LocationSets.CheckToLocation[0];
                            ArchipelagoHelper.locationsCompleted.Add(locationName);
                            ArchipelagoHelper.CompleteLocationChecks();
                        }
                        else RewardsHandler.SpawnReward(0, npc);
                    }
                    break;
                case NPCID.EyeofCthulhu:
                    if (!NPC.downedBoss1)
                    {
                        if (TerrariaFlagRandomizer.isArchipelago)
                        {
                            string locationName = LocationSets.CheckToLocation[1];
                            ArchipelagoHelper.locationsCompleted.Add(locationName);
                            ArchipelagoHelper.CompleteLocationChecks();
                        }
                        else RewardsHandler.SpawnReward(1, npc);
                    }
                    break;
                case NPCID.EaterofWorldsHead:
                case NPCID.EaterofWorldsBody:
                case NPCID.EaterofWorldsTail:
                    if(npc.boss) {
                        if (!NPCCheckSystem.firstEaterKill)
                        {
                            if (TerrariaFlagRandomizer.isArchipelago)
                            {
                                string locationName = LocationSets.CheckToLocation[2];
                                ArchipelagoHelper.locationsCompleted.Add(locationName);
                                ArchipelagoHelper.CompleteLocationChecks();
                            }
                            else RewardsHandler.SpawnReward(2, npc);
                            NPCCheckSystem.firstEaterKill = true;
                        }
                    }
                    break;
                case NPCID.BrainofCthulhu:
                    if (!NPCCheckSystem.firstBrainKill)
                    {
                        if (TerrariaFlagRandomizer.isArchipelago)
                        {
                            string locationName = LocationSets.CheckToLocation[3];
                            ArchipelagoHelper.locationsCompleted.Add(locationName);
                            ArchipelagoHelper.CompleteLocationChecks();
                        }
                        else RewardsHandler.SpawnReward(3, npc);
                        NPCCheckSystem.firstBrainKill = true;
                    }
                    break;
                case NPCID.SkeletronHead:
                    if (!NPCCheckSystem.firstSkeletronKill)
                    {
                        if (TerrariaFlagRandomizer.isArchipelago)
                        {
                            string locationName = LocationSets.CheckToLocation[4];
                            ArchipelagoHelper.locationsCompleted.Add(locationName);
                            ArchipelagoHelper.CompleteLocationChecks();
                        }
                        else RewardsHandler.SpawnReward(4, npc);
                        NPCCheckSystem.firstSkeletronKill = true;
                    }
                    break;
                case NPCID.QueenBee:
                    if (!NPC.downedQueenBee)
                    {
                        if (TerrariaFlagRandomizer.isArchipelago)
                        {
                            string locationName = LocationSets.CheckToLocation[5];
                            ArchipelagoHelper.locationsCompleted.Add(locationName);
                            ArchipelagoHelper.CompleteLocationChecks();
                        }
                        else RewardsHandler.SpawnReward(5, npc);
                    }
                    break;
                case NPCID.Deerclops:
                    if (!NPC.downedDeerclops)
                    {
                        if (TerrariaFlagRandomizer.isArchipelago)
                        {
                            string locationName = LocationSets.CheckToLocation[6];
                            ArchipelagoHelper.locationsCompleted.Add(locationName);
                            ArchipelagoHelper.CompleteLocationChecks();
                        }
                        else RewardsHandler.SpawnReward(6, npc);
                    }
                    break;
                case NPCID.WallofFlesh:
                    if (!NPCCheckSystem.firstWOFKill)
                    {
                        if (TerrariaFlagRandomizer.isArchipelago)
                        {
                            string locationName = LocationSets.CheckToLocation[7];
                            ArchipelagoHelper.locationsCompleted.Add(locationName);
                            ArchipelagoHelper.CompleteLocationChecks();
                        }
                        else RewardsHandler.SpawnReward(7, npc);
                        NPCCheckSystem.firstWOFKill = true;
                    }
                    break;
                case NPCID.QueenSlimeBoss:
                    if (!NPC.downedQueenSlime)
                    {
                        if (TerrariaFlagRandomizer.isArchipelago)
                        {
                            string locationName = LocationSets.CheckToLocation[8];
                            ArchipelagoHelper.locationsCompleted.Add(locationName);
                            ArchipelagoHelper.CompleteLocationChecks();
                        }
                        else RewardsHandler.SpawnReward(8, npc);
                    }
                    break;
                case NPCID.Retinazer:
                case NPCID.Spazmatism:
                    if(!NPC.AnyNPCs((npc.type == NPCID.Retinazer) ? NPCID.Spazmatism : NPCID.Retinazer))
                    {
                        if (!NPCCheckSystem.firstTwinsKill)
                        {
                            if (TerrariaFlagRandomizer.isArchipelago)
                            {
                                string locationName = LocationSets.CheckToLocation[9];
                                ArchipelagoHelper.locationsCompleted.Add(locationName);
                                ArchipelagoHelper.CompleteLocationChecks();
                            }
                            else RewardsHandler.SpawnReward(9, npc);
                            NPCCheckSystem.firstTwinsKill = true;
                        }
                    }
                    break;
                case NPCID.TheDestroyer:
                    if (!NPCCheckSystem.firstDestroyerKill)
                    {
                        if (TerrariaFlagRandomizer.isArchipelago)
                        {
                            string locationName = LocationSets.CheckToLocation[10];
                            ArchipelagoHelper.locationsCompleted.Add(locationName);
                            ArchipelagoHelper.CompleteLocationChecks();
                        }
                        else RewardsHandler.SpawnReward(10, npc);
                        NPCCheckSystem.firstDestroyerKill = true;
                    }
                    break;
                case NPCID.SkeletronPrime:
                    if (!NPCCheckSystem.firstPrimeKill)
                    {
                        if (TerrariaFlagRandomizer.isArchipelago)
                        {
                            string locationName = LocationSets.CheckToLocation[11];
                            ArchipelagoHelper.locationsCompleted.Add(locationName);
                            ArchipelagoHelper.CompleteLocationChecks();
                        }
                        else RewardsHandler.SpawnReward(11, npc);
                        NPCCheckSystem.firstPrimeKill = true;
                    }
                    break;
                case NPCID.Plantera:
                    if (!NPCCheckSystem.firstPlantKill)
                    {
                        if (TerrariaFlagRandomizer.isArchipelago)
                        {
                            string locationName = LocationSets.CheckToLocation[12];
                            ArchipelagoHelper.locationsCompleted.Add(locationName);
                            ArchipelagoHelper.CompleteLocationChecks();
                        }
                        else RewardsHandler.SpawnReward(12, npc);
                        NPCCheckSystem.firstPlantKill = true;
                    }
                    break;
                case NPCID.Golem:
                    if (!NPCCheckSystem.firstGolemKill)
                    {
                        if (TerrariaFlagRandomizer.isArchipelago)
                        {
                            string locationName = LocationSets.CheckToLocation[13];
                            ArchipelagoHelper.locationsCompleted.Add(locationName);
                            ArchipelagoHelper.CompleteLocationChecks();
                        }
                        else RewardsHandler.SpawnReward(13, npc);
                        NPCCheckSystem.firstGolemKill = true;
                    }
                    break;
                case NPCID.DukeFishron:
                    if (!NPC.downedFishron)
                    {
                        if (TerrariaFlagRandomizer.isArchipelago)
                        {
                            string locationName = LocationSets.CheckToLocation[14];
                            ArchipelagoHelper.locationsCompleted.Add(locationName);
                            ArchipelagoHelper.CompleteLocationChecks();
                        }
                        else RewardsHandler.SpawnReward(14, npc);
                    }
                    break;
                case NPCID.HallowBoss:
                    if (!NPC.downedEmpressOfLight)
                    {
                        if (TerrariaFlagRandomizer.isArchipelago)
                        {
                            string locationName = LocationSets.CheckToLocation[15];
                            ArchipelagoHelper.locationsCompleted.Add(locationName);
                            ArchipelagoHelper.CompleteLocationChecks();
                        }
                        else RewardsHandler.SpawnReward(15, npc);
                    }
                    break;
                case NPCID.CultistBoss:
                    if (!NPC.downedAncientCultist)
                    {
                        if (TerrariaFlagRandomizer.isArchipelago)
                        {
                            string locationName = LocationSets.CheckToLocation[16];
                            ArchipelagoHelper.locationsCompleted.Add(locationName);
                            ArchipelagoHelper.CompleteLocationChecks();
                        }
                        else RewardsHandler.SpawnReward(16, npc);
                    }
                    break;
                case NPCID.MoonLordCore:
                    Main.NewText("Moon Lord defeated");
                    if (!NPC.downedMoonlord)
                    {
                        Main.NewText("First Moon Lord kill");
                        if (TerrariaFlagRandomizer.isArchipelago)
                        {
                            string locationName = LocationSets.CheckToLocation[26];
                            ArchipelagoHelper.locationsCompleted.Add(locationName);
                            ArchipelagoHelper.CompleteLocationChecks();
                        }
                        else RewardsHandler.SpawnReward(26, npc);
                    }
                    break;
                case NPCID.Mothron:
                    if (!NPCCheckSystem.firstMothronKill)
                    {
                        if (TerrariaFlagRandomizer.isArchipelago)
                        {
                            string locationName = LocationSets.CheckToLocation[22];
                            ArchipelagoHelper.locationsCompleted.Add(locationName);
                            ArchipelagoHelper.CompleteLocationChecks();
                        }
                        else RewardsHandler.SpawnReward(22, npc);
                        NPCCheckSystem.firstMothronKill = true;
                        if(Main.netMode == NetmodeID.Server)
                        {
                            NetMessage.SendData(MessageID.WorldData);
                        }
                    }
                    break;
                case NPCID.BigMimicHallow:
                    if (!NPCCheckSystem.firstHallowMimicKill)
                    {
                        if (TerrariaFlagRandomizer.isArchipelago)
                        {
                            string locationName = LocationSets.CheckToLocation[23];
                            ArchipelagoHelper.locationsCompleted.Add(locationName);
                            ArchipelagoHelper.CompleteLocationChecks();
                        }
                        else RewardsHandler.SpawnReward(23, npc);
                        NPCCheckSystem.firstHallowMimicKill = true;
                        if(Main.netMode == NetmodeID.Server)
                        {
                            NetMessage.SendData(MessageID.WorldData);
                        }
                    }
                    break;
                case NPCID.BigMimicCorruption:
                    if (!NPCCheckSystem.firstCorruptMimicKill)
                    {
                        if (TerrariaFlagRandomizer.isArchipelago)
                        {
                            string locationName = LocationSets.CheckToLocation[24];
                            ArchipelagoHelper.locationsCompleted.Add(locationName);
                            ArchipelagoHelper.CompleteLocationChecks();
                        }
                        else RewardsHandler.SpawnReward(24, npc);
                        NPCCheckSystem.firstCorruptMimicKill = true;
                        if(Main.netMode == NetmodeID.Server)
                        {
                            NetMessage.SendData(MessageID.WorldData);
                        }
                    }
                    break;
                case NPCID.BigMimicCrimson:
                    if (!NPCCheckSystem.firstCrimsonMimicKill){
                        if (TerrariaFlagRandomizer.isArchipelago)
                        {
                            string locationName = LocationSets.CheckToLocation[25];
                            ArchipelagoHelper.locationsCompleted.Add(locationName);
                            ArchipelagoHelper.CompleteLocationChecks();
                        }
                        else RewardsHandler.SpawnReward(25, npc);
                        NPCCheckSystem.firstCrimsonMimicKill = true;
                        if (Main.netMode == NetmodeID.Server)
                        {
                            NetMessage.SendData(MessageID.WorldData);
                        }
                    }
                    break;
                case NPCID.MourningWood:
                    if (Main.pumpkinMoon && !NPC.downedHalloweenTree)
                    {
                        if (TerrariaFlagRandomizer.isArchipelago)
                        {
                            string locationName = LocationSets.CheckToLocation[17];
                            ArchipelagoHelper.locationsCompleted.Add(locationName);
                            ArchipelagoHelper.CompleteLocationChecks();
                        }
                        else RewardsHandler.SpawnReward(17, npc);
                    }
                    break;
                case NPCID.Pumpking:
                    if (Main.pumpkinMoon && !NPC.downedHalloweenKing)
                    {
                        if (TerrariaFlagRandomizer.isArchipelago)
                        {
                            string locationName = LocationSets.CheckToLocation[18];
                            ArchipelagoHelper.locationsCompleted.Add(locationName);
                            ArchipelagoHelper.CompleteLocationChecks();
                        }
                        else RewardsHandler.SpawnReward(18, npc);
                    }
                    break;
                case NPCID.Everscream:
                    if (Main.snowMoon && !NPC.downedChristmasTree)
                    {
                        if (TerrariaFlagRandomizer.isArchipelago)
                        {
                            string locationName = LocationSets.CheckToLocation[19];
                            ArchipelagoHelper.locationsCompleted.Add(locationName);
                            ArchipelagoHelper.CompleteLocationChecks();
                        }
                        else RewardsHandler.SpawnReward(19, npc);
                    }
                    break;
                case NPCID.SantaNK1:
                    if (Main.snowMoon && !NPC.downedChristmasSantank)
                    {
                        if (TerrariaFlagRandomizer.isArchipelago)
                        {
                            string locationName = LocationSets.CheckToLocation[20];
                            ArchipelagoHelper.locationsCompleted.Add(locationName);
                            ArchipelagoHelper.CompleteLocationChecks();
                        }
                        else RewardsHandler.SpawnReward(20, npc);
                    }
                    break;
                case NPCID.IceQueen:
                    if (Main.snowMoon && !NPC.downedChristmasIceQueen)
                    {
                        if (TerrariaFlagRandomizer.isArchipelago)
                        {
                            string locationName = LocationSets.CheckToLocation[21];
                            ArchipelagoHelper.locationsCompleted.Add(locationName);
                            ArchipelagoHelper.CompleteLocationChecks();
                        }
                        else RewardsHandler.SpawnReward(21, npc);
                    }
                    break;
            }
        }
    }
}
