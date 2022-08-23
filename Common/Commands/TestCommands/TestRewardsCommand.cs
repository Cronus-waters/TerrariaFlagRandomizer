using Terraria.ModLoader;

namespace TerrariaFlagRandomizer.Common.Commands.TestCommands
{
    public class TestRewardsCommand : ModCommand
    {
        public override CommandType Type => CommandType.Chat;
        public override string Command => "flag";
        public override string Description => "Cals the SetFlag method with the given args. Debug only.";
        public override string Usage => "/flag flag_id [from_boss]";
        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if(args.Length == 0)
            {
                throw new UsageException("Argument flag_id is required");
            }
            if(!int.TryParse(args[0], out int id))
            {
                throw new UsageException(args[0] + " is not an integer");
            }
            if(id < 1 || id > 6)
            {
                throw new UsageException("Argument flag_id is outside the allowed range (1..5)");
            }
            if(args.Length < 2 || !bool.TryParse(args[1], out bool flag))
            {
                flag = false;
            }
            RewardsHandler.SetFlag(id, flag);
        }
    }
}
