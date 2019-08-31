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
		public bool allowWallJump => hasWall;

		public bool isGrounded => states.movement.character.collisionState.down;
		public bool hasWall => states.movement.character.collisionState.left || states.movement.character.collisionState.right;

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

			CharacterStateBase.SetActive(states.gravity, allowGravity);
			CharacterStateBase.SetActive(states.input, allowInput);
			CharacterStateBase.SetActive(states.motor, allowMotor);
			CharacterStateBase.SetActive(states.movement, allowMovement);

			CharacterStateBase.UpdateState(states.gravity);
			CharacterStateBase.UpdateState(states.input);

			CharacterStateBase.UpdateState(states.jump);
			CharacterStateBase.UpdateState(states.wallJump);

			CharacterStateBase.UpdateState(states.motor);

			CharacterStateBase.UpdateState(states.movement);
		}
	}
}