namespace Gypo.SpicierPorky.Actors.Player
{
	using UnityEngine;

	public class PlayerGraphic : CharacterState<PlayerController>
	{
		[HideInInspector] public Animator anim = default;
		[HideInInspector] public SpriteRenderer sprRen = default;

		[SerializeField] private RuntimeAnimatorController character = default;

		private int flip;
		private string animState;

		public override void SetReferenceToCharacter(PlayerController parent)
		{
			base.SetReferenceToCharacter(parent);
			parent.states.graphic = this;

			anim = GetComponent<Animator>();
			sprRen = GetComponent<SpriteRenderer>();

			if (character != null)
				anim.runtimeAnimatorController = character;
		}

		protected override void UpdateState()
		{
			int flip = parent.logic.direction;
			string animState;

			if (parent.logic.isGrounded)
			{
				if (parent.logic.isIdle)
					animState = "Idle";
				else if (parent.logic.isSliding)
					animState = "Slide";
				else if (parent.logic.isKnockback)
					animState = "Knockback";
				else
					animState = "Run";
			}
			else
			{
				if (parent.logic.isWallDash)
				{
					animState = "WallDash";
					flip = -parent.logic.direction;
				}
				else if (parent.logic.isWallSlide)
				{
					animState = "WallSlide";
					flip = -parent.logic.direction;
				}
				else if (parent.logic.isSliding)
					animState = "Roll";
				else
					animState = "Air";
			}

			if (!string.Equals(this.animState, animState))
				Play(animState, -1, 0);

			if (this.flip != flip)
				SetFlip(flip);
		}

		private void Play(string stateName, int layer, float normalizedTime)
		{
			anim.Play(stateName, layer, normalizedTime);
			anim.Update(float.Epsilon);

			animState = stateName;
		}

		private void SetFlip(int direction)
		{
			Vector3 localScale = transform.localScale;
			localScale.x = Mathf.Sign(direction);
			transform.localScale = localScale;

			flip = direction;
		}

		protected override void SuspendState()		=> anim.speed = 0;
		protected override void UnsuspendState()	=> anim.speed = 1;
	}
}