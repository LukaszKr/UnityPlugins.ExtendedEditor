using UnityEditor;

namespace ProceduralLevel.UnityPluginsEditor.ExtendedEditor
{
	internal class SerializedPropertyWrapper: IDataProvider<SerializedProperty>
	{
		public int Count { get { return m_Property.arraySize; } }
		public SerializedProperty this[int index] { get { return m_Property.GetArrayElementAtIndex(index); } }

		private SerializedProperty m_Property;

		public SerializedPropertyWrapper(SerializedProperty property)
		{
			m_Property = property;
		}

		public void RemoveAt(int index)
		{
			m_Property.DeleteArrayElementAtIndex(index);
		}
	}
}
