using UnityEngine;
using System.Collections;

public class ParticleSortingLayer : MonoBehaviour {

	public int sortingOrder;
	public string sortingLayerName;

	// Use this for initialization
	void Start ()
	{
		// Set the sorting layer of the particle system.
		GetComponent<ParticleSystem> ().GetComponent<Renderer> ().sortingLayerName = sortingLayerName;
		GetComponent<ParticleSystem> ().GetComponent<Renderer> ().sortingOrder = sortingOrder;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
