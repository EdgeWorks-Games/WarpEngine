namespace WarpEngine.Graphics
{
	public class RenderSystem<TTransform> : EntitySystem
		where TTransform : IEntityComponent
	{
		public RenderSystem()
		{
			Filter = e => e.Has<TTransform>();
		}

		public override void ProcessEntity(Entity entity)
		{
			var transform = entity.Get<TTransform>();
		}
	}
}