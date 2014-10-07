using System;
using WarpEngine;
using WarpEngine.Graphics;

namespace Warpcore
{
	public sealed class Client : Game
	{
		private readonly Entity _world = new Entity();
		private readonly PlayerSystem _playerSystem = new PlayerSystem();
		private readonly RenderSystem _renderSystem = new RenderSystem();

		public Client()
		{
			//var atlas = new SpriteAtlas();
			//var playerSprite = atlas.CreateSprite("./Graphics/player.png");
			//atlas.Generate();

			var playerEntity = new Entity();
			playerEntity.Components.Add(new TransformComponent());
			//playerEntity.Components.Add(new SpriteComponent(playerSprite));
			playerEntity.Components.Add(new PlayerComponent());
			_world.Children.Add(playerEntity);

			// Create and set up our update loop (Twice the average VSynced framerate)
			StartLoop(Update, TimeSpan.FromSeconds(1.0/120.0));

			// Create and set up our render loop (As fast as possible with a sane limit)
			// Bug: Currently, loop deltas don't adjust for how long they actually take to execute.
			StartLoop(Render, TimeSpan.FromSeconds(1.0/1000.0));
		}

		private void Update(object sender, LoopEventArgs args)
		{
			lock (_world)
			{
				Console.WriteLine("Update: " + args.Delta);
				//_playerSystem.Process(_world);
			}
		}

		private void Render(object sender, LoopEventArgs args)
		{
			lock (_world)
			{
				Console.WriteLine("Render: " + args.Delta);
				//_renderSystem.Process(_world);
			}
		}
	}
}