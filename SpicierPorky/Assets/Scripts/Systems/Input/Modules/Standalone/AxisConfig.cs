namespace Gypo.Input.Standalone
{
	using UnityEngine;

	[System.Serializable]
	public class AxisConfig : IAxisInput
	{
		[SerializeField] private KeyCodeAxis[] keys = default;
		[SerializeField] private string[] raw = default;

		public float Get()
		{
			float val = 0;

			// KeyCodes
			foreach (KeyCodeAxis key in keys)
				val += key.Get();

			// Gamepad Axis
			// ...

			// Gamepad Buttons
			// ...

			val = Mathf.Clamp(val, -1, 1);

			// Raw Axis
			foreach (string r in raw)
				val += Input.GetAxisRaw(r);

			return val;
		}
	}
}