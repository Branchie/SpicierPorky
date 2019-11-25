namespace Gypo.SpicierPorky.Managers
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
				players[i].SetControllerID(i == 0 ? -1 : i);
			}
		}

		protected virtual void Update()
		{
			for (int i = 0; i < Inputs.players.Length; i++)
				Player(i);
		}

		protected virtual void Player(int index)
		{
			ControlScheme input = players[index];
			Inputs.Player player = Inputs.players[index];

			player.flip.Update(input.GetButton("Flip"));
			player.join.Update(input.GetButton("Join"));
			player.jump.Update(input.GetButton("Jump"));
			player.slide.Update(input.GetButton("Slide"));
		}
	}
}