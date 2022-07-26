using Terraria;
using Terraria.ModLoader;

namespace TerrariaFlagRandomizer.Common.Commands.TestCommands
{
    internal class ClearInventoryCommand : ModCommand
    {
        public override CommandType Type => CommandType.Chat;
        public override string Command => "clear";
        public override string Description => "Clears all non-favorited items from your inventory. Debug only.";
        public override string Usage => "/clear";
        public override void Action(CommandCaller caller, string input, string[] args)
        {
            Player callerPlayer = caller.Player;
            foreach(Item item in callerPlayer.inventory)
            {
                if (!item.favorited) item.TurnToAir();
            }
        }
    }
}
