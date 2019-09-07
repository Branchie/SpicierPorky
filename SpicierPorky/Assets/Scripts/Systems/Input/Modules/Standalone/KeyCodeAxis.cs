namespace Gypo.Input.Standalone
{
	using UnityEngine;

	[System.Serializable]
	public class KeyCodeAxis : IAxisInput
	{
		[SerializeField] private KeyCode negative = default;
		[SerializeField] private KeyCode positive = default;

		public float Get()
		{
			float val = 0;

			if (Input.GetKey(negative))
				val -= 1;

			if (Input.GetKey(positive))
				val += 1;

			return val;
		}
	}
}

#if UNITY_EDITOR

namespace Gypo.Input.Standalone.Editor
{
	using UnityEditor;
	using UnityEngine;

	[CustomPropertyDrawer(typeof(KeyCodeAxis), true)]
	public class EditorKeyCodeAxis : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.indentLevel++;
			EditorGUIUtility.labelWidth = (EditorGUI.indentLevel * 24) - 24;

			SerializedProperty negative = property.FindPropertyRelative("negative");
			SerializedProperty positive = property.FindPropertyRelative("positive");
			Rect r = position;

			r.width /= 2;
			EditorGUI.PropertyField(r, negative, new GUIContent("-"));

			r.x += r.width;
			EditorGUI.PropertyField(r, positive, new GUIContent("+"));
		}
	}
}

#endif