using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	public int lives = 3;
	static Health _instance;

	public static Health instance {
		get {
			return _instance;
		}
	}

	void Awake ()
	{
		if (_instance != null) {
			Debug.LogError ("Multiple Health Instances Exist.");
			Destroy (this.gameObject);
		} else {
			_instance = this;
		}
	}

	void Update ()
	{
		if (lives == 0 && PlayerController.instance.isDead == false) {
			PlayerController.instance.isDead = true;
			PlayerController.instance.Die ();
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (!PlayerController.instance.isHit) {
			if (other.tag == "BananaUp" || other.tag == "BananaDown" || other.tag == "BananaRight" || other.tag == "BananaLeft") {
				lives--;
				PlayerController.instance.Invulnerable ();
			}
		}
	}
}
