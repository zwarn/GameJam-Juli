using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

	public MovementHero movementHero;
	public CameraController cameraController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float x = Input.GetAxis ("ver3") + Input.GetAxis ("hor2");
		float y = +Input.GetAxis ("hor1") + Input.GetAxis ("hor3");
		float z = -Input.GetAxis ("ver1") + -Input.GetAxis ("ver2");
		movementHero.move (new Vector3 (x, y, z).normalized);

		if (Input.GetButtonDown("X-Flip")) {
			cameraController.flipX();
		}
		if (Input.GetButtonDown("Y-Flip")) {
			cameraController.flipY();
		}
		if (Input.GetButtonDown("Z-Flip")) {
			cameraController.flipZ();
		}
	}
}
