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

		private readonly Entity _player;

		public Client()
		{
			//var atlas = new SpriteAtlas();
			//var playerSprite = atlas.CreateSprite("./Graphics/player.png");
			//atlas.Generate();

			_player = Entities.CreatePlayerEntity();
			_world.Children.Add(_player);

			// Set up our game loops
			// Bug: Currently, loop deltas don't adjust for how long they actually take to execute.
			StartLoop(Update, TimeSpan.FromSeconds(1.0/120.0));
			StartLoop(Render, TimeSpan.FromSeconds(1.0/1000.0)); // < 1/1000 is just a sane minimum
		}

		private void Update(object sender, LoopEventArgs args)
		{
			lock (_world)
			{
				_playerSystem.ProcessTree(_world);
			}
		}

		private void Render(object sender, LoopEventArgs args)
		{
			lock (_world)
			{
				Console.WriteLine("Render():");

				_renderSystem.ProcessTree(_world);

				Console.WriteLine();
			}
		}
	}
}