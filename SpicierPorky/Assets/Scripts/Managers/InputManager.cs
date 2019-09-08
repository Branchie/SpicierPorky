namespace Gypo.SpicierPorky
{
	using Gypo.Input;
	using UnityEngine;

	public class InputManager : MonoBehaviour
	{
		[SerializeField] private ControlScheme player = default;

		protected virtual void Update()
		{
			Inputs.player.movement = player.GetAxis2D("Horizontal", "Vertical");
		}

		protected virtual void FixedUpdate()
		{
			Inputs.player.jump.Update(player.GetButton("Jump"));
			Inputs.player.slide.Update(player.GetButton("Slide"));
		}
	}
}