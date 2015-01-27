using UnityEngine;
using System.Collections;

public class AudioHandle : MonoBehaviour {

	public AudioClip tweet;
	public AudioClip urp;
	public AudioClip cUp;
	public AudioClip cDw;
	public AudioClip crack;
	public AudioClip ding;
	public AudioClip grunt;
	public AudioClip site;
	public AudioClip chime;
	public AudioClip lost;
	public AudioClip bubble;
	//MAKING A CHANGE LOL

	// Use this for initialization
	void Start () {
	
	}

	void Aura() {
		audio.PlayOneShot(chime);
	}
	void Bubble() {
		audio.PlayOneShot(bubble);
	}
	void Chime() {
		audio.PlayOneShot(chime);
	}
	void ClickUp() {
		audio.PlayOneShot(cUp);
	}
	void ClickDown() {
		audio.PlayOneShot(cDw);
	}
	void Crack() {
		audio.PlayOneShot(crack);
	}
	void Die() {
		audio.PlayOneShot(lost);
	}
	void Ding() {
		audio.PlayOneShot(ding);
	}
	void Grunt() {
		audio.PlayOneShot(grunt);
	}
	void Site() {
		audio.PlayOneShot(site);
	}
	void Tweet() {
		audio.PlayOneShot(tweet);
	}
	void Urp() {
		audio.PlayOneShot(urp);
	}



}
