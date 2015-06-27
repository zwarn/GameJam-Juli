using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{

	void Start ()
	{
		print ("Current Joysticks:");
		foreach (var item in Input.GetJoystickNames ()) {
			print (item);	
		}		
	}

	float GetAxis (string axis)
	{
		float x = Input.GetAxis (axis);
		if (x != 0.0f) {
			print (axis + ": " + x);
		}
		return x;
	}

	void Update ()
	{
		GetAxis ("hor1");
		GetAxis ("ver1");	
		GetAxis ("hor2");
		GetAxis ("ver2");	
		GetAxis ("hor3");
		GetAxis ("ver3");	
	}
}
