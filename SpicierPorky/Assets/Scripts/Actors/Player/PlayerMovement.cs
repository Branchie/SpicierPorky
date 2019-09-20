namespace Gypo.SpicierPorky.Actors.Player
{
	using UnityEngine;

	public class PlayerMovement : CharacterState<PlayerController>
	{
		public float maxFallSpeed = 60;

		[HideInInspector] public CC2D character;

		[HideInInspector] public Vector2 velocity;

		private float _maxFallSpeed;

		public override void SetReferenceToCharacter(PlayerController parent)
		{
			base.SetReferenceToCharacter(parent);
			parent.states.movement = this;

			character = GetComponent<CC2D>();

			_maxFallSpeed = maxFallSpeed;
		}

		protected override void UpdateState()
		{
			if (velocity.y < -maxFallSpeed)
				velocity.y = -maxFallSpeed;

			velocity = character.Move(velocity * Time.deltaTime);

			if (parent.logic.allowKnockback)
				parent.states.knockback.Activate();

			maxFallSpeed = _maxFallSpeed;
		}

		protected override void ResetState()
		{
			character.collisionState.Reset();
			velocity = Vector2.zero;
		}
	}
}