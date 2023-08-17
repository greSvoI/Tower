using UnityEngine;

namespace TowerDestroy
{
	public class Tower : MonoBehaviour
	{
		[SerializeField] private Platform partPrefab;
		[SerializeField] private float _startSpawnY;
		[SerializeField] private float _towerLenght;

		private void Start()
		{
			_startSpawnY = -this.transform.localScale.y;
			SpawnPlatform();
		}

		private void SpawnPlatform()
		{
			for (int i = (int)_startSpawnY; i < _towerLenght; i++)
			{
				Instantiate(partPrefab, new Vector3(0f, i, 0f), Quaternion.Euler(0f, Random.Range(0, 360), 0f),transform);
			}
		}
	}
}
