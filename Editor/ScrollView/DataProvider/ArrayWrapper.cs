using System;

namespace ProceduralLevel.UnityPluginsEditor.ExtendedEditor
{
	public class ArrayWrapper<EntryType>: IDataProvider<EntryType>
	{
		public int Count { get { return m_Array.Length; } }
		public EntryType this[int index] { get { return m_Array[index]; } }

		private EntryType[] m_Array;

		public ArrayWrapper(EntryType[] array)
		{
			m_Array = array;
		}

		public void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}
	}
}
