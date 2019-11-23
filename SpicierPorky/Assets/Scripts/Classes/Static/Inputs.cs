namespace Gypo.SpicierPorky
{
	public static class Inputs
	{
		[System.Serializable]
		public class Player
		{
			public InputProcessor jump = new InputProcessor();
			public InputProcessor slide = new InputProcessor();
		}

		public static Player[] players = new Player[]
		{
			new Player()
		};

		public static Player player => players[0];
	}
}