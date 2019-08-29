namespace Gypo.SpicierPorky
{
	using UnityEngine;

	[RequireComponent(typeof(RectTransform))]
	public class RectZone : Entity
	{
		public Vector2 center => position;
		public Vector2 max => center + halfSize;
		public Vector2 min => center - halfSize;

		private Vector2 halfSize => ((RectTransform)transform).sizeDelta / 2f;
	}
}