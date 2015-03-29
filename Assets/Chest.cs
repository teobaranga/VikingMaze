using UnityEngine;
using System.Collections;
 

public class Chest : MonoBehaviour {
	int content; 
	
	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Player"); 
		{

			content = Random.Range(1, 4);
			destroy();

			Debug.Log("HHJGFG");
		}
	}

	void destroy() {
		Destroy(this.gameObject);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
