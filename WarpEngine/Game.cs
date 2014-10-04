using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace WarpEngine
{
	public class Game
	{
		private readonly List<Loop> _loops = new List<Loop>();

		/// <summary>
		///     Returns true if any loop is currently running.
		/// </summary>
		public bool IsRunning
		{
			get
			{
				lock (_loops)
				{
					return _loops.All(l => l.IsRunning);
				}
			}
		}

		public Loop StartLoop(EventHandler<LoopEventArgs> callback, TimeSpan minimumDelta)
		{
			// Create and set up our update loop
			var loop = new Loop(callback, minimumDelta);

			TrackLoop(loop);
			loop.Start();

			return loop;
		}

		public void TrackLoop(Loop loop)
		{
			lock (_loops)
			{
				_loops.Add(loop);
			}
		}

		public TaskAwaiter GetAwaiter()
		{
			return Task.Run(() => AwaitFinish()).GetAwaiter();
		}

		private void AwaitFinish()
		{
			while (true)
			{
				Loop loop;

				lock (_loops)
				{
					// Try to find a running loop
					loop = _loops.FirstOrDefault(l => l.IsRunning);
				}

				// Couldn't find one? Then we're done!
				if (loop == null)
					break;

				// We found one, wait for it to finish
				loop.AwaitFinish();
			}
		}
	}
}