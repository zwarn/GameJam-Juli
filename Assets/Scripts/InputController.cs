using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

	public MovementHero movementHero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float x = Input.GetAxis ("X-Axis");
		float y = Input.GetAxis ("Y-Axis");
		float z = Input.GetAxis ("Z-Axis");
		movementHero.move (new Vector3 (x, y, z).normalized);
	}
}
