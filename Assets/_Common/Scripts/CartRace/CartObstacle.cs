using UnityEngine;
using System.Collections;

public class CartObstacle : MonoBehaviour {

	public ParticleSystem explosionPrefab;

	private ParticleSystem explosion;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CrashAndBurn () {
		GetComponent<Renderer>().enabled = false;
		GetComponent<Collider>().enabled = false;

		explosion = Instantiate(explosionPrefab) as ParticleSystem;
		explosion.transform.position = transform.position;
		explosion.transform.parent = transform;

		explosion.Play();
		Destroy(explosion, explosion.duration);
	}
}
