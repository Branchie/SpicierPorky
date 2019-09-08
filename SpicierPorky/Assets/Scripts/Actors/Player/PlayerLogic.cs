namespace Gypo.SpicierPorky.Actors.Player
{
	using UnityEngine;

	public class PlayerLogic : MonoBehaviour, ICharacterReceiver<PlayerController>
	{
		private PlayerController parent;
		private PlayerStates states;

		public bool allowGraphic => true;
		public bool allowGravity => true;
		public bool allowInput => true;
		public bool allowJump => isGrounded;
		public bool allowMotor => true;
		public bool allowMovement => true;
		public bool allowSlide => isGrounded && (!isSliding || states.slide.canQueue);
		public bool allowWallJump => hasWall;
		public bool allowWallSlide => hasWall;

		public bool hasWall => states.movement.character.collisionState.left || states.movement.character.collisionState.right;
		public bool isGrounded => states.movement.character.collisionState.down;
		public bool isSliding => states.slide.active;
		public bool isWallSlide => states.wallSlide.active;

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

			CharacterStateBase.UpdateState(states.jump);
			CharacterStateBase.UpdateState(states.slide);
			CharacterStateBase.UpdateState(states.wallJump);

			CharacterStateBase.UpdateState(states.wallSlide);
			CharacterStateBase.UpdateState(states.motor);

			CharacterStateBase.UpdateState(states.movement);
			CharacterStateBase.UpdateState(states.graphic);
		}
	}
}