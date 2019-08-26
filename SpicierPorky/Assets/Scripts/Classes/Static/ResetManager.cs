namespace Gypo
{
	using System.Collections.Generic;

	public static class ResetManager
	{
		private static Dictionary<string, List<IResetable>> resetables = new Dictionary<string, List<IResetable>>();

		public static void Register(IResetable resetable, params string[] keys)
		{
			foreach (string key in keys)
			{
				if (!resetables.TryGetValue(key, out List<IResetable> result))
				{
					result = new List<IResetable>();
					resetables.Add(key, result);
				}

				result.Add(resetable);
			}
		}

		public static void Unregister(IResetable resetable, params string[] keys)
		{
			foreach (string key in keys)
			{
				if (resetables.TryGetValue(key, out List<IResetable> result))
					result.RemoveAll(x => Equals(x, resetable));
			}
		}

		public static void Reset(params string[] keys)
		{
			foreach (string key in keys)
			{
				if (!resetables.TryGetValue(key, out List<IResetable> result))
					continue;

				foreach (IResetable r in result)
					r.OnReset();
			}
		}

		public static void ResetAll()
		{
			foreach (List<IResetable> resetables in resetables.Values)
				foreach (IResetable r in resetables)
					r.OnReset();
		}
	}
}