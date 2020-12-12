namespace ProceduralLevel.UnityPluginsEditor.ExtendedEditor
{
	public interface IDataProvider<EntryType>
	{
		int Count { get; }
		EntryType this[int index] { get; }

		void RemoveAt(int index);
	}
}
