using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace WarpEngine
{
	public abstract class Game
	{
		private readonly List<Loop> _loops = new List<Loop>();

		protected Game()
		{
			// Initialize defaults
			Loops = new ReadOnlyCollection<Loop>(_loops);
		}

		public ReadOnlyCollection<Loop> Loops { get; private set; }

		public void StartLoop(Action<TimeSpan> tickFunc, TimeSpan minimumDelta)
		{
			var loop = new Loop(tickFunc, minimumDelta);

			lock (_loops)
			{
				loop.Start();
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
					if (_loops.Count != 0)
						loop = _loops[0];
					else
						break;
				}

				loop.AwaitFinish();

				lock (_loops)
				{
					_loops.Remove(loop);
				}
			}
		}
	}
}