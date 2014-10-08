using OpenTK;
using WarpEngine;

namespace Warpcore
{
	public class PlayerSystem : EntitySystem
	{
		public PlayerSystem()
		{
			Filter = e => e.Has<TransformComponent>() && e.Has<PlayerComponent>();
		}

		public override void ProcessEntity(Entity entity)
		{
			var transform = entity.Get<TransformComponent>();
			var player = entity.Get<PlayerComponent>();

			transform.Position += new Vector2(player.Speed, 0);
		}
	}
}