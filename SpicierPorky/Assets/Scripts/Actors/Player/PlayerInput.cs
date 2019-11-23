namespace Gypo.SpicierPorky.Actors.Player
{
	using UnityEngine;

	public class PlayerInput : CharacterState<PlayerController>
	{
		private Inputs.Player input => Inputs.players[parent.playerId];

		public override void SetReferenceToCharacter(PlayerController parent)
		{
			base.SetReferenceToCharacter(parent);
			parent.states.input = this;
		}

		protected override void UpdateState()
		{
			if (input.slide.onPressed)
			{
				if (parent.logic.allowSlide)
					parent.states.slide.Activate();
				else if (parent.logic.allowWallDash)
					parent.states.wallDash.Activate();
			}

			if (input.jump.onPressed)
			{
				if (parent.logic.allowJump)
					parent.states.jump.Activate();
				else if (parent.logic.allowWallJump)
					parent.states.wallJump.Activate();
			}
		}
	}
}