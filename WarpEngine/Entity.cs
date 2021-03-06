﻿using System.Collections.ObjectModel;
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

		public bool Has<T>() where T : class, IEntityComponent
		{
			return Components.Any(c => c is T);
		}

		public T Get<T>() where T : class, IEntityComponent
		{
			return Components.OfType<T>().FirstOrDefault();
		}

		public void Remove<T>() where T : class, IEntityComponent
		{
			var entity = Get<T>();

			if (entity != null)
				Components.Remove(entity);
		}
	}
}