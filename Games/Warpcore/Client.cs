using System;
using System.Drawing;
using WarpEngine;
using WarpEngine.Basic;
using WarpEngine.Graphics;

namespace Warpcore
{
	public sealed class Client : Game
	{
		private readonly PlayerSystem _playerSystem = new PlayerSystem();
		private readonly RenderSystem<TransformComponent> _renderSystem = new RenderSystem<TransformComponent>();
		private readonly Entity _world = new Entity();
		private Window _window;

		public Client()
		{
			Initialize += OnInitialize;
		}

		private void OnInitialize(object sender, EventArgs args)
		{
			_window = new Window();
			_window.Closing += (s, e) => Stop();

			//var atlas = new SpriteAtlas();
			//var playerSprite = atlas.CreateSprite("./Graphics/player.png");
			//atlas.Generate();

			_world.Children.Add(Entities.CreatePlayerEntity());

			// Set up our game loops
			// Bug: Currently, loop deltas don't adjust for how long they actually take to execute.
			CreateLoop(Update, TimeSpan.FromSeconds(1.0 / 120.0));
			CreateLoop(Render, TimeSpan.FromSeconds(1.0 / 1000.0)); // < 1/1000 is just a sane minimum
		}

		private void Update(object sender, LoopEventArgs args)
		{
			_window.ProcessEvents();

			_playerSystem.ProcessTree(_world);
		}

		private void Render(object sender, LoopEventArgs args)
		{
			// We can only do rendering with the window if it exists
			if (!_window.Exists)
				return;

			_window.TempClear(Color.CornflowerBlue);

			_renderSystem.ProcessTree(_world);

			_window.SwapBuffers();
		}
	}
}