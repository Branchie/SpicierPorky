namespace Gypo.SpicierPorky.Actors.Player
{
	using UnityEngine;

	public class PlayerKnockback : CharacterState<PlayerController>
	{
		[SerializeField] private float force = 14;
		[SerializeField] private float duration = 0.7f;

		private float activeTime;

		public override void SetReferenceToCharacter(PlayerController parent)
		{
			base.SetReferenceToCharacter(parent);
			parent.states.knockback = this;
		}

		public override void Init()
		{
			parent.states.slide.onEnterEvent += Deactivate;
			parent.states.jump.onJump += Deactivate;
		}

		protected override void OnEnter()
		{
			parent.states.movement.velocity.x = parent.states.motor.direction * -force;
			activeTime = 0;
		}

		protected override void UpdateState()
		{
			parent.states.motor.speed.maxSpeed = 0;

			if ((activeTime += Time.deltaTime) >= duration)
				Deactivate();
		}

		protected override void ResetState()
		{
			Deactivate();
		}
	}
}