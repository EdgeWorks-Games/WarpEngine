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
			var transform = entity.Get<TransformComponent>();
		}
	}
}