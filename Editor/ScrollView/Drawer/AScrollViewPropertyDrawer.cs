using UnityEditor;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.ExtendedEditor.Editor
{
	public class AScrollViewPropertyDrawer: AExtendedPropertyDrawer
	{
		public const float DEFAULT_HEIGHT = 200f;

		private SerializedPropertyScrollView m_View;

		protected virtual float Height { get { return DEFAULT_HEIGHT; } }

		protected override void Draw(Rect position, SerializedProperty property, GUIContent label)
		{
			if(m_View == null)
			{
				m_View = new SerializedPropertyScrollView(property);
			}
			m_View.Draw(position);
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return Height;
		}
	}
}
