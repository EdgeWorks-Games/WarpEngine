using System;
using System.Drawing;
using WarpEngine;
using WarpEngine.Basic2D.Graphics;

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
			systems.PlayerSystem = PlayerSystem.Create();
			systems.RenderSystem = RenderSystem.Create();

			// Create our test world
			data.World = new Entity();
			data.World.Children.Add(Entities.CreatePlayerEntity());

			// Set up our game loops
			game.Loops.Add(new Loop(() => Update(engine, systems, data), TimeSpan.FromSeconds(1.0/120.0)));
			game.Loops.Add(new Loop(() => Render(engine, systems, data), TimeSpan.FromSeconds(1.0/60.0)));

			// Make sure our resources are cleared afterwards
			game.Uninitialize += (s, e) => Uninitialize(engine, data);
		}

		private static void Uninitialize(Engine engine, Data data)
		{
			// This disposes our sprites for now, eventually the renderer will have to do this
			var system = TempDisposerSystem.Create();
			system.Process(data.World);

			engine.Window.Dispose();
		}

		private static void Update(Engine engine, Systems systems, Data data)
		{
			engine.Window.ProcessEvents();

			systems.PlayerSystem.Process(data.World);
		}

		private static void Render(Engine engine, Systems systems, Data data)
		{
			// We can only do rendering with the window if it exists
			if (!engine.Window.Exists)
				return;

			engine.Window.TempClear(Color.CornflowerBlue);

			systems.RenderSystem.Process(data.World);

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
			public EntitySystem PlayerSystem { get; set; }
			public EntitySystem RenderSystem { get; set; }
		}
	}
}