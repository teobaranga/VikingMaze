using UnityEngine;
using System.Collections;

public class ParticleSorter : MonoBehaviour {

	void Start () {
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "ParticleFX";
	}
}
