namespace Gypo.SpicierPorky
{
	using UnityEngine;

	public class GameManager : MonoBehaviour
	{
		private void Awake()
		{
			if (GetComponentInChildren<InputManager>() == null)
				gameObject.AddComponent<InputManager>();
		}
	}
}