namespace Gypo.SpicierPorky.Actors.Player
{
	using UnityEngine;

	public class PlayerJump : CharacterState<PlayerController>
	{
		public event System.Action onJump = delegate { };

		[SerializeField] private float height = 5;

		public override void SetReferenceToCharacter(PlayerController parent)
		{
			base.SetReferenceToCharacter(parent);
			parent.states.jump = this;
		}

		public override void Activate()
		{
			Jump(height);
		}

		public void Jump(float height)
		{
			parent.states.movement.velocity.y = GetForceForHeight(height);
			onJump();
		}

		public float GetForceForHeight(float height) => Mathf.Sqrt(-2 * Physics2D.gravity.y * height);
	}
}