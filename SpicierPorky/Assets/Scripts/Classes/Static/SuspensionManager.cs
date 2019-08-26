namespace Gypo
{
	using System.Collections.Generic;
	using UnityEngine;

	public static class SuspensionManager
	{
		private class SuspendableObject
		{
			private ISuspendable suspendable;

			private int suspensionCount;

			public SuspendableObject(ISuspendable suspendable)
			{
				this.suspendable = suspendable;
			}

			public void Suspend()
			{
				if (++suspensionCount == 1)
					suspendable.isSuspended = true;
			}

			public void Unsuspend()
			{
				suspensionCount = Mathf.Max(0, suspensionCount - 1);

				if (suspensionCount == 0)
					suspendable.isSuspended = false;
			}

			public void SetSuspend(bool suspend)
			{
				if (suspend)
					Suspend();
				else
					Unsuspend();
			}

			public bool Equals(ISuspendable suspendable) => Equals(this.suspendable, suspendable);
		}

		private static Dictionary<string, List<SuspendableObject>> suspendables = new Dictionary<string, List<SuspendableObject>>();

		public static void Register(ISuspendable suspendable, params string[] keys)
		{
			foreach (string key in keys)
			{
				if (!suspendables.TryGetValue(key, out List<SuspendableObject> result))
				{
					result = new List<SuspendableObject>();
					suspendables.Add(key, result);
				}

				result.Add(new SuspendableObject(suspendable));
			}
		}

		public static void Unregister(ISuspendable suspendable, params string[] keys)
		{
			foreach (string key in keys)
			{
				if (suspendables.TryGetValue(key, out List<SuspendableObject> result))
					result.RemoveAll(s => s.Equals(suspendable));
			}
		}

		public static void Suspend(params string[] keys)
		{
			foreach (string key in keys)
			{
				if (!suspendables.ContainsKey(key))
					continue;

				foreach (SuspendableObject s in suspendables[key])
					s.Suspend();
			}
		}

		public static void SuspendAll()
		{
			foreach (var suspendables in suspendables.Values)
				foreach (SuspendableObject s in suspendables)
					s.Suspend();
		}

		public static void SuspendAllExcept(params string[] keys)
		{
			foreach (string key in keys)
			{
				foreach (var entry in suspendables)
				{
					if (keys.Contains(key))
						continue;

					foreach (SuspendableObject s in entry.Value)
						s.Suspend();
				}
			}
		}

		public static void Unsuspend(params string[] keys)
		{
			foreach (string key in keys)
			{
				if (!suspendables.ContainsKey(key))
					continue;

				foreach (SuspendableObject s in suspendables[key])
					s.Unsuspend();
			}
		}

		public static void UnsuspendAll()
		{
			foreach (var suspendables in suspendables.Values)
				foreach (SuspendableObject s in suspendables)
					s.Unsuspend();
		}

		public static void UnsuspendAllExcept(params string[] keys)
		{
			foreach (string key in keys)
			{
				foreach (var entry in suspendables)
				{
					if (keys.Contains(key))
						continue;

					foreach (SuspendableObject s in entry.Value)
						s.Unsuspend();
				}
			}
		}

		public static void SetSuspended(bool suspend, params string[] keys)
		{
			foreach (string key in keys)
			{
				if (suspendables.TryGetValue(key, out List<SuspendableObject> result))
				{
					foreach (SuspendableObject s in result)
						s.SetSuspend(suspend);
				}
			}
		}

		public static void SetSuspendedAll(bool suspend)
		{
			foreach (var suspendables in suspendables.Values)
			{
				foreach (SuspendableObject s in suspendables)
					s.SetSuspend(suspend);
			}
		}

		public static void SetSuspendedAllExcept(bool suspend, params string[] keys)
		{
			foreach (string key in keys)
			{
				foreach (var entry in suspendables)
				{
					if (keys.Contains(entry.Key))
						continue;

					foreach (SuspendableObject s in entry.Value)
						s.SetSuspend(suspend);
				}
			}
		}
	}
}