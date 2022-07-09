using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaFlagRandomizer.Common;

namespace TerrariaFlagRandomizer.Content.Items.TestItems
{
    public class HardmodeTrigger : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.GuideVoodooDoll;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hardmode Trigger");
            Tooltip.SetDefault("DEBUG: Starts Hardmode");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 28;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.NPCDeath59;
            Item.consumable = false;
        }

        public override bool? UseItem(Player player)
        {
            RewardsHandler.TriggerHardmode();
            return true;
        }
    }
}
