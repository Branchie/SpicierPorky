namespace Gypo.SpicierPorky
{
	using UnityEngine;

	public class Blink : MonoBehaviour
	{
		[SerializeField] private float interval = 0.25f;

		private Renderer rend = default;

		private float activeTime;

		private void Awake()
		{
			rend = GetComponent<Renderer>();
		}

		private void Update()
		{
			activeTime += Time.deltaTime;

			if (activeTime >= interval)
			{
				rend.enabled = !rend.enabled;
				activeTime -= interval;
			}
		}
	}
}