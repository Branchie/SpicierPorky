namespace Gypo
{
	using UnityEngine;

	public static class DebugHelper
	{
		public static void DrawCircle(Vector2 center, float radius)					=> DrawCircle(center, radius, 16, Color.white, 0);
		public static void DrawCircle(Vector2 center, float radius, Color color)	=> DrawCircle(center, radius, 16, color, 0);
		public static void DrawCircle(Vector2 center, float radius, int steps)		=> DrawCircle(center, radius, steps, Color.white, 0);
		
		public static void DrawCircle(Vector2 center, float radius, float duration)					=> DrawCircle(center, radius, 16, Color.white, duration);
		public static void DrawCircle(Vector2 center, float radius, Color color, float duration)	=> DrawCircle(center, radius, 16, color, duration);
		public static void DrawCircle(Vector2 center, float radius, int steps, float duration)		=> DrawCircle(center, radius, steps, Color.white, duration);

		public static void DrawCircle(Vector2 center, float radius, int steps, Color color, float duration)
		{
			Vector2 dir = Vector2.up * radius;
			Vector2 point = center + dir;
			float step = 360f / steps;

			for (int i = 0; i < steps; i++)
			{
				dir = dir.Rotate(step);
				Vector2 b = center + dir;

				Debug.DrawLine(point, b, color, duration);
				point = b;
			}
		}

		public static void DrawBox(Vector2 min, Vector2 max)					=> DrawBox(min, max, Color.white, 0);
		public static void DrawBox(Vector2 min, Vector2 max, float duration)	=> DrawBox(min, max, Color.white, duration);
		public static void DrawBox(Vector2 min, Vector2 max, Color color)		=> DrawBox(min, max, color, 0);
		public static void DrawBox(Vector2 min, Vector2 max, Color color, float duration)
		{
			Debug.DrawLine(new Vector3(min.x, min.y), new Vector3(min.x, max.y), color, duration);
			Debug.DrawLine(new Vector3(min.x, max.y), new Vector3(max.x, max.y), color, duration);
			Debug.DrawLine(new Vector3(max.x, max.y), new Vector3(max.x, min.y), color, duration);
			Debug.DrawLine(new Vector3(min.x, min.y), new Vector3(max.x, min.y), color, duration);
		}
	}
}