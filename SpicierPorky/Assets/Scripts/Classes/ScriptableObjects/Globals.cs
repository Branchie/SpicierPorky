namespace Gypo.SpicierPorky
{
	using UnityEngine;

	[CreateAssetMenu(menuName = GAME_NAME + "/Globals", fileName = "Globals")]
	public class Globals : ScriptableObject
	{
		public const int MAX_PLAYERS = 4;
		public const string GAME_NAME = "Spicier Porky";
	}
}