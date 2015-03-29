using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	void OnTriggerEnter (Collider other)
	{
		print("Triggered: " + other.tag);
		if(other.gameObject.tag == "Player")
		{
			Destroy(other.gameObject);
			Camera.main.GetComponent<CameraFade>().FadeAndNext(Color.black, 3, "x-01 Game Over", true);
		}
		
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
