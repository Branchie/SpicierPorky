namespace Gypo.SpicierPorky.Actors.Camera
{
	using UnityEngine;

	public class CameraMovement : CharacterState<CameraController>
	{
		[HideInInspector] public Vector2 newPosition;

		public float velocityMagnitude { get; private set; }
		public Vector2 velocity { get; private set; }

		public override void SetReferenceToCharacter(CameraController parent)
		{
			base.SetReferenceToCharacter(parent);
			parent.states.movement = this;

			newPosition = parent.position;
		}

		protected override void UpdateState()
		{
			velocity = (newPosition - parent.position) / Time.deltaTime;
			velocityMagnitude = velocity.magnitude;

			parent.position = newPosition;
		}

		protected override void ResetState()
		{
			newPosition = parent.position;
		}
	}
}