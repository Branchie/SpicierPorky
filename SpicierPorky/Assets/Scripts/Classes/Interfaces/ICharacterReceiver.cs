namespace Gypo
{
	public interface ICharacterReceiver<T> where T : CharacterBase
	{
		void SetReferenceToCharacter(T parent);
	}
}