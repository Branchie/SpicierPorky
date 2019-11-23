namespace Gypo.SpicierPorky.Actors.Player
{
	using UnityEngine;

	public class PlayerGraphic : CharacterState<PlayerController>
	{
		private static readonly int HASH_AIR		= Animator.StringToHash("Air");
		private static readonly int HASH_DEAD		= Animator.StringToHash("Dead");
		private static readonly int HASH_IDLE		= Animator.StringToHash("Idle");
		private static readonly int HASH_KNOCKBACK	= Animator.StringToHash("Knockback");
		private static readonly int HASH_ROLL		= Animator.StringToHash("Roll");
		private static readonly int HASH_RUN		= Animator.StringToHash("Run");
		private static readonly int HASH_SLIDE		= Animator.StringToHash("Slide");
		private static readonly int HASH_WALL_DASH	= Animator.StringToHash("WallDash");
		private static readonly int HASH_WALL_SLIDE	= Animator.StringToHash("WallSlide");

		[HideInInspector] public Animator anim = default;
		[HideInInspector] public SpriteRenderer sprRen = default;

		[SerializeField] private RuntimeAnimatorController character = default;

		private int flip;
		private int animStateHash;

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
			int animStateHash;

			if (parent.logic.isDead)
				animStateHash = HASH_DEAD;
			else if (parent.logic.isGrounded)
			{
				if (parent.logic.isIdle)
					animStateHash = HASH_IDLE;
				else if (parent.logic.isSliding)
					animStateHash = HASH_SLIDE;
				else if (parent.logic.isKnockback)
					animStateHash = HASH_KNOCKBACK;
				else
					animStateHash = HASH_RUN;
			}
			else
			{
				if (parent.logic.isWallDash)
				{
					animStateHash = HASH_WALL_DASH;
					flip = -parent.logic.direction;
				}
				else if (parent.logic.isWallSlide)
				{
					animStateHash = HASH_WALL_SLIDE;
					flip = -parent.logic.direction;
				}
				else if (parent.logic.isSliding)
					animStateHash = HASH_ROLL;
				else
					animStateHash = HASH_AIR;
			}

			if (this.animStateHash != animStateHash)
				Play(animStateHash, -1, 0);

			if (this.flip != flip)
				SetFlip(flip);
		}

		private void Play(int stateHash, int layer, float normalizedTime)
		{
			anim.Play(stateHash, layer, normalizedTime);
			anim.Update(float.Epsilon);

			animStateHash = stateHash;
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