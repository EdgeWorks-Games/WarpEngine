using System;
using System.Drawing;
using WarpEngine;
using WarpEngine.Graphics;

namespace Warpcore
{
	public sealed class Client : Game
	{
		private readonly Window _window = new Window();

		private readonly Entity _world = new Entity();
		private readonly PlayerSystem _playerSystem = new PlayerSystem();
		private readonly RenderSystem _renderSystem = new RenderSystem();

		public Client()
		{
			//var atlas = new SpriteAtlas();
			//var playerSprite = atlas.CreateSprite("./Graphics/player.png");
			//atlas.Generate();

			_world.Children.Add(Entities.CreatePlayerEntity());

			// Set up our game loops
			// Bug: Currently, loop deltas don't adjust for how long they actually take to execute.
			StartLoop(Update, TimeSpan.FromSeconds(1.0/120.0));
			StartLoop(Render, TimeSpan.FromSeconds(1.0/1000.0)); // < 1/1000 is just a sane minimum
		}

		private void Update(object sender, LoopEventArgs args)
		{
			_window.ProcessEvents();

			lock (_world)
			{
				_playerSystem.ProcessTree(_world);
			}
		}

		private void Render(object sender, LoopEventArgs args)
		{
			if (!_window.Exists)
				return;

			_window.MakeCurrent();
			_window.TempClear(Color.CornflowerBlue);

			lock (_world)
			{
				_renderSystem.ProcessTree(_world);
			}

			_window.SwapBuffers();
		}
	}
}