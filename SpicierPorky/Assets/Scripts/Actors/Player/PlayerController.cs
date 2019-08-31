namespace Gypo.SpicierPorky.Actors.Player
{
	using UnityEngine;

	public class PlayerController : CharacterBase, IHaveVelocity
	{
		[HideInInspector] public PlayerLogic logic;
		[HideInInspector] public PlayerStates states = new PlayerStates();

		public int playerId => 0;
		public Vector2 velocity => states.movement.velocity;
	}
}