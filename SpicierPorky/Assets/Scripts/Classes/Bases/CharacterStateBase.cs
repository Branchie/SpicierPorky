namespace Gypo
{
	using UnityEngine;

	public class CharacterStateBase : MonoBehaviour
	{
		public event System.Action onEnterEvent = delegate { };
		public event System.Action onExitEvent = delegate { };

		public bool active
		{
			get => _active;
			set
			{
				if (_active == value)
					return;

				if (value)
				{
					_active = true;

					OnEnter();
					onEnterEvent();
				}
				else
				{
					_active = false;

					OnExit();
					onExitEvent();
				}
			}
		}
		private bool _active;

		public virtual void Init() { }

		public virtual void Activate()		=> SetActive(true);
		public virtual void Deactivate()	=> SetActive(false);
		public void SetActive(bool active)	=> this.active = active;

		protected virtual void OnEnter() { }
		protected virtual void OnExit() { }
		protected virtual void UpdateState() { }

		protected virtual void ResetState() { }
		protected virtual void SuspendState() { }
		protected virtual void UnsuspendState() { }

		public static void SetActive(CharacterStateBase state, bool active)
		{
			if (state == null)
				Debug.LogWarning("Missing state!");
			else
				state.SetActive(active);
		}

		public static void UpdateState(CharacterStateBase state)
		{
			if (state == null)
				Debug.LogWarning("Missing state!");
			else if (state.active)
				state.UpdateState();
		}
	}
}