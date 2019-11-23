namespace Gypo.SpicierPorky
{
	using Gypo.Input;
	using UnityEngine;

	public class InputManager : MonoBehaviour
	{
		[SerializeField] private ControlScheme player = default;

		private ControlScheme[] players;

		protected virtual void Awake()
		{
			players = new ControlScheme[Inputs.players.Length];

			for (int i = 0; i < players.Length; i++)
			{
				players[i] = Instantiate(player);
				players[i].SetControllerID(i);
			}
		}

		protected virtual void Update()
		{
			for (int i = 0; i < Inputs.players.Length; i++)
				Player(i);
		}

		protected virtual void Player(int index)
		{
			ControlScheme player = players[index];

			Inputs.player.jump.Update(player.GetButton("Jump"));
			Inputs.player.slide.Update(player.GetButton("Slide"));
		}
	}
}