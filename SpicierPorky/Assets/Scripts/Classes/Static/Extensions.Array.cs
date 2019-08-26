namespace Gypo
{
	public static partial class Extensions
	{
		public static bool Contains<T>(this T[] arr, T element) where T : class
		{
			foreach (T e in arr)
			{
				if (ReferenceEquals(e, element))
					return true;
			}

			return false;
		}
	}
}