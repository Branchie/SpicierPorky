namespace Gypo.SpicierPorky.Managers
{
	using Gypo.Input;
	using UnityEngine;
	using Gypo.SpicierPorky.Actors.Camera;
	using Gypo.SpicierPorky.Actors.Player;

	public class GameManager : MonoBehaviour
	{
		private InputProcessor pause = new InputProcessor();
		private InputProcessor reset = new InputProcessor();

		private void Update()
		{
			// pause.Update(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape) || Gamepad.GetButton(Gamepad.Button.Start));
			reset.Update(Input.GetKeyDown(KeyCode.R) || Gamepad.GetButton(Gamepad.Button.Y));

			if (reset.onPressed)
				ResetManager.ResetAll();

			if (pause.onPressed)
				SuspensionManager.SetSuspendedAll(PauseManager.Toggle());

			for (int i = 0; i < Inputs.players.Length; i++)
				if (Inputs.players[i].join.onPressed)
					SpawnPlayer(i);
		}

		private void SpawnPlayer(int index)
		{
			foreach (PlayerController p in Game.players)
				if (p?.playerID == index)
					return;

			GameObject spawn = GameObject.Find("Spawn");
			Transform holder = new GameObject($"Player: {index}").transform;

			PlayerController player = PrefabManager.Spawn(Prefab.Player, spawn.transform.position).GetComponent<PlayerController>();
			player.SetPlayerID(index);
			player.transform.SetParent(holder);

			CameraController camera = PrefabManager.Spawn(Prefab.Camera, spawn.transform.position).GetComponent<CameraController>();
			camera.SetTarget(player.gameObject);
			camera.transform.SetParent(holder);

			Canvas ui = PrefabManager.Spawn(Prefab.GameUI, spawn.transform.position).GetComponent<Canvas>();
			ui.worldCamera = camera.cam;
			ui.transform.SetParent(holder);
		}
	}
}