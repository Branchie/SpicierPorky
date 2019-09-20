namespace Gypo.SpicierPorky
{
	using UnityEngine;

	public class GameManager : MonoBehaviour
	{
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.R))
				ResetManager.ResetAll();

			if (Input.GetKeyDown(KeyCode.P))
				SuspensionManager.SetSuspendedAll(PauseManager.Toggle());
		}
	}
}