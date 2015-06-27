using UnityEngine;
using System.Collections;

public class DiscoScript : MonoBehaviour {

	private MeshRenderer renderer;
	public Material mat1;
	public Material mat2;

	// Use this for initialization
	void Start () {
		Renderer = GetComponent<MeshRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Mathf.RoundToInt (Time.time) % 2 == 0) {
			renderer.material = mat1;
		} else {
			renderer.material = mat2;
		}

	}
}
