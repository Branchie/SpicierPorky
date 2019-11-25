namespace Gypo.SpicierPorky
{
	public enum Prefab
	{
		Camera,
		GameUI,
		Player,
	}
}

namespace Gypo.SpicierPorky.Data
{
	using UnityEngine;

	[CreateAssetMenu(menuName = Globals.GAME_NAME + "/Collections/Prefabs", fileName = "Prefabs")]
	public class PrefabData : ScriptableObject
	{
		public GameObject camera;
		public GameObject gameUI;
		public GameObject player;

		public GameObject Get(Prefab prefab)
		{
			switch (prefab)
			{
				case Prefab.Camera:
					return camera;

				case Prefab.GameUI:
					return gameUI;

				case Prefab.Player:
					return player;
			}

			return null;
		}
	}
}