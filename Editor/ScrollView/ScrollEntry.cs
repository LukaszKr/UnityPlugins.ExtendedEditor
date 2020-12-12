namespace ProceduralLevel.UnityPluginsEditor.ExtendedEditor
{
	public class ScrollEntry<EntryType>
	{
		public EntryType Entry;
		public float Height;
		public bool IsDirty;

		public override string ToString()
		{
			return string.Format("[IsDirty: {0}, Height: {1}, Entry: {2}]", IsDirty.ToString(), Height.ToString(), (Entry != null ? Entry.ToString() : ""));
		}
	}
}
