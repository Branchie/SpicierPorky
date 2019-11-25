namespace Gypo.SpicierPorky.Actors.Camera
{
	using UnityEngine;

	public class CameraViewport : CharacterState<CameraController>
	{
		public event System.Action onChanged = delegate { };

		private static readonly Rect[][] viewports = new Rect[][]
		{
			new Rect[] { full },
			new Rect[] { topHalf, bottomHalf },
			new Rect[] { topHalf, bottomLeft, bottomRight },
			new Rect[] { topLeft, topRight, bottomLeft, bottomRight },
		};

		private static Rect full			=> new Rect(0.0f, 0.0f, 1.0f, 1.0f);

		private static Rect bottomHalf		=> new Rect(0.0f, 0.0f, 1.0f, 0.5f);
		private static Rect topHalf			=> new Rect(0.0f, 0.5f, 1.0f, 0.5f);

		private static Rect bottomLeft		=> new Rect(0.0f, 0.0f, 0.5f, 0.5f);
		private static Rect bottomRight		=> new Rect(0.5f, 0.0f, 0.5f, 0.5f);
		private static Rect topLeft			=> new Rect(0.0f, 0.5f, 0.5f, 0.5f);
		private static Rect topRight		=> new Rect(0.5f, 0.5f, 0.5f, 0.5f);

		public override void SetReferenceToCharacter(CameraController parent)
		{
			base.SetReferenceToCharacter(parent);
			parent.states.viewport = this;
		}

		private void Start()
		{
			UpdateViewport();
			Game.onCamerasChanged += UpdateViewport;
		}

		private void OnDestroy()
		{
			Game.onCamerasChanged -= UpdateViewport;
		}

		private void UpdateViewport()
		{
			if (Game.cameras.TryFindIndex(parent, out int index))
				SetIndex(index, Game.cameras.count);
		}

		public void SetIndex(int index, int cameraCount)
		{
			if (cameraCount <= 0 || cameraCount >= viewports.Length)
				return;

			if (index < 0 || index >= viewports[cameraCount].Length)
				return;

			parent.cam.rect = viewports[cameraCount - 1][index];
			onChanged();
		}
	}
}