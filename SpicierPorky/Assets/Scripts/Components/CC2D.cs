namespace Gypo
{
	using System.Collections.Generic;
	using UnityEngine;

	public class CC2D : MonoBehaviour
	{
		public event System.Action onLanded = delegate { };

		private const float SKIN_WIDTH = 0.04f;

		public CollisionState collisionState;
		public List<Collider2D> ignoredPlatforms;

		[HideInInspector] public bool ignoreCollisions;
		[HideInInspector] public Vector2 velocity;

		private int layerMask = 1 << 0;
		private Vector2 center;

		private Vector2 adjustedSize => size - skinWidth;
		private Vector2 halfSize => col.bounds.extents;
		private Vector2 size => col.bounds.size;

		private Vector2 halfSkinWidth => new Vector2(SKIN_WIDTH * 0.5f, SKIN_WIDTH * 0.5f);
		private Vector2 skinWidth => new Vector2(SKIN_WIDTH, SKIN_WIDTH);

		private Vector2 offset => col.offset;
		private Vector2 position => transform.position;

		public BoxCollider2D col { get; private set; }

		private void Awake()
		{
			col = GetComponent<BoxCollider2D>();
			collisionState = new CollisionState();
			ignoredPlatforms = new List<Collider2D>();
		}

		public Vector2 Move(Vector2 deltaMovement)
		{
			CollisionState prevState = new CollisionState(collisionState);
			collisionState.Reset();

			if (!ignoreCollisions)
			{
				center = position + offset;

				center.x += Horizontal(ref deltaMovement);
				center.y += Vertical(ref deltaMovement);
			}

			transform.Translate(deltaMovement, Space.World);

			if (collisionState.slope)
				deltaMovement.y = 0;

			velocity = deltaMovement / Time.deltaTime;

			if (collisionState.down && !prevState.down)
				onLanded();

			return velocity;
		}

		private float Horizontal(ref Vector2 deltaMovement)
		{
			Vector2 direction = new Vector2(System.Math.Sign(deltaMovement.x), 0);
			float distance = Mathf.Abs(deltaMovement.x) + halfSkinWidth.x;

			RaycastHit2D hit = Physics2D.BoxCast(center, adjustedSize, 0, direction, distance, layerMask);

			if (hit.transform)
				HandleCollision_H(ref deltaMovement, hit.point.x, direction.x);

			return deltaMovement.x;
		}

		private void HandleCollision_H(ref Vector2 deltaMovement, float hitX, float direction)
		{
			int collisionDirection = System.Math.Sign(hitX - center.x);

			deltaMovement.x = hitX - (halfSize.x * collisionDirection) - center.x;
			collisionState.XCollision(direction);
		}

		private float Vertical(ref Vector2 deltaMovement)
		{
			Vector2 direction = new Vector2(0, System.Math.Sign(deltaMovement.y));
			float distance = Mathf.Abs(deltaMovement.y) + halfSkinWidth.y;

			RaycastHit2D hit;

			if (velocity.y > 0.1f)
				hit = Physics2D.BoxCast(center, adjustedSize, 0, direction, distance, layerMask);
			else
				hit = FilterOneWayPlatform(Physics2D.BoxCastAll(center, adjustedSize, 0, direction, distance, layerMask | 1 << 10));

			if (hit.transform)
				HandleCollision_V(ref deltaMovement, hit, direction.y);

			return deltaMovement.y;
		}

		private void HandleCollision_V(ref Vector2 deltaMovement, RaycastHit2D hit, float direction)
		{
			float hitY = hit.point.y;
			int collisionDirection = System.Math.Sign(hitY - center.y);

			deltaMovement.y = hitY - (halfSize.y * collisionDirection) - center.y;
			collisionState.YCollision(direction);

			if (collisionDirection < 0)
				collisionState.ground = hit.collider;
		}

		private RaycastHit2D FilterOneWayPlatform(RaycastHit2D[] hits)
		{
			foreach (RaycastHit2D h in hits)
			{
				if (!h.transform)
					continue;

				if (h.transform.gameObject.layer != 10)
					return h;

				if (ignoredPlatforms.Contains(h.collider))
					continue;

				if (h.collider.bounds.max.y - float.Epsilon > position.y)
					continue;

				return h;
			}

			return default;
		}

		public bool PlaceFree(Vector2 pos, int layers)
		{
			if (ignoreCollisions)
				return true;

			return Physics2D.BoxCast(
				pos,
				size - new Vector2(0.01f, 0.01f),
				0,
				Vector2.zero,
				float.Epsilon,
				layers
			).transform == null;
		}

		[System.Serializable]
		public class CollisionState
		{
			public Collider2D ground;

			public bool up, down, left, right;
			public bool slope;
			public float slopeAngle;

			public bool isGroundPlatform => ground && ground.transform.gameObject.layer == 1 << 10;
			public bool wall => left || right;

			public CollisionState() { }
			public CollisionState(CollisionState other)
			{
				ground = other.ground;

				up = other.up;
				down = other.down;
				left = other.left;
				right = other.right;

				slope = other.slope;
				slopeAngle = other.slopeAngle;
			}

			public void Reset()
			{
				up = down = left = right = false;

				slope = false;
				slopeAngle = 0;

				ground = null;
			}

			public void XCollision(float x)
			{
				if (x > 0) right = true;
				else if (x < 0) left = true;
				else left = right = true;
			}

			public void YCollision(float y)
			{
				if (y > 0) up = true;
				else if (y < 0) down = true;
				else down = up = true;
			}
		}
	}
}