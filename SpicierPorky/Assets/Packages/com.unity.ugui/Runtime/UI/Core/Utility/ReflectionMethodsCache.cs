using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityEngine.UI
{
	internal class ReflectionMethodsCache
	{
		public delegate RaycastHit2D Raycast2DCallback(Vector2 p1, Vector2 p2, float f, int i);
		public delegate RaycastHit2D[] GetRayIntersectionAllCallback(Ray r, float f, int i);
		public delegate int GetRayIntersectionAllNonAllocCallback(Ray r, RaycastHit2D[] results, float f, int i);

		// We call Physics.Raycast and Physics2D.Raycast through reflection to avoid creating a hard dependency from
		// this class to the Physics/Physics2D modules, which would otherwise make it impossible to make content with UI
		// without force-including both modules.
		//
		// *NOTE* If other methods are required ensure to add [RequiredByNativeCode] to the bindings for that function. It prevents
		//        the function from being stripped if required. See Dynamics.bindings.cs for examples (search for GraphicRaycaster.cs).
		public ReflectionMethodsCache()
		{
			var raycast2DMethodInfo = typeof(Physics2D).GetMethod("Raycast", new[] { typeof(Vector2), typeof(Vector2), typeof(float), typeof(int) });
			if (raycast2DMethodInfo != null)
				raycast2D = (Raycast2DCallback)Delegate.CreateDelegate(typeof(Raycast2DCallback), raycast2DMethodInfo);

			var getRayIntersectionAllMethodInfo = typeof(Physics2D).GetMethod("GetRayIntersectionAll", new[] { typeof(Ray), typeof(float), typeof(int) });
			if (getRayIntersectionAllMethodInfo != null)
				getRayIntersectionAll = (GetRayIntersectionAllCallback)Delegate.CreateDelegate(typeof(GetRayIntersectionAllCallback), getRayIntersectionAllMethodInfo);

			var getRayIntersectionAllNonAllocMethodInfo = typeof(Physics2D).GetMethod("GetRayIntersectionNonAlloc", new[] { typeof(Ray), typeof(RaycastHit2D[]), typeof(float), typeof(int) });
			if (getRayIntersectionAllNonAllocMethodInfo != null)
				getRayIntersectionAllNonAlloc = (GetRayIntersectionAllNonAllocCallback)Delegate.CreateDelegate(typeof(GetRayIntersectionAllNonAllocCallback), getRayIntersectionAllNonAllocMethodInfo);
		}

		public Raycast2DCallback raycast2D = null;
		public GetRayIntersectionAllCallback getRayIntersectionAll = null;
		public GetRayIntersectionAllNonAllocCallback getRayIntersectionAllNonAlloc = null;

		private static ReflectionMethodsCache s_ReflectionMethodsCache = null;

		public static ReflectionMethodsCache Singleton
		{
			get
			{
				if (s_ReflectionMethodsCache == null)
					s_ReflectionMethodsCache = new ReflectionMethodsCache();
				return s_ReflectionMethodsCache;
			}
		}
	};
}
