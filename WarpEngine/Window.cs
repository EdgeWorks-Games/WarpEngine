using System;
using System.Drawing;
using System.Threading;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace WarpEngine
{
	public class Window
	{
		private GameWindow _window;

#if DEBUG
		private int _threadId;
#endif

		public bool Exists
		{
			get { return _window != null && _window.Exists; }
		}

		public event EventHandler Closing = (s, e) => { };

		public void ProcessEvents()
		{
			// Lazy load in the window here, it needs to be created on the same thread that processes events
			if (_window == null)
			{
				_window = new GameWindow(1024, 768,
					new GraphicsMode(32, 16, 0, 0),
					"WarpEngine",
					GameWindowFlags.FixedWindow)
				{
					Visible = true
				};
				_window.Context.MakeCurrent(null);
				_window.Closing += (s, e) => Closing(this, EventArgs.Empty);

#if DEBUG
				_threadId = Thread.CurrentThread.ManagedThreadId;
#endif
			}

#if DEBUG
			if (_threadId != Thread.CurrentThread.ManagedThreadId)
				throw new InvalidOperationException("Can't process events on a different thread than the window was created on.");
#endif

			// Actually process the events
			_window.ProcessEvents();
		}

		public void MakeCurrent()
		{
			_window.MakeCurrent();
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