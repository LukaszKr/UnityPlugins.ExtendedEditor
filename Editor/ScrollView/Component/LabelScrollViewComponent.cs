using UnityEngine;

namespace ProceduralLevel.UnityPlugins.ExtendedEditor.Editor
{
	public class LabelScrollViewComponent<EntryType>: AScrollViewComponent<EntryType>
	{
		private readonly string m_Text;

		public LabelScrollViewComponent(string text)
		{
			m_Text = text;
		}

		public override float Draw(float yOffset, float width, bool render)
		{
			float height = ScrollViewStyles.Background.TotalLineHeight();
			if(render)
			{
				GUI.Label(new Rect(0, yOffset, width, height), m_Text, ScrollViewStyles.ListLabel);
			}
			return height;
		}
	}
}
