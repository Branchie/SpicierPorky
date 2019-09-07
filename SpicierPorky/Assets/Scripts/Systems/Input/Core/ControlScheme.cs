namespace Gypo.Input
{
	using UnityEngine;

	[CreateAssetMenu(menuName = "Gypo/Input/Control Scheme")]
	public class ControlScheme : ScriptableObject
	{
		public string displayName = "New Control Scheme";

		[SerializeField] private AxisConfig[] axis = default;
		[SerializeField] private ButtonConfig[] buttons = default;

		public float GetAxis(string axis)
		{
			if (TryGetAxisConfig(axis, out AxisConfig result))
				return result.Get();

			Debug.LogWarning($"Unknown Axis: {axis}");
			return 0;
		}

		public Vector2 GetAxis2D(string x, string y)
		{
			if (!TryGetAxisConfig(x, out AxisConfig xAxis))
			{
				Debug.LogWarning($"Unknown Axis: {x}");
				return Vector2.zero;
			}

			if (!TryGetAxisConfig(y, out AxisConfig yAxis))
			{
				Debug.LogWarning($"Unknown Axis: {y}");
				return Vector2.zero;
			}

			return new Vector2(
				xAxis.Get(),
				yAxis.Get()
			);
		}

		public bool GetButton(string button)
		{
			if (TryGetButtonConfig(button, out ButtonConfig result))
				return result.Get();

			Debug.LogWarning($"Unknown Button: {button}");
			return false;
		}

		private AxisConfig GetAxisConfig(string name)
		{
			foreach (AxisConfig axisConfig in axis)
				if (string.Equals(name, axisConfig.name))
					return axisConfig;

			return null;
		}

		private ButtonConfig GetButtonConfig(string name)
		{
			foreach (ButtonConfig buttonConfig in buttons)
				if (string.Equals(name, buttonConfig.name))
					return buttonConfig;

			return null;
		}

		private bool TryGetAxisConfig(string name, out AxisConfig result) => (result = GetAxisConfig(name)) != null;
		private bool TryGetButtonConfig(string name, out ButtonConfig result) => (result = GetButtonConfig(name)) != null;
	}
}