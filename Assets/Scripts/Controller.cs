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
		GetAxis ("Hor1");
		GetAxis ("Ver1");	
		GetAxis ("Hor2");
		GetAxis ("Ver2");	
		GetAxis ("Hor3");
		GetAxis ("Ver3");	
	}
}
