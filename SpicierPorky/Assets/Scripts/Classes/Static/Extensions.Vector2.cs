namespace Gypo
{
	using UnityEngine;

	public static partial class Extensions
	{
		public static bool Approximately(this Vector2 a, Vector2 b)
		{
			return
				Mathf.Approximately(a.x, b.x) &&
				Mathf.Approximately(a.y, b.y);
		}

		public static bool Approximately(this Vector2 a, Vector2 b, float fudge)
		{
			return
				Mathf.Abs(a.x - b.x) < fudge &&
				Mathf.Abs(a.y - b.y) < fudge;
		}

		public static Vector2 Rotate(this Vector2 v, float degrees)
		{
			return RotateRad(v, degrees * Mathf.Deg2Rad);
		}

		public static Vector2 RotateRad(this Vector2 v, float radians)
		{
			float cos = Mathf.Cos(radians);
			float sin = Mathf.Sin(radians);

			return new Vector2(
				cos * v.x - sin * v.y,
				sin * v.x + cos * v.y
			);
		}

		public static Vector2 Ceil(this Vector2 v, float precision)
		{
			return new Vector2(
				v.x.Ceil(precision),
				v.y.Ceil(precision)
			);
		}

		public static Vector2 Ceil(this Vector2 v, Vector2 precision)
		{
			return new Vector2(
				v.x.Ceil(precision.x),
				v.y.Ceil(precision.y)
			);
		}

		public static Vector2 Floor(this Vector2 v, float precision)
		{
			return new Vector2(
				v.x.Floor(precision),
				v.y.Floor(precision)
			);
		}

		public static Vector2 Floor(this Vector2 v, Vector2 precision)
		{
			return new Vector2(
				v.x.Floor(precision.x),
				v.y.Floor(precision.y)
			);
		}

		public static Vector2 Round(this Vector2 v, float precision)
		{
			return new Vector2(
				v.x.Round(precision),
				v.y.Round(precision)
			);
		}

		public static Vector2 Round(this Vector2 v, Vector2 precision)
		{
			return new Vector2(
				v.x.Round(precision.x),
				v.y.Round(precision.y)
			);
		}

		public static Vector2Int ToVector2Int(this Vector2 v)
		{
			return new Vector2Int(
				(int)v.x,
				(int)v.y
			);
		}

		public static Vector3Int ToVector3Int(this Vector2 v, int z = 0)
		{
			return new Vector3Int(
				(int)v.x,
				(int)v.y,
				z
			);
		}
	}
}