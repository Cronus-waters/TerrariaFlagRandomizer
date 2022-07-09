using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaFlagRandomizer.Common.Systems;

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
                        RewardsHandler.SpawnReward(npc, 0);
                    }
                    break;
                case NPCID.EyeofCthulhu:
                    if (!NPC.downedBoss1)
                    {
                        RewardsHandler.SpawnReward(npc, 1);
                    }
                    break;
                case NPCID.EaterofWorldsHead:
                case NPCID.EaterofWorldsBody:
                case NPCID.EaterofWorldsTail:
                    if(npc.boss) {
                        if (!NPCCheckSystem.firstEaterKill)
                        {
                            RewardsHandler.SpawnReward(npc, 2);
                            NPCCheckSystem.firstEaterKill = true;
                        }
                    }
                    break;
                case NPCID.BrainofCthulhu:
                    if (!NPCCheckSystem.firstBrainKill)
                    {
                        RewardsHandler.SpawnReward(npc, 3);
                        NPCCheckSystem.firstBrainKill = true;
                    }
                    break;
                case NPCID.SkeletronHead:
                    if (!NPCCheckSystem.firstSkeletronKill)
                    {
                        RewardsHandler.SpawnReward(npc, 4);
                        NPCCheckSystem.firstSkeletronKill = true;
                    }
                    break;
                case NPCID.QueenBee:
                    if (!NPC.downedQueenBee)
                    {
                        RewardsHandler.SpawnReward(npc, 5);
                    }
                    break;
                case NPCID.Deerclops:
                    if (!NPC.downedDeerclops)
                    {
                        RewardsHandler.SpawnReward(npc, 6);
                    }
                    break;
                case NPCID.WallofFlesh:
                    if (!NPCCheckSystem.firstWOFKill)
                    {
                        RewardsHandler.SpawnReward(npc, 7);
                        NPCCheckSystem.firstWOFKill = true;
                    }
                    break;
                case NPCID.QueenSlimeBoss:
                    if (!NPC.downedQueenSlime)
                    {
                        RewardsHandler.SpawnReward(npc, 8);
                    }
                    break;
                case NPCID.Retinazer:
                case NPCID.Spazmatism:
                    if(!NPC.AnyNPCs((npc.type == NPCID.Retinazer) ? NPCID.Spazmatism : NPCID.Retinazer))
                    {
                        if (!NPCCheckSystem.firstTwinsKill)
                        {
                            RewardsHandler.SpawnReward(npc, 9);
                            NPCCheckSystem.firstTwinsKill = true;
                        }
                    }
                    break;
                case NPCID.TheDestroyer:
                    if (!NPCCheckSystem.firstDestroyerKill)
                    {
                        RewardsHandler.SpawnReward(npc, 10);
                        NPCCheckSystem.firstDestroyerKill = true;
                    }
                    break;
                case NPCID.SkeletronPrime:
                    if (!NPCCheckSystem.firstPrimeKill)
                    {
                        RewardsHandler.SpawnReward(npc, 11);
                        NPCCheckSystem.firstPrimeKill = true;
                    }
                    break;
                case NPCID.Plantera:
                    if (!NPCCheckSystem.firstPlantKill)
                    {
                        RewardsHandler.SpawnReward(npc, 12);
                        NPCCheckSystem.firstPlantKill = true;
                    }
                    break;
                case NPCID.Golem:
                    if (!NPCCheckSystem.firstGolemKill)
                    {
                        RewardsHandler.SpawnReward(npc, 13);
                        NPCCheckSystem.firstGolemKill = true;
                    }
                    break;
                case NPCID.DukeFishron:
                    if (!NPC.downedFishron)
                    {
                        RewardsHandler.SpawnReward(npc, 14);
                    }
                    break;
                case NPCID.HallowBoss:
                    if (!NPC.downedEmpressOfLight)
                    {
                        RewardsHandler.SpawnReward(npc, 15);
                    }
                    break;
                case NPCID.CultistBoss:
                    if (!NPC.downedAncientCultist)
                    {
                        RewardsHandler.SpawnReward(npc, 16);
                    }
                    break;
                /*case NPCID.MoonLordCore:
                    Main.NewText("Moon Lord defeated");
                    if (!NPC.downedMoonlord)
                    {
                        Main.NewText("First Moon Lord kill");
                        SpawnLootBag(npc);
                    }
                    break;*/
                case NPCID.Mothron:
                    if (!NPCCheckSystem.firstMothronKill)
                    {
                        RewardsHandler.SpawnReward(npc, 22);
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
                        RewardsHandler.SpawnReward(npc, 23);
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
                        RewardsHandler.SpawnReward(npc, 24);
                        NPCCheckSystem.firstCorruptMimicKill = true;
                        if(Main.netMode == NetmodeID.Server)
                        {
                            NetMessage.SendData(MessageID.WorldData);
                        }
                    }
                    break;
                case NPCID.BigMimicCrimson:
                    if (!NPCCheckSystem.firstCrimsonMimicKill){
                        RewardsHandler.SpawnReward(npc, 25);
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
                        RewardsHandler.SpawnReward(npc, 17);
                    }
                    break;
                case NPCID.Pumpking:
                    if (Main.pumpkinMoon && !NPC.downedHalloweenKing)
                    {
                        RewardsHandler.SpawnReward(npc, 18);
                    }
                    break;
                case NPCID.Everscream:
                    if (Main.snowMoon && !NPC.downedChristmasTree)
                    {
                        RewardsHandler.SpawnReward(npc, 19);
                    }
                    break;
                case NPCID.SantaNK1:
                    if (Main.snowMoon && !NPC.downedChristmasSantank)
                    {
                        RewardsHandler.SpawnReward(npc, 20);
                    }
                    break;
                case NPCID.IceQueen:
                    if (Main.snowMoon && !NPC.downedChristmasIceQueen)
                    {
                        RewardsHandler.SpawnReward(npc, 21);
                    }
                    break;
            }
        }
    }
}
