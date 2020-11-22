namespace ProceduralLevel.UnityPluginsEditor.ExtendedEditor
{
	public struct LayoutData
	{
		public readonly float X;
		public readonly float Y;
		public readonly float Width;

		public LayoutData(float x, float y, float width)
		{
			X = x;
			Y = y;
			Width = width;
		}

		public override string ToString()
		{
			return string.Format("[({0}, {1}), Width: {2}]", X.ToString(), Y.ToString(), Width.ToString());
		}
	}
}
