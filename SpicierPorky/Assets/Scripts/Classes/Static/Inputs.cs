namespace Gypo.SpicierPorky
{
	using UnityEngine;

	public static class Inputs
	{
		public class Player
		{
			public InputProcessor jump = new InputProcessor();

			public Vector2 movement;
		}

		public static Player[] players = new Player[]
		{
			new Player()
		};

		public static Player player => players[0];
	}
}