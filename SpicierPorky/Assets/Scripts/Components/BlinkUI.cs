namespace Gypo.SpicierPorky
{
	using UnityEngine;
	using UnityEngine.UI;

	public class BlinkUI : MonoBehaviour
	{
		[SerializeField] private float interval = 0.25f;

		private Graphic rend = default;

		private float activeTime;

		private void Awake()
		{
			rend = GetComponent<Graphic>();
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