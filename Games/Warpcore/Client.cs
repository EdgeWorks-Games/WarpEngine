using System;
using WarpEngine;

namespace Warpcore
{
	public sealed class Client : Game
	{
		public Client()
		{
			// Create and set up our update loop (Twice the average framerate)
			StartLoop(Update, TimeSpan.FromSeconds(1.0/120.0));

			// Create and set up our render loop (As fast as possible with a sane limit)
			StartLoop(Render, TimeSpan.FromSeconds(1.0/1000.0));
		}

		private void Update(object sender, LoopEventArgs args)
		{
			Console.WriteLine("Update: " + args.Delta);
		}

		private void Render(object sender, LoopEventArgs args)
		{
			Console.WriteLine("Render: " + args.Delta);
		}
	}
}