using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace TerrariaFlagRandomizer.Common.Configs
{
    internal class GenerationConfigs : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("Generator Settings")]
        [Label("Progressive Flags")]
        [Tooltip("Turns all flags into Progressive Flag rewards, which rewards one of the three in that specific order.")]
        [DefaultValue(false)]
        public bool ProgressiveFlagsToggle;
    }
}
