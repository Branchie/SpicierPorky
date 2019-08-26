namespace Gypo
{
	using UnityEngine;

	public static partial class Extensions
	{
		public static T GetOrAddComponent<T>(this GameObject g) where T : Component
		{
			if (!g.TryGetComponent(out T result))
				result = g.AddComponent<T>();

			return result;
		}

		public static bool TryGetComponent<T>(this GameObject g, out T result) where T : Component
		{
			result = g.GetComponent<T>();
			return result != null;
		}
	}
}