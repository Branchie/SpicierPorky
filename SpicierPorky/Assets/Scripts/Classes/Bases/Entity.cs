namespace Gypo
{
	using UnityEngine;

	public class Entity : MonoBehaviour
	{
		public Vector2 position
		{
			get => transform.position;
			set => transform.position = value;
		}
	}
}