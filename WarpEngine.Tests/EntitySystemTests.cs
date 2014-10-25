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
			var ran = false;
			var system = new EntitySystem
			{
				Processor = (e) =>
				{
					Assert.Same(matchingEntity, e);
					ran = true;
				},
				Filter = e => e.Has<ComponentA>()
			};

			// Run our system
			system.Process(root);

			// Make sure our test was run
			Assert.True(ran);
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
			var ran = false;
			var system = new EntitySystem
			{
				Processor = e =>
				{
					Assert.Same(matchingEntity, e);
					ran = true;
				},
				Filter = e => e.Has<ComponentA>()
			};

			// Run our system
			system.Process(root);

			// Make sure our test was run
			Assert.True(ran);
		}
	}
}