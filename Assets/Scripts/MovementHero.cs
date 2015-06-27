using UnityEngine;
using System.Collections;

public class MovementHero : MonoBehaviour {

	public float force;
	private Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void move(Vector3 direction) {
		Debug.Log (direction);
		rigidbody.AddForce (force * direction);
	}


}
