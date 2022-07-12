using Terraria.ModLoader;

namespace TerrariaFlagRandomizer.Common.Commands
{
    public class ArchipelagoDisconnectCommand : ModCommand
    {
        public override CommandType Type => CommandType.Chat;
        public override string Command => "disconnect";
        public override string Description => "/disconnect";
        public override void Action(CommandCaller caller, string input, string[] args)
        {
            TerrariaFlagRandomizer.DisconnectFromServer();
        }
    }
}
