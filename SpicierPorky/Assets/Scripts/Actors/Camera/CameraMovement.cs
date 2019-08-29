namespace Gypo.SpicierPorky.Actors.Camera
{
	using UnityEngine;

	public class CameraMovement : CharacterState<CameraController>
	{
		[HideInInspector] public Vector2 newPosition;

		public override void SetReferenceToCharacter(CameraController parent)
		{
			base.SetReferenceToCharacter(parent);
			parent.states.movement = this;

			newPosition = parent.position;
		}

		protected override void UpdateState()
		{
			parent.position = newPosition;
		}
	}
}