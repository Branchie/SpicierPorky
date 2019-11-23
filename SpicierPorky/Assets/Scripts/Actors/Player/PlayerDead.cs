namespace Gypo.SpicierPorky.Actors.Player
{
	using UnityEngine;

	public class PlayerDead : CharacterState<PlayerController>
	{
		[SerializeField] private float height = 5;

		public override void SetReferenceToCharacter(PlayerController parent)
		{
			base.SetReferenceToCharacter(parent);
			parent.states.dead = this;
		}

		protected override void OnEnter()
		{
			parent.states.movement.character.ignoreCollisions = true;
			parent.states.movement.velocity = Vector2.zero;

			parent.states.jump.Jump(height);
		}

		protected override void UpdateState()
		{

		}

		protected override void OnExit()
		{
			parent.states.movement.character.ignoreCollisions = false;
		}

		protected override void ResetState()
		{
			Deactivate();
		}
	}
}