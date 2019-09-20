namespace Gypo.SpicierPorky.Actors.Player
{
	using UnityEngine;

	public class PlayerWallJump : CharacterState<PlayerController>
	{
		[SerializeField] private float height = 5;

		public override void SetReferenceToCharacter(PlayerController parent)
		{
			base.SetReferenceToCharacter(parent);
			parent.states.wallJump = this;
		}

		public override void Activate()
		{
			int dir = parent.states.movement.character.collisionState.left ? 1 : -1;

			parent.states.motor.direction = dir;
			parent.states.movement.velocity.x = dir * parent.states.motor.speed.maxSpeed;
			parent.states.jump.Jump(height);
		}
	}
}