using UnityEngine;


namespace ProceduralLevel.UnityPluginsEditor.ExtendedEditor
{
	public static class GUIStyleExt
	{
		public static float TotalLineHeight(this GUIStyle style)
		{
			return style.lineHeight+style.padding.vertical;
		}

		public static float TotalLineHeight(this ExtendedGUIStyle style)
		{
			return style.Style.TotalLineHeight();
		}
	}
}
