namespace Gypo.SpicierPorky.Actors.Player
{
	using UnityEngine;

	public class PlayerMotor : CharacterState<PlayerController>
	{
		public Speed speed = new Speed(5, 5, 15);

		[HideInInspector] public int direction = 1;

		[SerializeField] private bool forceRight = true;

		private Speed _speed;

		private int _direction;

		public override void SetReferenceToCharacter(PlayerController parent)
		{
			base.SetReferenceToCharacter(parent);
			parent.states.motor = this;

			_speed = new Speed(speed);

			_direction = direction;
		}

		protected override void UpdateState()
		{
			Vector2 vel = parent.states.movement.velocity;

			float acc = speed.acceleration;
			float tar = direction * speed.maxSpeed;

			if (forceRight && parent.logic.isGrounded)
				direction = 1;

			if ((direction > 0 && vel.x > tar) || (direction < 0 && vel.x < tar))
				vel.x = tar;
			else
			{
				if ((vel.x > 0 && tar < vel.x) || (vel.x < 0 && tar > vel.x))
					acc = speed.deceleration;

				vel.x = Mathf.MoveTowards(vel.x, tar, Time.deltaTime * acc);
			}

			parent.states.movement.velocity = vel;
			speed.Set(_speed);
		}

		protected override void ResetState()
		{
			direction = _direction;
		}

		public void SetForceRight(bool value) => forceRight = value;
		public void ToggleForceRight() => forceRight = !forceRight;
	}
}