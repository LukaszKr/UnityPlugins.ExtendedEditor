using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.ExtendedEditor.Editor
{
	public class ScrollView<EntryType>
	{
		private const int REDRAW_LIMIT = 1000;

		private Vector2 m_ScrollPosition;
		private List<ScrollEntry<EntryType>> m_Entries = new List<ScrollEntry<EntryType>>();

		private float m_TotalHeight = 0;
		private int m_DisplayedCount = 0;

		public int DisplayedCount { get { return m_DisplayedCount; } }

		protected GUIStyle VerticalScrollStyle { get { return GUI.skin.verticalScrollbar; } }
		protected float LineHeight { get { return EditorGUIUtility.singleLineHeight; } }

		public readonly IDataProvider<EntryType> Collection;

		private List<AScrollViewComponent<EntryType>> m_Headers = new List<AScrollViewComponent<EntryType>>();
		private List<AScrollViewComponent<EntryType>> m_Footers = new List<AScrollViewComponent<EntryType>>();

		public ILayoutProvider Layout = new ColumnLayoutProvider(1, true);

		public ScrollView(IDataProvider<EntryType> wrapper)
		{
			Collection = wrapper;
		}

		public ScrollView(List<EntryType> list)
		{
			Collection = new ListWrapper<EntryType>(list);
		}

		public ScrollView(EntryType[] array)
		{
			Collection = new ArrayWrapper<EntryType>(array);
		}

		#region Draw
		public void Draw(Rect position)
		{
			Event currentEvent = Event.current;
			if(currentEvent != null && currentEvent.type == EventType.Layout)
			{
				return;
			}
			EventType eventType = currentEvent.type;
			bool isMouse = currentEvent.isMouse;

			GUI.BeginGroup(position);

			int sourceCount = Collection.Count;
			AdjustItemCount();

			float scrollWidth = VerticalScrollStyle.fixedWidth;
			float maxWidth = position.width-scrollWidth;

			float headerHeight = DrawHeaders(position.width, true);
			float footerHeight = DrawFooters(position.width, position.height, true);

			float maxHeight = position.height-headerHeight-footerHeight;
			float scrollViewHeight = Mathf.Max(m_TotalHeight, maxHeight);

			Rect scrollPositionRect = new Rect(0, headerHeight, position.width, maxHeight);
			Rect scrollViewRect = new Rect(0, 0, maxWidth, scrollViewHeight);
			GUI.Box(scrollPositionRect, "", ScrollViewStyles.Background);
			m_ScrollPosition = GUI.BeginScrollView(scrollPositionRect, m_ScrollPosition, scrollViewRect, false, true);

			float minY = m_ScrollPosition.y;
			float maxY = minY+maxHeight;
			m_TotalHeight = 0;
			m_DisplayedCount = 0;
			int redrawRemaining = REDRAW_LIMIT; //prevent massive lag on initialization in case of 20k+ lists

			Layout.Prepare(maxWidth);
			LayoutData nextPosition = new LayoutData(0, 0, 0);
			float lastHeight = 0;

			for(int x = 0; x < sourceCount; x++)
			{
				ScrollEntry<EntryType> scrollEntry = m_Entries[x];
				EntryType currentEntry = scrollEntry.Entry;
				//check if order or item at index wasn't changed
				EntryType collectionItem = Collection[x];
				if(!Equals(currentEntry, collectionItem))
				{
					scrollEntry.Entry = collectionItem;
					currentEntry = collectionItem;
					scrollEntry.IsDirty = true;
				}

				nextPosition = Layout.GetNext(lastHeight);

				bool isVisible = nextPosition.Y <= maxY && nextPosition.Y+scrollEntry.Height >= minY;

				if(isVisible || (redrawRemaining > 0 && scrollEntry.IsDirty))
				{
					if(isVisible)
					{
						++m_DisplayedCount;
						if(isMouse && eventType == EventType.MouseUp)
						{
							HandleMouse(currentEntry, new Rect(nextPosition.X, nextPosition.Y, nextPosition.Width, scrollEntry.Height));
						}
					}
					else
					{
						--redrawRemaining;
					}
					scrollEntry.Height = DrawItem(nextPosition, currentEntry, isVisible);
					scrollEntry.IsDirty = false;
				}
				lastHeight = scrollEntry.Height;
			}
			m_TotalHeight = nextPosition.Y+lastHeight;

			GUI.EndScrollView();
			GUI.EndGroup();
		}

		private float DrawHeaders(float width, bool isRepaint)
		{
			float headerHeight = 0f;
			int headerCount = m_Headers.Count;
			for(int x = 0; x != headerCount; ++x)
			{
				headerHeight += m_Headers[x].Draw(headerHeight, width, isRepaint);
			}

			return headerHeight;
		}

		private float DrawFooters(float width, float height, bool isRepaint)
		{
			float footerHeight = 0f;
			int footerCount = m_Footers.Count;
			for(int x = 0; x != footerCount; ++x)
			{
				footerHeight += m_Footers[x].Draw(footerHeight, width, false);
			}

			if(isRepaint)
			{
				float hOffset = 0f;
				for(int x = 0; x != footerCount; ++x)
				{
					hOffset += m_Footers[x].Draw(height-footerHeight+hOffset, width, true);
				}
			}
			return footerHeight;
		}

		protected virtual float DrawItem(LayoutData layoutPosition, EntryType entry, bool render)
		{
			Rect position = new Rect(layoutPosition.X, layoutPosition.Y, layoutPosition.Width, EditorGUIUtility.singleLineHeight);
			if(render)
			{
				EditorGUI.LabelField(position, (entry != null ? entry.ToString() : "Null"));
			}
			return position.height;
		}
		#endregion

		#region Input
		protected virtual void HandleMouse(EntryType entry, Rect position)
		{
			Event currentEvent = Event.current;
			if(currentEvent != null)
			{
				Vector2 mousePosition = currentEvent.mousePosition;
				if(position.Contains(mousePosition))
				{
					if(currentEvent.type == EventType.MouseUp)
					{
						if(currentEvent.shift)
						{
							OnShiftClick(entry, mousePosition);
						}
						else
						{
							if(currentEvent.button == 0)
							{
								OnClick(entry, mousePosition);
							}
							else if(currentEvent.button == 1)
							{
								OnContextClick(entry, mousePosition);
							}
						}
					}
					else if(currentEvent.type == EventType.ContextClick)
					{
						OnContextClick(entry, mousePosition);
					}
				}
			}
		}

		protected virtual void OnClick(EntryType entry, Vector2 clickPosition) { }
		protected virtual void OnShiftClick(EntryType entry, Vector2 clickPosition) { }
		protected virtual void OnContextClick(EntryType entry, Vector2 clickPosition) { }
		#endregion

		#region Componentns
		public ScrollView<EntryType> AddHeader(params AScrollViewComponent<EntryType>[] headers)
		{
			m_Headers.AddRange(headers);
			for(int x = 0; x != headers.Length; ++x)
			{
				headers[x].SetScrollView(this);
			}
			return this;
		}

		public ScrollView<EntryType> AddFooter(params AScrollViewComponent<EntryType>[] footers)
		{
			m_Footers.AddRange(footers);
			for(int x = 0; x != footers.Length; ++x)
			{
				footers[x].SetScrollView(this);
			}
			return this;
		}
		#endregion

		public void MarkAsDirty()
		{
			for(int x = 0; x < m_Entries.Count; ++x)
			{
				m_Entries[x].IsDirty = true;
			}
		}

		private void AdjustItemCount()
		{
			int sourceCount = Collection.Count;

			while(sourceCount > m_Entries.Count)
			{
				m_Entries.Add(new ScrollEntry<EntryType>());
			}
			while(sourceCount < m_Entries.Count)
			{
				m_Entries.RemoveAt(m_Entries.Count-1);
			}
		}
	}
}
