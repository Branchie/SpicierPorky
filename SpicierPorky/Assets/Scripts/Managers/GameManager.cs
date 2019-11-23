namespace Gypo.SpicierPorky
{
	using Gypo.Input;
	using UnityEngine;

	public class GameManager : MonoBehaviour
	{
		private InputProcessor pause = new InputProcessor();
		private InputProcessor reset = new InputProcessor();

		private void Update()
		{
			pause.Update(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape) || Gamepad.GetButton(Gamepad.Button.Start));
			reset.Update(Input.GetKeyDown(KeyCode.R) || Gamepad.GetButton(Gamepad.Button.Y));

			if (reset.onPressed)
				ResetManager.ResetAll();

			if (pause.onPressed)
				SuspensionManager.SetSuspendedAll(PauseManager.Toggle());
		}
	}
}