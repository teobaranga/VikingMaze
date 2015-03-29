using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {
	public EnemyBehaviour enemyScript;
	public int maxEnemies=3;
	public GameObject enemy;
	public Maze mazeScript; 
	public int minSpawnDistance = 5;

	public GameObject player;
	public int numberOfEnemies;


	// Use this for initialization
	void Start () {
		numberOfEnemies = 0;
	}
	
	// Update is called once per frame
	void Update () {
		numberOfEnemies = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		if (numberOfEnemies < maxEnemies) {
			SpawnEnemy();
		}
	}

	void SpawnEnemy () {
		int index = Random.Range (0, mazeScript.cellCentres.Count);
		Vector3 point = /*new Vector3 (1.0f, 0.0f, 10.0f);*/ mazeScript.cellCentres [index];
		if (Vector3.Distance (player.transform.position, new Vector3 (1.0f, 0.0f, 10.0f)) < minSpawnDistance) {
			return;
		}
		Instantiate (enemy, point, Quaternion.identity);
	}
}
