using UnityEngine;
using System.Collections;

public class MovementHero : MonoBehaviour {

	public float force;
	public LayerMask collisionLayer;
	private Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void move(Vector3 direction) {
		rigidbody.AddForce (force * direction);
	}

	void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag == "Solid") {
			Debug.Log (coll.gameObject.name);
		}
	}


}
