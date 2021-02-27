using UnityEditor;

namespace ProceduralLevel.UnityPlugins.ExtendedEditor.Editor
{
	public class SerializedPropertyScrollView: ScrollView<SerializedProperty>
	{
		public SerializedPropertyScrollView(SerializedProperty property)
			: base(new SerializedPropertyWrapper(property))
		{
		}
	}
}
