using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public float robotSpeed=1f;
	public Spawn spawn;
	public bool deleted;

	private bool collide=false;
	private float time;
	private int collisionSwitch;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		deleted = false;
		transform.position.Set (transform.position.x, 0f, transform.position.z);
		if (collide) {
			if (time > 1f) {
				time=0;
				collide=false;
			} else {
				time+=Time.deltaTime;
			}
		} else {
			move ();
		}
	}

	void move () {
		transform.Translate(Vector3.right*robotSpeed);
	}

	void OnCollisionEnter (Collision collision) {
		/*collide = true;*/
		if (collision.gameObject.name == "Wall(Clone)" || collision.gameObject.name == "Enemy(Clone)") {
			transform.Rotate (Vector3.up, 90f);
		}

		if(collision.gameObject.tag == "Player")
		{
			Destroy(collision.gameObject);
			Camera.main.GetComponent<CameraFade>().FadeAndNext(Color.black, 3, "x-01 Game Over", true);
		}
		Destroy (this.gameObject);
	}
}
