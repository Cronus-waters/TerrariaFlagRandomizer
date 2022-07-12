using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;

namespace TerrariaFlagRandomizer.Common.Helpers
{
    internal class RandomizerUtils
    {
        public static void SendText(string text, int r = 255, int g = 255, int b = 255)
        {
            if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(text, new Color(r, g, b));
            else if (Main.netMode == NetmodeID.Server) ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), new Color(r, g, b));
        }
    }
}
