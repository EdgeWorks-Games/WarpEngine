using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;

namespace WarpEngine
{
	public sealed class Game
	{
		private readonly Action<Game> _initializer;
		private readonly Thread _thread;
		private bool _keepRunning = true;

		public Game(Action<Game> initializer)
		{
			Loops = new Collection<Loop>();

			_initializer = initializer;
			_thread = new Thread(Run) {Name = "Game Thread"};
			_thread.Start();
		}

		public Collection<Loop> Loops { get; set; } 
		public bool IsRunning { get; private set; }

		private void Run()
		{
			// Initialize our game
			_initializer(this);

			// Run our loop
			var stopwatch = new Stopwatch();
			while (_keepRunning)
			{
				// Get the elapsed time since last check
				var elapsed = stopwatch.Elapsed;
				stopwatch.Restart();

				// Notify all loops of the time passed
				foreach (var loop in Loops)
				{
					loop.NotifyTimePassed(elapsed);
				}

				// Use yield to give the rest of our thread's time to another thread
				if (!Thread.Yield())
				{
					// We couldn't yield, sleep a bit instead
					Thread.Sleep(0);
				}
			}
		}

		public void Stop()
		{
			_keepRunning = false;
		}

		public void Join()
		{
			_thread.Join();
		}
	}
}