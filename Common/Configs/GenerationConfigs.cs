using IL.Terraria.Graphics.Capture;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace TerrariaFlagRandomizer.Common.Configs
{
    internal class GenerationConfigs : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("Generator Settings")]
        [Label("Progressive Flags")]
        [DefaultValue(false)]
        [Tooltip("Turns all major world-altering flags into Progressive Flag rewards, which rewards one of the three in that specific order.")]
        public bool ProgressiveFlagsToggle;

        [Label("Chestsanity")]
        [DefaultValue(false)]
        [Tooltip("Adds checks for opening certain types of chests in the world.")]
        public bool ChestsanityToggle;

        [Header("Chestsanity Settings")]

        [Label("% of common chests")]
        [Range(1, 100)]
        [Increment(1)]
        [DefaultValue(10)]
        [Tooltip("Percentage of common underground chests to add into the pool.\nAffects Gold Chests, Granite/Marble chests, Rich Mahogany chests in the jungle, and Glowing Mushroom chests\nOnly applies if Chestsanity is enabled.")]
        public int ChestsanityUndergroundChestRate;

        [Label("% of Underground Ice chests")]
        [Range(1, 100)]
        [Increment(1)]
        [DefaultValue(10)]
        [Tooltip("Percentage of chests in the underground Ice biome to add into the pool.\nOnly applies if Chestsanity is enabled.")]
        public int ChestsanityIceChestRate;

        [Label("% of Desert chests")]
        [Range(1, 100)]
        [Increment(1)]
        [DefaultValue(20)]
        [Tooltip("Percentage of chests from houses in the Underground Desert to add into the pool.\nOnly applies if Chestsanity is enabled.")]
        public int ChestsanityDesertChestRate;

        [Label("% of Dungeon chests")]
        [Range(1, 100)]
        [Increment(1)]
        [DefaultValue(20)]
        [Tooltip("Percentage of locked chests from the Dungeon to add into the pool.\nOnly applies if Chestsanity is enabled.")]
        public int ChestsanityDungeonChestRate;

        [Label("% of Jungle chests")]
        [Range(1, 100)]
        [Increment(1)]
        [DefaultValue(15)]
        [Tooltip("Percentage of Ivy chests from Jungle shrines and living mahogany trees to add into the pool.\nOnly applies if Chestsanity is enabled.")]
        public int ChestsanityJungleShrineChestRate;

        [Label("% of Shadow chests")]
        [Range(1, 100)]
        [Increment(1)]
        [DefaultValue(20)]
        [Tooltip("Percentage of Shadow Chests from the Underworld to add into the pool.\nOnly applies if Chestsanity is enabled.")]
        public int ChestsanityShadowChestRate;

        [Label("% of Temple chests")]
        [Range(1, 100)]
        [Increment(1)]
        [DefaultValue(50)]
        [Tooltip("Percentage of Lihzahrd chests from the Jungle Temple to add into the pool.\nOnly applies if Chestsanity is enabled.")]
        public int ChestsanityTempleChestRate;

        [Label("Include Sky Islands")]
        [DefaultValue(false)]
        [Tooltip("Adds the chests on floating islands into the pool.\nOnly applies if Chestsanity is enabled.")]
        public bool ChestsanityIncludeSkyIslands;

        [Label("Include Pyramid chests")]
        [DefaultValue(false)]
        [Tooltip("Includes the chests inside pyramids to the pool, if any generate.\nOnly applies if Chestsanity is enabled.")]
        public bool ChestsanityIncludePyramids;

        [Label("Include Ocean chests")]
        [DefaultValue(false)]
        [Tooltip("Includes the Water Chests inside Ocean caves into the pool, if any generate.\nOnly applies if Chestsanity is enabled.")]
        public bool ChestsanityIncludeOceanChests;

        [Label("Include Biome chests")]
        [DefaultValue(false)]
        [Tooltip("Adds the Biome chests from the Dungeon into the pool.\nOnly applies if Chestsanity is enabled.")]
        public bool ChestsanityIncludeBiomeChests;
    }
}
