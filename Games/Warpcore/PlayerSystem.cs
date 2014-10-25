using WarpEngine;
using WarpEngine.Basic2D;

namespace Warpcore
{
	public static class PlayerSystem
	{
		public static EntitySystem Create()
		{
			return new EntitySystem
			{
				Filter = e => e.Has<TransformComponent>() && e.Has<PlayerComponent>(),
				Processor = Process
			};
		}

		private static void Process(Entity entity)
		{
			var transform = entity.Get<TransformComponent>();
			var player = entity.Get<PlayerComponent>();

			transform.Position += new WorldDistance(player.Speed, 0);
		}
	}
}