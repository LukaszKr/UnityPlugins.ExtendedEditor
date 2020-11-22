namespace ProceduralLevel.UnityPluginsEditor.ExtendedEditor
{
	public static class ScrollViewFactory
	{
		public static ScrollView<EntryType> BuildText<EntryType>(this ScrollView<EntryType> view, string text)
		{
			view.AddHeader(new LabelScrollViewComponent<EntryType>(text));
			return view;
		}

		public static ScrollView<EntryType> BuildDefault<EntryType>(this ScrollView<EntryType> view, string name)
		{
			view.BuildText(name);
			view.AddFooter(new SummaryScrollViewComponent<EntryType>());
			return view;
		}
	}
}
