namespace Gypo
{
	public class InputProcessor
	{
		public bool isDown;
		public bool wasDown;

		public bool onPressed => isDown && !wasDown;
		public bool onReleased => !isDown && wasDown;

		public void Update(bool isDown)
		{
			wasDown = this.isDown;
			this.isDown = isDown;
		}
	}
}