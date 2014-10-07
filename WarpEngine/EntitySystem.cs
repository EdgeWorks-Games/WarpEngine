using System;

namespace WarpEngine
{
	public abstract class EntitySystem
	{
		protected EntitySystem()
		{
			Filter = e => true;
		}

		public Func<Entity, bool> Filter { get; set; }

		public void ProcessTree(Entity root)
		{
			foreach (var entity in root.Children)
			{
				if (Filter(entity))
					ProcessEntity(entity);

				ProcessTree(entity);
			}
		}

		public abstract void ProcessEntity(Entity entity);
	}
}