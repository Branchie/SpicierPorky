namespace Gypo
{
	[System.Serializable]
	public class Speed
	{
		public float acceleration;
		public float deceleration;
		public float maxSpeed;

		public Speed(Speed s)
		{
			acceleration = s.acceleration;
			deceleration = s.deceleration;
			maxSpeed = s.maxSpeed;
		}

		public Speed(float acceleration, float deceleration, float maxSpeed)
		{
			this.acceleration = acceleration;
			this.deceleration = deceleration;
			this.maxSpeed = maxSpeed;
		}

		public void Multiply(float multiplier)
		{
			acceleration *= multiplier;
			deceleration *= multiplier;
			maxSpeed *= multiplier;
		}

		public void Multiply(Speed s)
		{
			acceleration *= s.acceleration;
			deceleration *= s.deceleration;
			maxSpeed *= s.maxSpeed;
		}

		public void Set(float acceleration, float deceleration, float maxSpeed)
		{
			this.acceleration = acceleration;
			this.deceleration = deceleration;
			this.maxSpeed = maxSpeed;
		}

		public void Set(Speed s)
		{
			acceleration = s.acceleration;
			deceleration = s.deceleration;
			maxSpeed = s.maxSpeed;
		}
	}
}