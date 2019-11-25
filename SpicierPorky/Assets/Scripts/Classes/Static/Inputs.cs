namespace Gypo.SpicierPorky
{
	public static class Inputs
	{
		[System.Serializable]
		public class Player
		{
			public InputProcessor flip = new InputProcessor();
			public InputProcessor join = new InputProcessor();
			public InputProcessor jump = new InputProcessor();
			public InputProcessor slide = new InputProcessor();
		}

		public static Player[] players = new Player[]
		{
			new Player(),
			new Player(),
			new Player(),
			new Player(),
		};

		public static Player player => players[0];
	}
}