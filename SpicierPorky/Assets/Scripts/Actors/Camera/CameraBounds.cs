namespace Gypo.SpicierPorky.Actors.Camera
{
	using UnityEngine;

	public class CameraBounds : CharacterState<CameraController>
	{
		public float xMax			{ get; private set; }
		public float xMin			{ get; private set; }
		public float yMax			{ get; private set; }
		public float yMin			{ get; private set; }
		public Vector2 halfSize		{ get; private set; }
		public Vector2 size			{ get; private set; }

		private float aspect;
		private float orthographicSize;

		public override void SetReferenceToCharacter(CameraController parent)
		{
			base.SetReferenceToCharacter(parent);
			parent.states.bounds = this;

			UpdateBounds();
		}

		protected override void UpdateState()
		{
			Vector2 position = transform.position;

			xMax = position.x + halfSize.x;
			xMin = position.x - halfSize.x;
			yMax = position.y + halfSize.y;
			yMin = position.y - halfSize.y;

			if (!Mathf.Approximately(aspect, parent.cam.aspect) ||
				!Mathf.Approximately(orthographicSize, parent.cam.orthographicSize))
				UpdateBounds();
		}

		public void UpdateBounds()
		{
			aspect = parent.cam.aspect;
			orthographicSize = parent.cam.orthographicSize;

			Vector2 size;
			size.y = orthographicSize * 2;
			size.x = size.y * aspect;

			halfSize = size / 2f;
			this.size = size;
		}
	}
}