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
			var engine = new Engine();
			var systems = new Systems();
			var data = new Data();

			// Set up our engine components
			engine.Game = game;
			engine.Window = new Window();
			engine.Window.Closing += (s, e) => game.Stop();

			// Initialize our entity systems
			systems.PlayerSystem = new PlayerSystem();
			systems.RenderSystem = new RenderSystem<TransformComponent>();

			//var atlas = new SpriteAtlas();
			//var playerSprite = atlas.CreateSprite("./Graphics/player.png");
			//atlas.Generate();

			data.World = new Entity();
			data.World.Children.Add(Entities.CreatePlayerEntity());

			// Set up our game loops
			// Bug: Currently, loop deltas don't adjust for how long they actually take to execute.
			game.Loops.Add(new Loop(() => Update(engine, systems, data), TimeSpan.FromSeconds(1.0/120.0)));
			game.Loops.Add(new Loop(() => Render(engine, systems, data), TimeSpan.FromSeconds(1.0/60.0)));
		}

		private static void Update(Engine engine, Systems systems, Data data)
		{
			engine.Window.ProcessEvents();

			systems.PlayerSystem.ProcessTree(data.World);
		}

		private static void Render(Engine engine, Systems systems, Data data)
		{
			// We can only do rendering with the window if it exists
			if (!engine.Window.Exists)
				return;

			engine.Window.TempClear(Color.CornflowerBlue);

			systems.RenderSystem.ProcessTree(data.World);

			engine.Window.SwapBuffers();
		}

		private class Data
		{
			public Entity World { get; set; }
		}

		private class Engine
		{
			public Game Game { get; set; }
			public Window Window { get; set; }
		}

		public class Systems
		{
			public PlayerSystem PlayerSystem { get; set; }
			public RenderSystem<TransformComponent> RenderSystem { get; set; }
		}
	}
}