using UnityEngine;
using System.Collections;

public class MovementHero : MonoBehaviour {

	public float force;
	public float rotationSpeed = 100;

	public LayerMask collisionLayer;
	private Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void rotate(Vector3 direction) {
		transform.Rotate (direction * Time.deltaTime * rotationSpeed);
	}

	public void move(float thrust) {
		rigidbody.AddForce (thrust * force * Time.deltaTime * transform.up);
	}

	void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag == "Solid") {
			Debug.Log (coll.gameObject.name);
		}
	}


}
