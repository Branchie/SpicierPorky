namespace Gypo.SpicierPorky.Actors.Camera
{
	using UnityEngine;

	public class CameraZoom : CharacterState<CameraController>
	{
		[SerializeField] private float maxSize = 12.5f;
		[SerializeField] private float maxSpeed = 20;
		[SerializeField] private float minSpeed = 10;
		[SerializeField] private float smoothTime = 0.25f;

		private float defaultSize;
		private float dampVel;

		public override void SetReferenceToCharacter(CameraController parent)
		{
			base.SetReferenceToCharacter(parent);
			parent.states.zoom = this;

			defaultSize = parent.cam.orthographicSize;
		}

		protected override void UpdateState()
		{
			float tar = defaultSize;

			if (parent.states.movement.velocityMagnitude > minSpeed)
				tar = Mathf.Lerp(defaultSize, maxSize, (parent.states.movement.velocityMagnitude - minSpeed) / (maxSpeed - minSpeed));

			parent.cam.orthographicSize = Mathf.SmoothDamp(
				parent.cam.orthographicSize,
				tar,
				ref dampVel,
				smoothTime,
				float.MaxValue,
				Time.deltaTime
			);
		}

		protected override void ResetState()
		{
			parent.cam.orthographicSize = defaultSize;

			dampVel = 0;
		}
	}
}