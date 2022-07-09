using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaFlagRandomizer.Common.Sets;

namespace TerrariaFlagRandomizer.Content.Items
{
    internal class LootBag1 : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.BossBagOgre;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tier 1 Loot Bag");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}\nPre-Skeletron items");
        }

        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.consumable = true;
            Item.width = 24;
            Item.height = 24;
            Item.rare = ItemRarityID.Purple;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
			var source = player.GetSource_OpenItem(Type);
			// Progressive bar
			int type = Main.rand.Next(TieredItemSets.PreSkeletronBars);
			player.QuickSpawnItem(source, type, Main.rand.Next(15, 36));
			// Special case: Shadow Scale/Tissue Sample
			if (type == ItemID.DemoniteBar) player.QuickSpawnItem(source, ItemID.ShadowScale, Main.rand.Next(15, 21));
			if (type == ItemID.CrimtaneBar) player.QuickSpawnItem(source, ItemID.TissueSample, Main.rand.Next(15, 21));

			// Progressive Material
			int times = Main.rand.Next(3, 5);
			for(int i = 0; i < times; i++)
            {
				int index = Main.rand.Next(TieredItemSets.PreSkeletronMaterials.GetLength(0));
				type = TieredItemSets.PreSkeletronMaterials[index, 0];
				int min = TieredItemSets.PreSkeletronMaterials[index, 1];
				int max = TieredItemSets.PreSkeletronMaterials[index, 2] + 1;
				player.QuickSpawnItem(source, type, Main.rand.Next(min, max));
            }

			// Progressive weapons
			times = Main.rand.Next(1, 3);
			for(int i = 0; i < times; i++)
            {
				type = Main.rand.Next(TieredItemSets.PreSkeletronWeapons);
				player.QuickSpawnItem(source, type);
				// Special case: Weapons that require ammo
                if (TieredItemSets.AmmoWeapons.ContainsKey(type))
                {
					int index = type;
					type = (int)TieredItemSets.AmmoWeapons[index].GetValue(0);
					int min = (int)TieredItemSets.AmmoWeapons[index].GetValue(1);
					int max = (int)TieredItemSets.AmmoWeapons[index].GetValue(2) + 1;
					player.QuickSpawnItem(source, type, Main.rand.Next(min, max));
                }
            }

			// Any accessory
			times = Main.rand.Next(0, 3);
			for(int i = 0; i < times; i++)
            {
				type = Main.rand.Next(TieredItemSets.Accessories);
				player.QuickSpawnItem(source, type);
            }

            // Expert Item (10% chance)
            if (Main.rand.NextBool(10))
            {
				type = Main.rand.Next(TieredItemSets.ExpertItems);
				player.QuickSpawnItem(source, type);
            }

			// Junk Item
			times = Main.rand.Next(0, 5);
			for (int i = 0; i < times; i++)
			{
				int index = Main.rand.Next(TieredItemSets.JunkItems.GetLength(0));
				type = TieredItemSets.JunkItems[index, 0];
				int min = TieredItemSets.JunkItems[index, 1];
				int max = TieredItemSets.JunkItems[index, 2] + 1;
				player.QuickSpawnItem(source, type, Main.rand.Next(min, max));
            }
        }

		public override Color? GetAlpha(Color lightColor)
		{
			// Makes sure the dropped bag is always visible
			return Color.Lerp(lightColor, Color.White, 0.4f);
		}

		public override void PostUpdate()
		{
			// Spawn some light and dust when dropped in the world
			Lighting.AddLight(Item.Center, Color.White.ToVector3() * 0.4f);

			if (Item.timeSinceItemSpawned % 12 == 0)
			{
				Vector2 center = Item.Center + new Vector2(0f, Item.height * -0.1f);

				// This creates a randomly rotated vector of length 1, which gets it's components multiplied by the parameters
				Vector2 direction = Main.rand.NextVector2CircularEdge(Item.width * 0.6f, Item.height * 0.6f);
				float distance = 0.3f + Main.rand.NextFloat() * 0.5f;
				Vector2 velocity = new Vector2(0f, -Main.rand.NextFloat() * 0.3f - 1.5f);

				Dust dust = Dust.NewDustPerfect(center + direction * distance, DustID.SilverFlame, velocity);
				dust.scale = 0.5f;
				dust.fadeIn = 1.1f;
				dust.noGravity = true;
				dust.noLight = true;
				dust.alpha = 0;
			}
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			// Draw the periodic glow effect behind the item when dropped in the world (hence PreDrawInWorld)
			Texture2D texture = TextureAssets.Item[Item.type].Value;

			Rectangle frame;

			if (Main.itemAnimations[Item.type] != null)
			{
				// In case this item is animated, this picks the correct frame
				frame = Main.itemAnimations[Item.type].GetFrame(texture, Main.itemFrameCounter[whoAmI]);
			}
			else
			{
				frame = texture.Frame();
			}

			Vector2 frameOrigin = frame.Size() / 2f;
			Vector2 offset = new Vector2(Item.width / 2 - frameOrigin.X, Item.height - frame.Height);
			Vector2 drawPos = Item.position - Main.screenPosition + frameOrigin + offset;

			float time = Main.GlobalTimeWrappedHourly;
			float timer = Item.timeSinceItemSpawned / 240f + time * 0.04f;

			time %= 4f;
			time /= 2f;

			if (time >= 1f)
			{
				time = 2f - time;
			}

			time = time * 0.5f + 0.5f;

			for (float i = 0f; i < 1f; i += 0.25f)
			{
				float radians = (i + timer) * MathHelper.TwoPi;

				spriteBatch.Draw(texture, drawPos + new Vector2(0f, 8f).RotatedBy(radians) * time, frame, new Color(90, 70, 255, 50), rotation, frameOrigin, scale, SpriteEffects.None, 0);
			}

			for (float i = 0f; i < 1f; i += 0.34f)
			{
				float radians = (i + timer) * MathHelper.TwoPi;

				spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(140, 120, 255, 77), rotation, frameOrigin, scale, SpriteEffects.None, 0);
			}

			return true;
		}
	}
}
