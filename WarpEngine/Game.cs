using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace WarpEngine
{
	public abstract class Game
	{
		private readonly Task _task;

		protected Game()
		{
			// Initialize defaults
			Loops = new Collection<Loop>();

			// Start game
			_task = Task.Run(() => Run());
		}

		public Collection<Loop> Loops { get; private set; }

		public Task Task
		{
			get { return _task; }
		}

		private void Run()
		{
			foreach (var loop in Loops)
			{
				loop.Start();
			}

			foreach (var loop in Loops)
			{
				loop.Task.Wait();
			}
		}
	}
}