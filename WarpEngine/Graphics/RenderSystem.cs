using System;

namespace WarpEngine.Graphics
{
	public class RenderSystem : EntitySystem
	{
		public RenderSystem()
		{
			Filter = e => e.Has<TransformComponent>();
		}

		public override void ProcessEntity(Entity entity)
		{
			Console.WriteLine(entity.Get<TransformComponent>().Position);
		}
	}
}