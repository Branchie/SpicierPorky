namespace Gypo
{
	using System.Collections.Generic;
	using UnityEngine;

	public static partial class Extensions
	{
		public static T GetRandom<T>(this IList<T> l)
		{
			return l[Random.Range(0, l.Count)];
		}
	}
}