namespace Gypo
{
	using UnityEngine;

	public class CharacterBase : MonoBehaviour, IResetable, ISuspendable
	{
		public virtual string resetKey		=> "Game";
		public virtual string suspensionKey	=> "Game";

		public bool isSuspended
		{
			get => _isSuspended;
			set
			{
				if (_isSuspended == value)
					return;

				if (value)
				{
					_isSuspended = true;
					OnSuspended();
				}
				else
				{
					_isSuspended = false;
					OnUnsuspended();
				}
			}
		}
		private bool _isSuspended;

		protected virtual void Awake()
		{
			transform.BroadcastMessage("SetReferenceToCharacter", this, SendMessageOptions.DontRequireReceiver);
			transform.BroadcastMessage("Init", SendMessageOptions.DontRequireReceiver);

			ResetManager.Register(this, resetKey);
			SuspensionManager.Register(this, suspensionKey);
		}

		protected virtual void OnDestroy()
		{
			ResetManager.Unregister(this, resetKey);
			SuspensionManager.Unregister(this, suspensionKey);
		}

		protected virtual void OnSuspended()
		{
			transform.BroadcastMessage("SuspendState", SendMessageOptions.DontRequireReceiver);
		}

		protected virtual void OnUnsuspended()
		{
			transform.BroadcastMessage("UnsuspendState", SendMessageOptions.DontRequireReceiver);
		}

		public virtual void OnReset()
		{
			transform.BroadcastMessage("ResetState", SendMessageOptions.DontRequireReceiver);
		}
	}
}