using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {
	public EnemyBehaviour enemyScript;
	public int maxEnemies=3;
	public GameObject enemy;
	public Maze mazeScript; 

	public GameObject player = GameObject.Find("Player");
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
		Vector3 pos = player.transform.position;
		int index = Random.Range (0, mazeScript.cellCentres.Count);
		Vector3 point = mazeScript.cellCentres [index];
		int distance = (int)Vector3.Distance (pos, point);
		print (distance);
		if ( distance > 40 ) {
			print ("true");
			Instantiate (enemy, point, Quaternion.identity);
		}
		return; 
	}
}
