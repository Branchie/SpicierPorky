namespace Gypo.SpicierPorky.Actors.Player
{
	using UnityEngine;

	public class PlayerSlide : CharacterState<PlayerController>
	{
		[SerializeField] private Speed speed = new Speed(5, 5, 15);

		[SerializeField] private float duration = 1;
		[SerializeField] private float landExtension = 0.25f;

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

			parent.states.graphic.transform.localScale = new Vector3(1, 0.25f, 1);
			parent.states.graphic.transform.localPosition = new Vector3(0, 0.125f, 0);

			parent.states.movement.velocity.x = parent.states.motor.direction * speed.maxSpeed;
		}

		protected override void OnExit()
		{
			parent.states.graphic.transform.localScale = Vector3.one;
			parent.states.graphic.transform.localPosition = new Vector3(0, 0.5f, 0);
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

			if (activeTime >= duration)
				Deactivate();
		}

		protected override void ResetState()
		{
			Deactivate();
		}
	}
}