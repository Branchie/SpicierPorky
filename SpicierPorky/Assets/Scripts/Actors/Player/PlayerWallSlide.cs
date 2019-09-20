namespace Gypo.SpicierPorky.Actors.Player
{
	using UnityEngine;

	public class PlayerWallSlide : CharacterState<PlayerController>
	{
		[SerializeField] private float maxFallSpeed = 10f;

		public override void SetReferenceToCharacter(PlayerController parent)
		{
			base.SetReferenceToCharacter(parent);
			parent.states.wallSlide = this;
		}

		protected override void OnEnter()
		{
			if (parent.states.movement.velocity.y > 0)
				parent.states.movement.velocity.y *= 0.9f;
		}

		protected override void UpdateState()
		{
			parent.states.movement.maxFallSpeed = maxFallSpeed;
		}
	}
}