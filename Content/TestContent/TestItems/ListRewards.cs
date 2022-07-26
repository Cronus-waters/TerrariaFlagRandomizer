using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.ModLoader;
using TerrariaFlagRandomizer.Common.Systems;

namespace TerrariaFlagRandomizer.Content.TestContent.TestItems
{
    internal class ListRewards : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.DirtBlock;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("List locations");
            Tooltip.SetDefault("DEBUG: Lists all locations and rewards");
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.DirtBlock);
            Item.consumable = false;
        }

        public override bool? UseItem(Player player)
        {
            foreach(var entry in RandomizerSystem.locationRewardPairs)
            {
                if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(entry.Key + ", " + entry.Value);
                else if (Main.netMode == NetmodeID.Server) ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(entry.Key + ", " + entry.Value), Color.White);
            }
            return true;
        }
    }
}
