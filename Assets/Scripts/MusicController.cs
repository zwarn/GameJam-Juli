using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicController : MonoBehaviour {

	public List<AudioClip> clips;

	void Start() {
		this.GetComponent<AudioSource>().clip = clips[Random.Range (0, clips.Count)];
	}
}
