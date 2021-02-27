namespace ProceduralLevel.UnityPlugins.ExtendedEditor.Editor
{
	public interface ILayoutProvider
	{
		void Prepare(float width); //precalculate stuff if needed for specific width
		LayoutData GetNext(float lastHeight);

		void Reset();
	}
}
