using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Camera cameraX;
	public Camera cameraY;
	public Camera cameraZ;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void flipX() {
		cameraX.transform.Rotate (new Vector3 (180, 0, 0));

	}

	public void flipY() {
		cameraY.transform.Rotate (new Vector3 (180, 0, 0));
	}

	public void flipZ() {
		cameraZ.transform.Rotate (new Vector3 (180, 0, 0));
	}
}
