namespace Gypo
{
	public static class Layers
	{
		public static class Bitwise
		{
			public const int DEFAULT			= 1 << 0;
			public const int TRANSPARENT_FX		= 1 << 1;
			public const int IGNORE_RAYCAST		= 1 << 2;
			public const int WATER				= 1 << 4;
			public const int UI					= 1 << 5;
			public const int PLAYER				= 1 << 8;
			public const int INTERACTABLES		= 1 << 9;
			public const int PLATFORM			= 1 << 10;
		}

		public static class Integer
		{
			public const int DEFAULT			= 0;
			public const int TRANSPARENT_FX		= 1;
			public const int IGNORE_RAYCAST		= 2;
			public const int WATER				= 4;
			public const int UI					= 5;
			public const int PLAYER				= 8;
			public const int INTERACTABLES		= 9;
			public const int PLATFORM			= 10;
		}
	}
}