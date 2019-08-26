using System.Collections.Generic;
using UnityEngine.UI;

namespace UnityEngine.EventSystems
{
	/// <summary>
	/// Simple event system using physics raycasts.
	/// </summary>
	[AddComponentMenu("Event/Physics 2D Raycaster")]
	[RequireComponent(typeof(Camera))]
	/// <summary>
	/// Raycaster for casting against 2D Physics components.
	/// </summary>
	public class Physics2DRaycaster : BaseRaycaster
	{
		RaycastHit2D[] m_Hits;

		/// <summary>
		/// Const to use for clarity when no event mask is set
		/// </summary>
		protected const int kNoEventMaskSet = -1;

		/// <summary>
		/// Layer mask used to filter events. Always combined with the camera's culling mask if a camera is used.
		/// </summary>
		[SerializeField]
		protected LayerMask m_EventMask = kNoEventMaskSet;

		/// <summary>
		/// The max number of intersections allowed. 0 = allocating version anything else is non alloc.
		/// </summary>
		[SerializeField]
		protected int m_MaxRayIntersections = 0;
		protected int m_LastMaxRayIntersections = 0;

		public override Camera eventCamera
		{
			get
			{
				if (m_EventCamera == null)
					m_EventCamera = GetComponent<Camera>();
				return m_EventCamera ?? Camera.main;
			}
		}
		protected Camera m_EventCamera;

		/// <summary>
		/// Event mask used to determine which objects will receive events.
		/// </summary>
		public int finalEventMask
		{
			get { return (eventCamera != null) ? eventCamera.cullingMask & m_EventMask : kNoEventMaskSet; }
		}

		/// <summary>
		/// Layer mask used to filter events. Always combined with the camera's culling mask if a camera is used.
		/// </summary>
		public LayerMask eventMask
		{
			get { return m_EventMask; }
			set { m_EventMask = value; }
		}

		/// <summary>
		/// Max number of ray intersection allowed to be found.
		/// </summary>
		/// <remarks>
		/// A value of zero will represent using the allocating version of the raycast function where as any other value will use the non allocating version.
		/// </remarks>
		public int maxRayIntersections
		{
			get { return m_MaxRayIntersections; }
			set { m_MaxRayIntersections = value; }
		}

		protected Physics2DRaycaster()
		{ }

		/// <summary>
		/// Raycast against 2D elements in the scene.
		/// </summary>
		public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
		{
			Ray ray = new Ray();
			float distanceToClipPlane = 0;
			if (!ComputeRayAndDistance(eventData, ref ray, ref distanceToClipPlane))
				return;

			int hitCount = 0;

			if (maxRayIntersections == 0)
			{
				if (ReflectionMethodsCache.Singleton.getRayIntersectionAll == null)
					return;

				m_Hits = ReflectionMethodsCache.Singleton.getRayIntersectionAll(ray, distanceToClipPlane, finalEventMask);
				hitCount = m_Hits.Length;
			}
			else
			{
				if (ReflectionMethodsCache.Singleton.getRayIntersectionAllNonAlloc == null)
					return;

				if (m_LastMaxRayIntersections != m_MaxRayIntersections)
				{
					m_Hits = new RaycastHit2D[maxRayIntersections];
					m_LastMaxRayIntersections = m_MaxRayIntersections;
				}

				hitCount = ReflectionMethodsCache.Singleton.getRayIntersectionAllNonAlloc(ray, m_Hits, distanceToClipPlane, finalEventMask);
			}

			if (hitCount != 0)
			{
				for (int b = 0, bmax = hitCount; b < bmax; ++b)
				{
					var sr = m_Hits[b].collider.gameObject.GetComponent<SpriteRenderer>();

					var result = new RaycastResult
					{
						gameObject = m_Hits[b].collider.gameObject,
						module = this,
						distance = Vector3.Distance(eventCamera.transform.position, m_Hits[b].point),
						worldPosition = m_Hits[b].point,
						worldNormal = m_Hits[b].normal,
						screenPosition = eventData.position,
						index = resultAppendList.Count,
						sortingLayer = sr != null ? sr.sortingLayerID : 0,
						sortingOrder = sr != null ? sr.sortingOrder : 0
					};
					resultAppendList.Add(result);
				}
			}
		}

		/// <summary>
		/// Returns a ray going from camera through the event position and the distance between the near and far clipping planes along that ray.
		/// </summary>
		/// <param name="eventData">The pointer event for which we will cast a ray.</param>
		/// <param name="ray">The ray to use.</param>
		/// <param name="distanceToClipPlane">The distance between the near and far clipping planes along the ray.</param>
		/// <returns>True if the operation was successful. false if it was not possible to compute, such as the eventPosition being outside of the view.</returns>
		protected bool ComputeRayAndDistance(PointerEventData eventData, ref Ray ray, ref float distanceToClipPlane)
		{
			if (eventCamera == null)
				return false;

			var eventPosition = Display.RelativeMouseAt(eventData.position);
			if (eventPosition != Vector3.zero)
			{
				// We support multiple display and display identification based on event position.
				int eventDisplayIndex = (int)eventPosition.z;

				// Discard events that are not part of this display so the user does not interact with multiple displays at once.
				if (eventDisplayIndex != eventCamera.targetDisplay)
					return false;
			}
			else
			{
				// The multiple display system is not supported on all platforms, when it is not supported the returned position
				// will be all zeros so when the returned index is 0 we will default to the event data to be safe.
				eventPosition = eventData.position;
			}

			// Cull ray casts that are outside of the view rect. (case 636595)
			if (!eventCamera.pixelRect.Contains(eventPosition))
				return false;

			ray = eventCamera.ScreenPointToRay(eventPosition);
			// compensate far plane distance - see MouseEvents.cs
			float projectionDirection = ray.direction.z;
			distanceToClipPlane = Mathf.Approximately(0.0f, projectionDirection)
				? Mathf.Infinity
				: Mathf.Abs((eventCamera.farClipPlane - eventCamera.nearClipPlane) / projectionDirection);
			return true;
		}
	}
}