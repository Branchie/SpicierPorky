namespace Gypo.SpicierPorky
{
	using UnityEngine;

	public class GameManager : MonoBehaviour
	{
		private bool paused = false;

		private void Awake()
		{
			if (GetComponentInChildren<InputManager>() == null)
				gameObject.AddComponent<InputManager>();
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.R))
				ResetManager.ResetAll();

			if (Input.GetKeyDown(KeyCode.P))
				SuspensionManager.SetSuspendedAll(paused = !paused);
		}
	}
}