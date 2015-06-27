using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{

	public MovementHero movementHero;
	public CameraController cameraController;
	private bool isFlippedX = false;
	private bool isFlippedY = false;
	private bool isFlippedZ = false;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	float GetAxis (string axis, bool flip)
	{
		var keyAxis = axis + "k";
		var res = Input.GetAxis (axis) + Input.GetAxis (keyAxis);
		return flip ? -res : res;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float x = GetAxis ("ver3", isFlippedZ) + GetAxis ("hor2", false);
		float y = GetAxis ("hor1", false) + GetAxis ("hor3", false);
		float z = - GetAxis ("ver1", isFlippedX) - GetAxis ("ver2", isFlippedY);
		movementHero.move (new Vector3 (x, y, z).normalized);

		if (Input.GetButtonDown ("X-Flip")) {
			isFlippedX = ! isFlippedX;
			cameraController.flipX ();
		}
		if (Input.GetButtonDown ("Y-Flip")) {
			isFlippedY = ! isFlippedY;
			cameraController.flipY ();
		}
		if (Input.GetButtonDown ("Z-Flip")) {
			isFlippedZ = ! isFlippedZ;
			cameraController.flipZ ();
		}
	}
}
