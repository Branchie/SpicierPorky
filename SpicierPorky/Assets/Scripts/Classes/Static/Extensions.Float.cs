namespace Gypo
{
	using UnityEngine;

	public static partial class Extensions
	{
		public static bool Approximately(this float a, float b, float fudge)
		{
			return Mathf.Abs(a - b) < fudge;
		}

		public static float Ceil(this float f, float precision)
		{
			return Mathf.Ceil(f / precision) * precision;
		}

		public static float Floor(this float f, float precision)
		{
			return Mathf.Floor(f / precision) * precision;
		}

		public static float Round(this float f, float precision)
		{
			return Mathf.Round(f / precision) * precision;
		}
	}
}