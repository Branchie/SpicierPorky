namespace Gypo.SpicierPorky.Actors.Player
{
	using UnityEngine;

	public class PlayerWallDash : CharacterState<PlayerController>
	{
		[SerializeField] private float duration = 1;
		[SerializeField] private float speed = 5;

		private float activeTime;

		public override void SetReferenceToCharacter(PlayerController parent)
		{
			base.SetReferenceToCharacter(parent);
			parent.states.wallDash = this;
		}

		protected override void OnEnter()
		{
			activeTime = 0;

			parent.states.movement.velocity.y = -speed;
			parent.states.slide.Activate();
		}

		protected override void UpdateState()
		{
			if (!parent.logic.isWallSlide || ((activeTime += Time.deltaTime) > duration))
				Deactivate();
			else
				parent.states.movement.maxFallSpeed = speed;
		}

		protected override void ResetState()
		{
			Deactivate();
		}
	}
}