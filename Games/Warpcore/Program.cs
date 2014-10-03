using System.Threading.Tasks;
using CommandLine;

namespace Warpcore
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			MainAsync(args).Wait();
		}

		private static async Task MainAsync(string[] args)
		{
			var options = new CmdOptions();
			if (!Parser.Default.ParseArguments(args, options))
				return;

			// Right now we're not doing anything with the flags.
			// We're just creating a client-side game.

			var game = new Client();
			await game;
		}
	}
}