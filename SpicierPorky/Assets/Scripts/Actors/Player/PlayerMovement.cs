namespace Gypo.SpicierPorky.Actors.Player
{
	using UnityEngine;

	public class PlayerMovement : CharacterState<PlayerController>
	{
		[HideInInspector] public CC2D character;

		[HideInInspector] public Vector2 velocity;

		[SerializeField] private float maxFallSpeed = 30;

		public override void SetReferenceToCharacter(PlayerController parent)
		{
			base.SetReferenceToCharacter(parent);
			parent.states.movement = this;

			character = GetComponent<CC2D>();
		}

		protected override void UpdateState()
		{
			if (velocity.y < -maxFallSpeed)
				velocity.y = -maxFallSpeed;

			velocity = character.Move(velocity * Time.deltaTime);

			if (character.collisionState.down && character.collisionState.wall)
				parent.states.motor.direction = -parent.states.motor.direction;
		}

		protected override void ResetState()
		{
			velocity = Vector2.zero;
		}
	}
}