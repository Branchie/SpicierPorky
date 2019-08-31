namespace Gypo.SpicierPorky.Actors.Player
{
	using UnityEngine;

	public class PlayerGravity : CharacterState<PlayerController>
	{
		public override void SetReferenceToCharacter(PlayerController parent)
		{
			base.SetReferenceToCharacter(parent);
			parent.states.gravity = this;
		}

		protected override void UpdateState()
		{
			parent.states.movement.velocity += Physics2D.gravity * Time.deltaTime;
		}
	}
}