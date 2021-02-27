namespace ProceduralLevel.UnityPlugins.ExtendedEditor.Editor
{
	public class ColumnLayoutProvider: ILayoutProvider
	{
		private readonly int m_ColCount;
		private readonly bool m_AlignRows;
		private readonly float[] m_Offsets;

		private float m_ColWidth;
		private int m_RowProgress;
		private float m_RowHeight;

		public ColumnLayoutProvider(int colCount, bool alignRows)
		{
			m_ColCount = colCount;
			m_AlignRows = alignRows;
			m_Offsets = new float[colCount];
		}


		public void Prepare(float maxWidth)
		{
			Reset();
			m_ColWidth = maxWidth/m_ColCount;
		}

		public void Reset()
		{
			for(int x = 0; x < m_Offsets.Length; ++x)
			{
				m_Offsets[x] = 0;
			}
			m_RowProgress = 0;
			m_RowHeight = 0;
		}

		public LayoutData GetNext(float lastHeight)
		{
			//avoid dividing
			if(m_AlignRows)
			{
				if(lastHeight > m_RowHeight)
				{
					m_RowHeight = lastHeight;
				}
			}
			else
			{
				int index = (m_RowProgress > 0 ? m_RowProgress-1 : m_Offsets.Length-1);
				m_Offsets[index] += lastHeight;
			}

			if(m_RowProgress == m_ColCount)
			{
				m_RowProgress = 0;
				if(m_AlignRows)
				{
					for(int x = 0; x < m_Offsets.Length; ++x)
					{
						m_Offsets[x] += m_RowHeight;
					}
				}
				else
				{
					m_RowHeight = 0;
				}
			}
			LayoutData data = new LayoutData(m_RowProgress*m_ColWidth, m_Offsets[m_RowProgress], m_ColWidth);
			++m_RowProgress;
			return data;
		}
	}
}
