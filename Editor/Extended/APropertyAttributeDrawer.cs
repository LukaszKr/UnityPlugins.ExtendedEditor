using UnityEngine;

namespace ProceduralLevel.UnityPlugins.ExtendedEditor.Editor
{
	public abstract class APropertyAttributeDrawer<PropertyType>: AExtendedPropertyDrawer
		where PropertyType : PropertyAttribute
	{
		public PropertyType Attribute { get { return attribute as PropertyType; } }
	}
}
