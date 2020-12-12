using UnityEngine;

namespace ProceduralLevel.UnityPluginsEditor.ExtendedEditor
{
	public abstract class APropertyAttributeDrawer<PropertyType>: AExtendedPropertyDrawer
		where PropertyType : PropertyAttribute
	{
		public PropertyType Attribute { get { return attribute as PropertyType; } }
	}
}
