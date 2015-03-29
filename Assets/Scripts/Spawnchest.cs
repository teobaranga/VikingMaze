using UnityEngine;
using System.Collections;

public class Spawnchest : MonoBehaviour {

	public int maxchest=2;
	public GameObject chests;
	public Maze mazeScript; 
	public int minSpawnDistance = 5;

	public GameObject player;
	private int numberOfchests;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (numberOfchests < maxchest) {
			Spawnchests();
		}
	}
	
	void Spawnchests () {
		int index = Random.Range (0, mazeScript.cellCentres.Count);
		Vector3 point = mazeScript.cellCentres [index];/* mazeScript.cellCentres [index];*/
		if (Vector3.Distance (player.transform.position, new Vector3 (1.0f, 0.0f, 10.0f)) < minSpawnDistance) {
			return;
		}
		Instantiate (chests, point, Quaternion.identity);

		numberOfchests++;
	}

}
