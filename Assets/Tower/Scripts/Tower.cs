using UnityEngine;

namespace TowerDestroy
{
	public class Tower : MonoBehaviour
	{
		[SerializeField] private GameObject partPrefab;
		[SerializeField] private Tower towerPrefab;
		[SerializeField] private float _startSpawnY;
		[SerializeField] private float _towerLenght;

		private void Start()
		{
			_startSpawnY = -this.transform.localScale.y +1;
			SpawnPlatform();
		}

		private void SpawnPlatform()
		{
			for (int i = (int)_startSpawnY; i < _towerLenght; i++)
			{
			 	GameObject platform = Instantiate(partPrefab, new Vector3(0f, i, 0f), Quaternion.Euler(0f, Random.Range(0, 360), 0f));
				platform.transform.localScale = new Vector3(1f,1f,1f);
				platform.transform.SetParent(transform);

				
			}
		}
	}
}
