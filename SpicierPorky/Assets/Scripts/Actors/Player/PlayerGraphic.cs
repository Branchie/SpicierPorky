namespace Gypo.SpicierPorky.Actors.Player
{
	using UnityEngine;

	public class PlayerGraphic : CharacterState<PlayerController>
	{
		[HideInInspector] public Animator anim = default;
		[HideInInspector] public SpriteRenderer sprRen = default;

		public override void SetReferenceToCharacter(PlayerController parent)
		{
			base.SetReferenceToCharacter(parent);
			parent.states.graphic = this;

			anim = GetComponent<Animator>();
			sprRen = GetComponent<SpriteRenderer>();
		}
	}
}