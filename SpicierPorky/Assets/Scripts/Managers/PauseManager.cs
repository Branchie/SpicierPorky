namespace Gypo
{
	public static class PauseManager
	{
		public static System.Action<bool> onPauseChanged = delegate { };

		public static bool paused
		{
			get => _paused;
			set
			{
				if (_paused == value)
					return;

				_paused = value;
				onPauseChanged(_paused);
			}
		}
		private static bool _paused;

		public static bool Pause()		=> paused = true;
		public static bool Unpause()	=> paused = false;
		public static bool Toggle()		=> paused = !paused;
	}
}