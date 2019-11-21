namespace Gypo.SpicierPorky.Actors.Camera
{
	using UnityEngine;

	public class CameraFollow : CharacterState<CameraController>
	{
		[SerializeField, Range(0f, 1f)] private float normalizedX = 0.25f;
		[SerializeField] private float smoothTime = 0.125f;
		[SerializeField] private float yOffset = 2;

		private Vector2 dampVel;

		public override void SetReferenceToCharacter(CameraController parent)
		{
			base.SetReferenceToCharacter(parent);
			parent.states.follow = this;
		}

		public override void Init()
		{
			if (parent.target)
			{
				parent.position = GetTargetPosition();
				parent.states.movement.newPosition = parent.position;
			}
		}

		protected override void UpdateState()
		{
			parent.states.movement.newPosition = Vector2.SmoothDamp(
				parent.states.movement.newPosition,
				GetTargetPosition(),
				ref dampVel,
				smoothTime,
				float.MaxValue,
				Time.deltaTime
			);
		}

		private Vector2 GetTargetPosition()
		{
			return new Vector2(
				parent.targetPosition.x + (parent.states.bounds.halfSize.x - parent.states.bounds.size.x * normalizedX) * parent.targetDirection,
				parent.targetPosition.y + yOffset
			);
		}

		protected override void ResetState()
		{
			dampVel = Vector2.zero;
		}
	}
}