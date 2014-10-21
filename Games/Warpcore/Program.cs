using CommandLine;

namespace Warpcore
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			var options = new CmdOptions();
			if (!Parser.Default.ParseArguments(args, options))
				return;

			// Right now we're not doing anything with the flags.
			// We're just creating a client-side game.

			var game = new Client();
			game.Join();
		}
	}
}