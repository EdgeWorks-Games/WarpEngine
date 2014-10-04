using System;
using WarpEngine;

namespace Warpcore
{
	public sealed class Client : Game
	{
		public Client()
		{
			// Create and set up our update loop
			var updateLoop = new Loop(TimeSpan.FromSeconds(1.0/100.0));
			updateLoop.Tick += Update;

			TrackLoop(updateLoop);
			updateLoop.Start();

			// Create and set up our render loop (As fast as possible with a sane limit)
			var renderLoop = new Loop(TimeSpan.FromSeconds(1.0/1000.0));
			renderLoop.Tick += Render;

			TrackLoop(renderLoop);
			renderLoop.Start();
		}

		private void Update(object sender, EventArgs args)
		{
		}

		private void Render(object sender, EventArgs args)
		{
		}
	}
}