namespace Gypo.SpicierPorky.Actors.Player
{
	using UnityEngine;

	public class PlayerController : CharacterBase, IHaveVelocity
	{
		[HideInInspector] public PlayerLogic logic;
		[HideInInspector] public PlayerStates states = new PlayerStates();

		private Vector2 startPosition;

		public int playerId => 0;
		public Vector2 velocity => states.movement.velocity;

		protected override void Awake()
		{
			base.Awake();

			startPosition = position;
		}

		public override void OnReset()
		{
			position = startPosition;

			base.OnReset();
		}
	}
}