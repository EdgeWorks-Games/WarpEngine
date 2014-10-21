using System;
using System.Drawing;
using WarpEngine;
using WarpEngine.Basic;
using WarpEngine.Graphics;

namespace Warpcore
{
	public static class Client
	{
		public static Game Create()
		{
			return new Game(Initialize);
		}

		private static void Initialize(Game game)
		{
			var data = new Data();

			// Initialize our entity systems
			data.PlayerSystem = new PlayerSystem();
			data.RenderSystem = new RenderSystem<TransformComponent>();

			//var atlas = new SpriteAtlas();
			//var playerSprite = atlas.CreateSprite("./Graphics/player.png");
			//atlas.Generate();

			data.World = new Entity();
			data.World.Children.Add(Entities.CreatePlayerEntity());

			// Set up our game loops
			// Bug: Currently, loop deltas don't adjust for how long they actually take to execute.
			game.CreateLoop(() => Update(game, data), TimeSpan.FromSeconds(1.0/120.0));
			game.CreateLoop(() => Render(game, data), TimeSpan.FromSeconds(1.0/1000.0)); // < 1/1000 is just a sane minimum

			// Create our game's window (this is where the window will be opened)
			data.Window = new Window();
			data.Window.Closing += (s, e) => game.Stop();
		}

		private static void Update(Game game, Data data)
		{
			data.Window.ProcessEvents();

			data.PlayerSystem.ProcessTree(data.World);
		}

		private static void Render(Game game, Data data)
		{
			// We can only do rendering with the window if it exists
			if (!data.Window.Exists)
				return;

			data.Window.TempClear(Color.CornflowerBlue);

			data.RenderSystem.ProcessTree(data.World);

			data.Window.SwapBuffers();
		}

		private class Data
		{
			public Window Window { get; set; }
			public Entity World { get; set; }

			public PlayerSystem PlayerSystem { get; set; }
			public RenderSystem<TransformComponent> RenderSystem { get; set; }
		}
	}
}