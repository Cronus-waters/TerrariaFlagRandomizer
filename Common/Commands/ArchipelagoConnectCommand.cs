using Terraria.ModLoader;

namespace TerrariaFlagRandomizer.Common.Commands
{
    public class ArchipelagoConnectCommand : ModCommand
    {
        public override CommandType Type => CommandType.Chat;
        public override string Command => "connect";
        public override string Description => "/connect username ip port";
        public override void Action(CommandCaller caller, string input, string[] args)
        {
            TerrariaFlagRandomizer.ConnectToServer(input, args);
        }
    }
}
