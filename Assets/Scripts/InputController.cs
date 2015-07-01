using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{

	public MovementHero movementHero;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		float rotX = Input.GetAxis ("ver1");
		float rotY = Input.GetAxis ("hor1");

		movementHero.rotate(new Vector3(rotX, 0, rotY));

		float speed = Input.GetAxis ("thrust1");
		Debug.Log (speed);

		movementHero.move (speed);
	}
}
