using System;
using System.Collections.ObjectModel;
using System.Linq;

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

		public bool Has<T>()
		{
			return Components.Any(c => c is T);
		}

		public IEntityComponent Get<T>()
		{
			var component = Components.FirstOrDefault(e => e is T);

			if (component == null)
				throw new InvalidOperationException("Entity does not have component of type \"" + typeof (T) + "\".");

			return component;
		}
	}
}