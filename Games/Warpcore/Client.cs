using System;
using WarpEngine;

namespace Warpcore
{
	public sealed class Client : Game
	{
		public Client()
		{
			// Create and set up our update loop
			var updateLoop = new Loop(Update, TimeSpan.FromSeconds(1.0/100.0));

			TrackLoop(updateLoop);
			updateLoop.Start();

			// Create and set up our render loop (As fast as possible with a sane limit)
			var renderLoop = new Loop(Render, TimeSpan.FromSeconds(1.0/1000.0));

			TrackLoop(renderLoop);
			renderLoop.Start();
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