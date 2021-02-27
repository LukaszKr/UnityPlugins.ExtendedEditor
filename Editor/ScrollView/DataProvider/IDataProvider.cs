namespace ProceduralLevel.UnityPlugins.ExtendedEditor.Editor
{
	public interface IDataProvider<EntryType>
	{
		int Count { get; }
		EntryType this[int index] { get; }

		void RemoveAt(int index);
	}
}
