using Moq;
using Xunit;

namespace WarpEngine.Tests
{
	public class EntitySystemTests
	{
		[Fact]
		public void ProcessTree_MatchingEntity_Processes()
		{
			// Set up our tree
			var root = new Entity();

			var matchingEntity = new Entity();
			matchingEntity.Components.Add(new ComponentA());
			root.Children.Add(matchingEntity);

			var clashingEntity = new Entity();
			clashingEntity.Components.Add(new ComponentB());
			root.Children.Add(clashingEntity);

			// Set up our system
			var system = new Mock<EntitySystem>();
			var processFunc = system.Setup(s => s.ProcessEntity(It.IsAny<Entity>()));
			processFunc.Callback<Entity>(e => Assert.Same(matchingEntity, e));

			system.Object.Filter = e => e.Has<ComponentA>();

			// Run our system
			system.Object.ProcessTree(root);

			// Make sure our test was run
			system.Verify(s => s.ProcessEntity(It.IsAny<Entity>()), Times.Once);
		}

		[Fact]
		public void ProcessTree_NestedMatchingEntity_Processes()
		{
			// Set up our tree
			var root = new Entity();

			var clashingEntity = new Entity();
			clashingEntity.Components.Add(new ComponentB());
			root.Children.Add(clashingEntity);

			// Add our matching entity to the clashing entity's children
			var matchingEntity = new Entity();
			matchingEntity.Components.Add(new ComponentA());
			clashingEntity.Children.Add(matchingEntity);

			// Set up our system
			var system = new Mock<EntitySystem>();
			var processFunc = system.Setup(s => s.ProcessEntity(It.IsAny<Entity>()));
			processFunc.Callback<Entity>(e => Assert.Same(matchingEntity, e));

			system.Object.Filter = e => e.Has<ComponentA>();

			// Run our system
			system.Object.ProcessTree(root);

			// Make sure our test was run
			system.Verify(s => s.ProcessEntity(It.IsAny<Entity>()), Times.Once);
		}
	}
}