﻿namespace Gypo.SpicierPorky.Actors.Camera
{
	using UnityEngine;

	public class CameraLogic : MonoBehaviour, ICharacterReceiver<CameraController>
	{
		private CameraController parent;
		private CameraStates states;

		public bool allowFollow => hasTarget;
		public bool allowMovement => true;

		public bool hasTarget => parent.target;

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

			CharacterStateBase.SetActive(states.follow, allowFollow);
			CharacterStateBase.SetActive(states.movement, allowMovement);

			CharacterStateBase.UpdateState(states.follow);
			CharacterStateBase.UpdateState(states.movement);
		}
	}
}