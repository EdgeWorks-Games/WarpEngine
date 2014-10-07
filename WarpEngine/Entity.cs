using System.Collections.ObjectModel;

namespace WarpEngine
{
	public sealed class Entity
	{
		public Entity()
		{
			Components = new Collection<IEntityComponent>();
			Children = new Collection<Entity>();
		}

		public Collection<IEntityComponent> Components { get; set; }
		public Collection<Entity> Children { get; set; }
	}
}