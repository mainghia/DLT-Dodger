using UnityEngine;
using System.Collections;

public class ItemPickup : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (Game.playing) {
			if (other.tag == "Player") {
				if (tag == "Banana") {
					Debug.Log ("Banana Pickup");
					Game.instance.bananas++;
					Game.instance.SpawnBananaItems ();
					gameObject.SetActive (false);
				}
				if (tag == "BananaUp" || tag == "BananaDown" || tag == "BananaRight" || tag == "BananaLeft") {
					Debug.Log ("Hit");
				}
			}
		}
	}
}
