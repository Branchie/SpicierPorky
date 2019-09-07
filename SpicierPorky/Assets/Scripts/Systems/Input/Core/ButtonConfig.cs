#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
#define STANDALONE
#endif

namespace Gypo.Input
{
	using UnityEngine;

	[System.Serializable]
	public class ButtonConfig : IButtonInput
	{
		public string name;

#if STANDALONE
		[SerializeField] private Standalone.ButtonConfig standalone = default;
#endif

		public bool Get()
		{
#if STANDALONE
			if (standalone.Get())
				return true;
#endif

			return false;
		}
	}
}