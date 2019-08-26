namespace Gypo
{
	using UnityEngine;

	public class CharacterState<T> : CharacterStateBase, ICharacterReceiver<T> where T : CharacterBase
	{
		protected T parent;

		public virtual void SetReferenceToCharacter(T parent)
		{
			this.parent = parent;
		}
	}
}