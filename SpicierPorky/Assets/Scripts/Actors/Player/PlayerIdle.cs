namespace Gypo.SpicierPorky.Actors.Player
{
	using UnityEngine;

	public class PlayerIdle : CharacterState<PlayerController>
	{
		public override void SetReferenceToCharacter(PlayerController parent)
		{
			base.SetReferenceToCharacter(parent);
			parent.states.idle = this;
		}

		public override void Init()
		{
			Activate();

			parent.states.jump.onJump += Deactivate;
			parent.states.slide.onEnterEvent += Deactivate;
		}

		protected override void ResetState()
		{
			Activate();
		}
	}
}