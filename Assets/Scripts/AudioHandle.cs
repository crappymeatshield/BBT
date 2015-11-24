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
		GetComponent<AudioSource>().PlayOneShot(chime);
	}
	void Bubble() {
		GetComponent<AudioSource>().PlayOneShot(bubble);
	}
	void Chime() {
		GetComponent<AudioSource>().PlayOneShot(chime);
	}
	void ClickUp() {
		GetComponent<AudioSource>().PlayOneShot(cUp);
	}
	void ClickDown() {
		GetComponent<AudioSource>().PlayOneShot(cDw);
	}
	void Crack() {
		GetComponent<AudioSource>().PlayOneShot(crack);
	}
	void Die() {
		GetComponent<AudioSource>().PlayOneShot(lost);
	}
	void Ding() {
		GetComponent<AudioSource>().PlayOneShot(ding);
	}
	void Grunt() {
		GetComponent<AudioSource>().PlayOneShot(grunt);
	}
	void Site() {
		GetComponent<AudioSource>().PlayOneShot(site);
	}
	void Tweet() {
		GetComponent<AudioSource>().PlayOneShot(tweet);
	}
	void Urp() {
		GetComponent<AudioSource>().PlayOneShot(urp);
	}



}
