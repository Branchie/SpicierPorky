namespace Gypo.SpicierPorky.Actors.Camera
{
	using UnityEngine;

	public class CameraController : CharacterBase
	{
		public event System.Action<Transform> onTargetChanged = delegate { };

		[HideInInspector] public Camera cam;
		[HideInInspector] public CameraLogic logic;
		[HideInInspector] public CameraStates states = new CameraStates();
		[HideInInspector] public CharacterBase targetCharacter;

		[SerializeField] protected string targetStartTag = "Player";

		public Transform target
		{
			get => _target;
			set
			{
				if (_target == value)
					return;

				_target = value;

				if (_target)
					targetCharacter = _target.GetComponent<CharacterBase>();

				onTargetChanged(_target);
			}
		}
		private Transform _target;

		public Vector2 targetPosition
		{
			get => target ? (Vector2)target.position : Vector2.zero;
		}

		protected override void Awake()
		{
			cam = GetComponentInChildren<Camera>();
			target = GameObject.FindGameObjectWithTag(targetStartTag)?.transform;

			base.Awake();
		}
	}
}