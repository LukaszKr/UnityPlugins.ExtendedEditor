using ProceduralLevel.UnityPlugins.Common;
using UnityEngine;

namespace ProceduralLevel.UnityPluginsEditor.ExtendedEditor
{
	public static class ScrollViewStyles
	{
		public static ExtendedGUIStyle Background = new ExtendedGUIStyle("box", (s) =>
		{
			Texture2D texture = StyleHelper.CreateFrameTexture(StyleHelper.SkinColor, StyleHelper.SkinColor.Offset(-25, -25, -25), false);
			StyleHelper.CreateBackgroundStyle(s, texture);
		});

		public static ExtendedGUIStyle ListLabel = new ExtendedGUIStyle("label", (s) =>
		{
			s.alignment = TextAnchor.MiddleCenter;
		});
	}
}
