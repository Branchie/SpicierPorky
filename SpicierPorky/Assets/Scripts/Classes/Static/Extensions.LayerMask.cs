namespace Gypo
{
	using UnityEngine;

	public static partial class Extensions
	{
		public static bool Contains(this LayerMask layerMask, int layer)			=> layerMask == (layerMask | (1 << layer));
		public static bool ContainsBitwise(this LayerMask layerMask, int bitwise)	=> layerMask == (layerMask | bitwise);
	}
}