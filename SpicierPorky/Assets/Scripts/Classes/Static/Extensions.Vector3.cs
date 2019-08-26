namespace Gypo
{
	using UnityEngine;

	public static partial class Extensions
	{
		public static Vector3 Ceil(this Vector3 v, float precision)
		{
			return new Vector3(
				v.x.Ceil(precision),
				v.y.Ceil(precision),
				v.z.Ceil(precision)
			);
		}

		public static Vector3 Ceil(this Vector3 v, Vector3 precision)
		{
			return new Vector3(
				v.x.Ceil(precision.x),
				v.y.Ceil(precision.y),
				v.z.Ceil(precision.z)
			);
		}

		public static Vector3 Floor(this Vector3 v, float precision)
		{
			return new Vector3(
				v.x.Floor(precision),
				v.y.Floor(precision),
				v.z.Floor(precision)
			);
		}

		public static Vector3 Floor(this Vector3 v, Vector3 precision)
		{
			return new Vector3(
				v.x.Floor(precision.x),
				v.y.Floor(precision.y),
				v.z.Floor(precision.z)
			);
		}

		public static Vector3 Round(this Vector3 v, float precision)
		{
			return new Vector3(
				v.x.Round(precision),
				v.y.Round(precision),
				v.z.Round(precision)
			);
		}

		public static Vector3 Round(this Vector3 v, Vector3 precision)
		{
			return new Vector3(
				v.x.Round(precision.x),
				v.y.Round(precision.y),
				v.z.Round(precision.z)
			);
		}

		public static Vector2Int ToVector2Int(this Vector3 v)
		{
			return new Vector2Int(
				(int)v.x,
				(int)v.y
			);
		}

		public static Vector3Int ToVector3Int(this Vector3 v)
		{
			return new Vector3Int(
				(int)v.x,
				(int)v.y,
				(int)v.z
			);
		}
	}
}