using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace WarpEngine
{
	public class Game
	{
		private readonly List<Loop> _loops = new List<Loop>();
		private readonly Thread _thread;
		private bool _keepRunning = true;

		public Game()
		{
			_thread = new Thread(Run) {Name = "Game Thread"};
			_thread.Start();
		}

		public bool IsRunning { get; private set; }

		public event EventHandler Initialize = (s, e) => { };

		private void Run()
		{
			// Initialize our game
			Initialize(this, EventArgs.Empty);

			// Run our loop
			var stopwatch = new Stopwatch();
			while (_keepRunning)
			{
				// Get the elapsed time since last check
				var elapsed = stopwatch.Elapsed;
				stopwatch.Restart();

				// Notify all loops of the time passed
				_loops.ForEach(l => l.NotifyTimePassed(elapsed));

				// Use yield to give the rest of our thread's time to another thread
				if (!Thread.Yield())
				{
					// We couldn't yield, sleep a bit instead
					Thread.Sleep(0);
				}
			}
		}

		public Loop CreateLoop(EventHandler<LoopEventArgs> callback, TimeSpan targetDelta)
		{
			// Create and set up our update loop
			var loop = new Loop(callback, targetDelta);

			AddLoop(loop);

			return loop;
		}

		public void AddLoop(Loop loop)
		{
			lock (_loops)
			{
				_loops.Add(loop);
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