namespace Gypo.SpicierPorky
{
	using UnityEngine;

	public class InputManager : MonoBehaviour
	{
		protected virtual void Update()
		{
			Inputs.player.movement = new Vector2(
				Input.GetAxisRaw("Horizontal"),
				Input.GetAxisRaw("Vertical")
			);
		}

		protected virtual void FixedUpdate()
		{
			Inputs.player.jump.Update(Input.GetKey(KeyCode.L));
		}
	}
}