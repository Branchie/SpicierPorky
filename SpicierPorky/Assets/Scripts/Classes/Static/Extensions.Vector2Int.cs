namespace Gypo
{
	using UnityEngine;

	public static partial class Extensions
	{
		public static Vector2 ToVector2(this Vector2Int v)
		{
			return new Vector2(
				v.x,
				v.y
			);
		}
	}
}