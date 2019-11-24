namespace Gypo.SpicierPorky.Actors.Camera
{
	using Interfaces;
	using UnityEngine;

	public class CameraController : CharacterBase, ICamera
	{
		public event System.Action<Transform> onTargetChanged = delegate { };

		[HideInInspector] public CameraLogic logic;
		[HideInInspector] public CameraStates states = new CameraStates();
		[HideInInspector] public ICameraTarget cameraTarget;
		[HideInInspector] public Transform target;

		[SerializeField] protected string targetStartTag = "Player";

		private Vector2 startPosition;

		public int targetDirection		=> logic.hasCameraTarget ? cameraTarget.direction : 1;
		public Vector2 targetPosition	=> target ? (Vector2)target.position : Vector2.zero;

		public Camera cam { get; private set; }

		protected override void Awake()
		{
			cam = GetComponentInChildren<Camera>();
			SetTarget(GameObject.FindGameObjectWithTag(targetStartTag));

			base.Awake();

			startPosition = position;

			Game.cameras.Add(this);
		}

		protected override void OnDestroy()
		{
			Game.cameras.Remove(this);

			base.OnDestroy();
		}

		public void SetTarget(GameObject target)
		{
			if (!Equals(cameraTarget, null))
				cameraTarget.OnLostCameraFocus(this);

			if (target)
			{
				this.target = target.transform;
				cameraTarget = target.GetComponent<ICameraTarget>();

				if (!Equals(cameraTarget, null))
					cameraTarget.OnGainedCameraFocus(this);
			}
			else
			{
				this.target = null;
				cameraTarget = null;
			}

			onTargetChanged(this.target);
		}

		public override void OnReset()
		{
			position = startPosition;

			base.OnReset();
		}
	}
}