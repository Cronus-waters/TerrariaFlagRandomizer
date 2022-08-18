using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaFlagRandomizer.Common.Sets;

namespace TerrariaFlagRandomizer.Content.Items
{
	internal class LootBag3 : ModItem
	{
		public override string Texture => "Terraria/Images/Item_" + ItemID.BossBagOgre;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tier 3 Loot Bag");
			Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}\nEarly Hardmode items");
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

		public override void ModifyItemLoot(ItemLoot itemLoot)
		{
			// Progressive Bar rule
			itemLoot.Add(new OneFromRulesRule(1, TieredItemSets.HardmodeBars));

			// Progressive Material rule
			itemLoot.Add(new OneFromRulesRule(1, TieredItemSets.HardmodeMaterials));

			// Build Progressive Weapons rules
			List<IItemDropRule> weaponRulesList = new List<IItemDropRule>();
			foreach (int id in TieredItemSets.HardmodeWeapons)
			{
				IItemDropRule rule = ItemDropRule.Common(id);
				// Ammo considerations
				if (TieredItemSets.AmmoWeapons.ContainsKey(id))
				{
					rule.OnSuccess(TieredItemSets.AmmoWeapons[id]);
				}
				weaponRulesList.Add(rule);
			}
			// Rules for dropping 1-3 weapons
			IItemDropRule tripleWeaponRule = new FewFromRulesRule(3, 3, weaponRulesList.ToArray());
			IItemDropRule doubleWeaponRule = new FewFromRulesRule(2, 3, weaponRulesList.ToArray());
			IItemDropRule singleWeaponRule = new OneFromRulesRule(1, weaponRulesList.ToArray());
			IItemDropRule[] weaponRules = new IItemDropRule[]
			{
				tripleWeaponRule,
				doubleWeaponRule,
				singleWeaponRule
			};
			itemLoot.Add(new SequentialRulesNotScalingWithLuckRule(1, weaponRules));

			// Rules for dropping 0-3 accessories
			IItemDropRule threeAccessoriesRule = ItemDropRule.FewFromOptionsNotScalingWithLuck(3, 4, TieredItemSets.Accessories);
			IItemDropRule twoAccessoriesRule = ItemDropRule.FewFromOptionsNotScalingWithLuck(2, 4, TieredItemSets.Accessories);
			IItemDropRule oneAccessoryRule = ItemDropRule.FewFromOptionsNotScalingWithLuck(1, 4, TieredItemSets.Accessories);
			IItemDropRule[] accessoryRules = new IItemDropRule[]
			{
				threeAccessoriesRule,
				twoAccessoriesRule,
				oneAccessoryRule
			};
			itemLoot.Add(new SequentialRulesNotScalingWithLuckRule(1, accessoryRules));

			// Wings rule (10% chance)
			IItemDropRule wingsRule = ItemDropRule.OneFromOptionsNotScalingWithLuck(10, TieredItemSets.Wings);
			itemLoot.Add(wingsRule);

			// Expert Item rule (10% chance)
			IItemDropRule expertItemRule = ItemDropRule.OneFromOptionsNotScalingWithLuck(10, TieredItemSets.ExpertItems);
			itemLoot.Add(expertItemRule);

			// Junk Item rules
			const int numJunk = 4;
			List<IItemDropRule> junkRules = new List<IItemDropRule>();
			for (int i = 1; i <= numJunk; i++)
			{
				IItemDropRule rule = new FewFromRulesRule(i, numJunk + 1, TieredItemSets.JunkItems);
				junkRules.Add(rule);
			}
			itemLoot.Add(new SequentialRulesNotScalingWithLuckRule(1, junkRules.ToArray()));
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
