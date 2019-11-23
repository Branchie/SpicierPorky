namespace Gypo.SpicierPorky.Interfaces
{
	public interface ICameraTarget : Gypo.ICameraTarget
	{
		bool canFollow { get; }
		int direction { get; }
	}
}