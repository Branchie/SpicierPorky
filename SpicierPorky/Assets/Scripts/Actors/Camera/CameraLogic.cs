﻿namespace Gypo.SpicierPorky.Actors.Camera
{
	using UnityEngine;

	public class CameraLogic : MonoBehaviour, ICharacterReceiver<CameraController>
	{
		private CameraController parent;
		private CameraStates states;

		public bool allowBounds			=> true;
		public bool allowFollow			=> hasTarget && canFollowTarget;
		public bool allowMovement		=> true;
		public bool allowViewport		=> true;
		public bool allowZoom			=> true;

		public bool canFollowTarget		=> !hasCameraTarget || hasCameraTarget && parent.cameraTarget.canFollow;
		public bool hasCameraTarget		=> !Equals(parent.cameraTarget, null);
		public bool hasTarget			=> parent.target;

		public void SetReferenceToCharacter(CameraController parent)
		{
			this.parent = parent;
			parent.logic = this;

			states = parent.states;
		}

		private void LateUpdate()
		{
			if (parent.isSuspended)
				return;

			CharacterStateBase.SetActive(states.bounds, allowBounds);
			CharacterStateBase.SetActive(states.follow, allowFollow);
			CharacterStateBase.SetActive(states.movement, allowMovement);
			CharacterStateBase.SetActive(states.viewport, allowViewport);
			CharacterStateBase.SetActive(states.zoom, allowZoom);

			CharacterStateBase.UpdateState(states.bounds);

			CharacterStateBase.UpdateState(states.follow);
			CharacterStateBase.UpdateState(states.viewport);
			CharacterStateBase.UpdateState(states.zoom);

			CharacterStateBase.UpdateState(states.movement);
		}
	}
}