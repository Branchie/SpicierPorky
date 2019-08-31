namespace Gypo.SpicierPorky
{
	using UnityEngine;

	public class InputManager : MonoBehaviour
	{
		protected virtual void Update()
		{
			Inputs.player.jump.Update(Input.GetKey(KeyCode.L));

			Inputs.player.movement = new Vector2(
				Input.GetAxisRaw("Horizontal"),
				Input.GetAxisRaw("Vertical")
			);
		}
	}
}