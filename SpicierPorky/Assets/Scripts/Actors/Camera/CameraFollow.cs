namespace Gypo.SpicierPorky.Actors.Camera
{
	using UnityEngine;

	public class CameraFollow : CharacterState<CameraController>
	{
		[SerializeField] private float smoothTime = 0.125f;

		private Vector2 dampVel;

		public override void SetReferenceToCharacter(CameraController parent)
		{
			base.SetReferenceToCharacter(parent);
			parent.states.follow = this;
		}

		protected override void UpdateState()
		{
			parent.states.movement.newPosition = Vector2.SmoothDamp(
				parent.states.movement.newPosition,
				parent.targetPosition,
				ref dampVel,
				smoothTime,
				float.MaxValue,
				Time.deltaTime
			);
		}
	}
}