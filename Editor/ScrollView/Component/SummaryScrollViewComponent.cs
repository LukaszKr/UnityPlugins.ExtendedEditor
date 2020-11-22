using ProceduralLevel.UnityPluginsEditor.ExtendedEditor;
using UnityEngine;

namespace ProceduralLevel.UnityPluginsEditor.ExtendedEditor
{
	public class SummaryScrollViewComponent<EntryType>: AScrollViewComponent<EntryType>
	{
		private const string FORMAT = "Displayed: {0}/{1}";

		public override float Draw(float yOffset, float width, bool render)
		{
			float height = ScrollViewStyles.Background.TotalLineHeight();
			if(render)
			{
				GUI.Label(new Rect(0, yOffset, width, height), string.Format(FORMAT, m_ScrollView.DisplayedCount, m_ScrollView.Collection.Count), ScrollViewStyles.Background);
			}
			return height;
		}
	}
}
