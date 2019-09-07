#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
#define STANDALONE
#endif

namespace Gypo.Input
{
	using UnityEngine;

	[System.Serializable]
	public class AxisConfig : IAxisInput
	{
		public string name;

#if STANDALONE
		[SerializeField] private Standalone.AxisConfig standalone = default;
#endif

		public float Get()
		{
			float result = 0;

#if STANDALONE
			result += standalone.Get();
#endif

			return result;
		}
	}
}