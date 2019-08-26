namespace Gypo
{
	using UnityEngine;

	public static class GizmosHelper
	{
		public static void DrawCircle(Vector2 center, float radius)					=> DrawCircle(center, radius, 16, Color.white);
		public static void DrawCircle(Vector2 center, float radius, Color color)	=> DrawCircle(center, radius, 16, color);
		public static void DrawCircle(Vector2 center, float radius, int steps)		=> DrawCircle(center, radius, steps, Color.white);
		public static void DrawCircle(Vector2 center, float radius, int steps, Color color)
		{
			Vector2 dir = Vector2.up * radius;
			Vector2 point = center + dir;
			float step = 360f / steps;

			Gizmos.color = color;
			for (int i = 0; i < steps; i++)
			{
				dir = dir.Rotate(step);
				Vector2 b = center + dir;

				Gizmos.DrawLine(point, b);
				point = b;
			}
		}

		public static void DrawBox(Vector2 min, Vector2 max) => DrawBox(min, max, Color.white);
		public static void DrawBox(Vector2 min, Vector2 max, Color color)
		{
			Gizmos.color = color;
			Gizmos.DrawLine(new Vector3(min.x, min.y), new Vector3(min.x, max.y));
			Gizmos.DrawLine(new Vector3(min.x, max.y), new Vector3(max.x, max.y));
			Gizmos.DrawLine(new Vector3(max.x, max.y), new Vector3(max.x, min.y));
			Gizmos.DrawLine(new Vector3(min.x, min.y), new Vector3(max.x, min.y));
		}
	}
}