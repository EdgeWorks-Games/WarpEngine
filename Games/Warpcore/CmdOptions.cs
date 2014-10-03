using CommandLine;
using CommandLine.Text;

namespace Warpcore
{
	internal class CmdOptions
	{
		[Option('h', "headless", DefaultValue = false, HelpText = "Runs the game in headless mode (to be used with server).")]
		public bool Headless { get; set; }

		[Option('s', "server", DefaultValue = false, HelpText = "Runs the game as a server.")]
		public bool Server { get; set; }

		[HelpOption]
		public string GetUsage()
		{
			return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
		}
	}
}