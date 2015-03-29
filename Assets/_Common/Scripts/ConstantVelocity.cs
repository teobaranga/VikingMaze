using UnityEngine;
using System.Collections;

public class ConstantVelocity : MonoBehaviour {

	public Vector2 velocity = Vector2.zero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		GetComponent<Rigidbody2D>().velocity = velocity;
	}
}
