namespace Gypo.SpicierPorky.Actors.Player
{
	using UnityEngine;

	public class PlayerLogic : MonoBehaviour, ICharacterReceiver<PlayerController>
	{
		private PlayerController parent;
		private PlayerStates states;

		public bool allowGraphic		=> true;
		public bool allowGravity		=> true;
		public bool allowInput			=> true;
		public bool allowJump			=> isGrounded && !hasCeiling;
		public bool allowKnockback		=> isGrounded && hasWallCollision;
		public bool allowMotor			=> !isIdle;
		public bool allowMovement		=> true;
		public bool allowSlide			=> isGrounded && (!isSliding || states.slide.canQueue);
		public bool allowWallDash		=> isWallSlide && !isWallDash;
		public bool allowWallJump		=> hasWall;
		public bool allowWallSlide		=> hasWall;

		public bool hasCeiling			=> states.movement.character.CheckVertical(0.25f);
		public bool hasWall				=> collisionState.left || collisionState.right;
		public bool hasWallCollision	=> (direction > 0 && collisionState.right) || (direction < 0 && collisionState.left);
		public bool isIdle				=> states.idle.active;
		public bool isGrounded			=> states.movement.character.collisionState.down;
		public bool isKnockback			=> states.knockback.active;
		public bool isSliding			=> states.slide.active;
		public bool isWallDash			=> states.wallDash.active;
		public bool isWallSlide			=> states.wallSlide.active;
		public int direction			=> states.motor.direction;

		public CC2D.CollisionState collisionState => states.movement.character.collisionState;

		public void SetReferenceToCharacter(PlayerController parent)
		{
			this.parent = parent;
			parent.logic = this;

			states = parent.states;
		}

		private void Update()
		{
			if (parent.isSuspended)
				return;

			CharacterStateBase.SetActive(states.graphic, allowGraphic);
			CharacterStateBase.SetActive(states.gravity, allowGravity);
			CharacterStateBase.SetActive(states.input, allowInput);
			CharacterStateBase.SetActive(states.motor, allowMotor);
			CharacterStateBase.SetActive(states.movement, allowMovement);
			CharacterStateBase.SetActive(states.wallSlide, allowWallSlide);

			CharacterStateBase.UpdateState(states.gravity);
			CharacterStateBase.UpdateState(states.input);
			CharacterStateBase.UpdateState(states.wallSlide);

			CharacterStateBase.UpdateState(states.jump);
			CharacterStateBase.UpdateState(states.knockback);
			CharacterStateBase.UpdateState(states.slide);
			CharacterStateBase.UpdateState(states.wallDash);
			CharacterStateBase.UpdateState(states.wallJump);

			CharacterStateBase.UpdateState(states.motor);

			CharacterStateBase.UpdateState(states.movement);
			CharacterStateBase.UpdateState(states.graphic);
		}
	}
}