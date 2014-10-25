using System;

namespace WarpEngine
{
	public sealed class EntitySystem
	{
		public EntitySystem()
		{
			Filter = e => true;
		}

		public Func<Entity, bool> Filter { get; set; }
		public Action<Entity> Processor { get; set; }

		public void Process(Entity root)
		{
			ProcessTree(root);
		}

		private void ProcessTree(Entity root)
		{
			foreach (var entity in root.Children)
			{
				if (Filter(entity))
					Processor(entity);

				ProcessTree(entity);
			}
		}
	}
}