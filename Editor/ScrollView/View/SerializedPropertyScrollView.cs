using UnityEditor;

namespace ProceduralLevel.UnityPluginsEditor.ExtendedEditor
{
	public class SerializedPropertyScrollView: ScrollView<SerializedProperty>
	{
		public SerializedPropertyScrollView(SerializedProperty property)
			: base(new SerializedPropertyWrapper(property))
		{
		}
	}
}
