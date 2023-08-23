using System.Collections.Generic;
using UnityEngine;

namespace TowerDestroy
{
	public class Tower : MonoBehaviour
	{
		[Header("Platform prefab")]
		[SerializeField] private Platform platform;
		[SerializeField] private GameObject startPlatform;
		[SerializeField] private GameObject finishPlatform;

		[Space(2)]
		[Header("Spawn platform parametrs")]
		[SerializeField] private float _startSpawnY= 0;
		[SerializeField] private float _towerLenght;

		[Space(2)]
		[Header("Ball")]
		[SerializeField] private BallController ball;
		[SerializeField] private List<BallDetection> ballDetections;

		private void Start()
		{
			ball = FindFirstObjectByType<BallController>();
		}
		public float SpawnTower { get => _startSpawnY; set=>_startSpawnY=value; }
		private void Update()
		{
			if(ball.transform.position.y > (transform.position.y + transform.localScale.y))
			{
				Debug.Log("Destroy");
				Destroy(this.gameObject,1f);
			}
		}

		public void SpawnPlatform(int spawnY, int heightTower,bool isStart = false)
		{	
			
			if(isStart)
			{
				GameObject platform = Instantiate(startPlatform,new Vector3(0f,spawnY,0f),Quaternion.identity);
				platform.transform.localScale = Vector3.one;
				platform.transform.SetParent(transform);
				spawnY++;
			}
			for (int i = spawnY; i < heightTower; i++)
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

				GameObject platform = Instantiate(this.platform.gameObject, new Vector3(0f, i, 0f), Quaternion.Euler(0f, Random.Range(0, 360), 0f));
				platform.transform.localScale = new Vector3(1f,1f,1f);
				platform.transform.SetParent(transform);	
			}
		}

	}
}
