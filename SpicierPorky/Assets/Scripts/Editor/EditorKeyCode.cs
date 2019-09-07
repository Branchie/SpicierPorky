namespace Gypo
{
	using UnityEditor;
	using UnityEngine;

	// Fix for Unity 2019.3 not displaying KeyCodes as Enum Popups in the Inspector
	[CustomPropertyDrawer(typeof(KeyCode), true)]
	public class EditorKeyCode : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginChangeCheck();

			KeyCode key = (KeyCode)EditorGUI.EnumPopup(position, label, (KeyCode)property.intValue);

			if (EditorGUI.EndChangeCheck())
				property.intValue = (int)key;
		}
	}
}