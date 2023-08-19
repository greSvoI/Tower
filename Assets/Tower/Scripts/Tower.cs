using System.Collections.Generic;
using UnityEngine;

namespace TowerDestroy
{
	public class Tower : MonoBehaviour
	{
		[SerializeField] private Platform platformPrefab;
		[SerializeField] private float _startSpawnY;
		[SerializeField] private float _towerLenght;

		[SerializeField] private List<BallDetection> ballDetections;

		private void Start()
		{
			_startSpawnY = -this.transform.localScale.y + 1;
			SpawnPlatform();
		}

		private void SpawnPlatform()
		{
			float spawn = _startSpawnY;
			
			for (int i = (int)spawn; i < _towerLenght; i++)
			{
				if(i%4 == 0)
				{
					float x = 0f;
					float z = 0f;
					if(Random.Range(-1,1) == 0)
					{
						x = Random.Range(-1, 1) == 0 ? 0.7f : -0.7f; 
					}
                    else
                    {
                        
						z = Random.Range(-1, 1) == 0 ? 0.7f : -0.7f;
					}

					GameObject ball = Instantiate(ballDetections[Random.Range(0, ballDetections.Count)].gameObject,new Vector3(x, i + 0.5f, z), Quaternion.identity);
					ball.transform.localScale = new Vector3(0.2f,0.2f, 0.2f);
					ball.transform.SetParent(transform);
				}

			 	GameObject platform = Instantiate(platformPrefab.gameObject, new Vector3(0f, i, 0f), Quaternion.Euler(0f, Random.Range(0, 360), 0f));
				platform.transform.localScale = new Vector3(1f,1f,1f);
				platform.transform.SetParent(transform);	
			}
		}
	}
}
