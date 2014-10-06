using System;
using WarpEngine;
using WarpEngine.Graphics;

namespace Warpcore
{
	public sealed class Client : Game
	{
		private readonly World _world = new World();

		public Client()
		{
			var atlas = new SpriteAtlas();
			var playerSprite = atlas.CreateSprite("./Graphics/player.png");
			atlas.Generate();

			var playerEntity = new Entity();
			playerEntity.Add(new TransformComponent());
			playerEntity.Add(new SpriteComponent(playerSprite));
			playerEntity.Add(new PlayerComponent());
			_world.Add(playerEntity);

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