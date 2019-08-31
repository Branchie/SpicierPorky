namespace Gypo.SpicierPorky.Actors.Player
{
	using UnityEngine;

	public class PlayerMotor : CharacterState<PlayerController>
	{
		public Speed speed = new Speed(5, 5, 25);

		[HideInInspector] public int direction = 1;

		public override void SetReferenceToCharacter(PlayerController parent)
		{
			base.SetReferenceToCharacter(parent);
			parent.states.motor = this;
		}

		protected override void UpdateState()
		{
			Vector2 vel = parent.states.movement.velocity;

			float acc = speed.acceleration;
			float tar = direction * speed.maxSpeed;

			if ((vel.x > 0 && tar < vel.x) || (vel.x < 0 && tar > vel.x))
				acc = speed.deceleration;

			vel.x = Mathf.MoveTowards(vel.x, tar, Time.deltaTime * acc);

			parent.states.movement.velocity = vel;
		}
	}
}