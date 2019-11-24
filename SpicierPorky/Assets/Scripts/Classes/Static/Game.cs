namespace Gypo.SpicierPorky
{
	using Actors.Camera;
	using Actors.Player;

	public static class Game
	{
		public static event System.Action onCamerasChanged
		{
			add => cameras.onChanged += value;
			remove => cameras.onChanged -= value;
		}

		public static event System.Action onPlayersChanged
		{
			add => players.onChanged += value;
			remove => players.onChanged -= value;
		}

		public static ReusableList<CameraController> cameras = new ReusableList<CameraController>(Globals.MAX_PLAYERS);
		public static ReusableList<PlayerController> players = new ReusableList<PlayerController>(Globals.MAX_PLAYERS);
	}
}