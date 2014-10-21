using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace WarpEngine
{
	public class Window
	{
		private readonly GameWindow _window;

		public Window()
		{
			_window = new GameWindow(1024, 768,
				new GraphicsMode(32, 16, 0, 0),
				"WarpEngine",
				GameWindowFlags.FixedWindow)
			{
				Visible = true
			};
			_window.Closing += (s, e) => Closing(this, EventArgs.Empty);
		}

		public bool Exists
		{
			get { return _window != null && _window.Exists; }
		}

		public bool IsCurrent
		{
			get { return _window.Context.IsCurrent; }
		}

		public event EventHandler Closing = (s, e) => { };

		public void ProcessEvents()
		{
			_window.ProcessEvents();
		}

		public void TempClear(Color color)
		{
			GL.ClearColor(color);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
		}

		public void SwapBuffers()
		{
			_window.SwapBuffers();
		}
	}
}