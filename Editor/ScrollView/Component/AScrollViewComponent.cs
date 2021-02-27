namespace ProceduralLevel.UnityPlugins.ExtendedEditor.Editor
{
	public abstract class AScrollViewComponent<EntryType>
	{
		protected ScrollView<EntryType> m_ScrollView;

		public void SetScrollView(ScrollView<EntryType> scrollView)
		{
			m_ScrollView = scrollView;
		}

		public abstract float Draw(float yOffset, float width, bool render);
	}
}
