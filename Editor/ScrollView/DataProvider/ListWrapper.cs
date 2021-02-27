using System.Collections.Generic;

namespace ProceduralLevel.UnityPlugins.ExtendedEditor.Editor
{
	public class ListWrapper<EntryType>: IDataProvider<EntryType>
	{
		public int Count { get { return m_List.Count; } }
		public EntryType this[int index] { get { return m_List[index]; } }

		private List<EntryType> m_List;

		public ListWrapper(List<EntryType> list)
		{
			m_List = list;
		}

		public void RemoveAt(int index)
		{
			m_List.RemoveAt(index);
		}
	}
}
