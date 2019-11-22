namespace Gypo.SpicierPorky.Actors.Player
{
	using UnityEngine;

	public class PlayerSlide : CharacterState<PlayerController>
	{
		[SerializeField] private Speed speed = new Speed(5, 5, 15);

		[SerializeField] private float duration = 1;
		[SerializeField] private float landExtension = 0.25f;
		[SerializeField] private float tunnelExtension = 0.25f;

		private float activeTime;

		public bool canQueue => activeTime >= duration - 0.2f;

		public override void SetReferenceToCharacter(PlayerController parent)
		{
			base.SetReferenceToCharacter(parent);
			parent.states.slide = this;
		}

		public override void Init()
		{
			parent.states.knockback.onEnterEvent += Deactivate;
		}

		public override void Activate()
		{
			if (active)
				Deactivate();

			base.Activate();
		}

		protected override void OnEnter()
		{
			activeTime = 0;

			parent.states.movement.velocity.x = parent.states.motor.direction * speed.maxSpeed;
		}

		protected override void UpdateState()
		{
			parent.states.motor.speed.Set(speed);
			activeTime += Time.deltaTime;

			if (!parent.logic.isGrounded)
			{
				activeTime = duration - landExtension;
				return;
			}
			else
				parent.states.movement.character.SetRelativeColliderSize(1, 0.5f);

			if (parent.logic.hasCeiling)
				activeTime = Mathf.Min(activeTime, duration - tunnelExtension);
			else if (activeTime >= duration)
				Deactivate();
		}

		protected override void ResetState()
		{
			Deactivate();
		}
	}
}