namespace Gypo.SpicierPorky.Managers
{
	using Data;
	using UnityEngine;

	public class PrefabManager : MonoBehaviour
	{
		private static PrefabData prefabs;

		[SerializeField] private PrefabData prefabData = default;

		private void Awake()
		{
			prefabs = prefabData;
		}

		public static GameObject Spawn(Prefab prefab, Vector2 position)
		{
			return Instantiate(Get(prefab), position, Quaternion.identity);
		}

		public static GameObject Get(Prefab prefab) => prefabs?.Get(prefab);
	}
}