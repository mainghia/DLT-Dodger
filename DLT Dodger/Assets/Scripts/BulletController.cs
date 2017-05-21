using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public float moveSpeed;
	public float accelerate = 1f;
	public float xMove = 5f;
	public float yMove = 5f;
	public Vector2 moveVector;
	Animator animator;

	void Start () {
		xMove = transform.position.x;
		yMove = transform.position.y;
	}
	void Update () {
		if (!Game.pause && !Game.end) {
			if (tag == "BananaRight") {
				xMove -= accelerate * Time.deltaTime;
				moveVector = new Vector2 (xMove, transform.position.y);
			} else if (tag == "BananaUp")  {
				yMove -= accelerate * Time.deltaTime;
				moveVector = new Vector2 (transform.position.x, yMove);
			} else if (tag == "BananaLeft")  {
				xMove += accelerate * Time.deltaTime;
				moveVector = new Vector2 (xMove, transform.position.y);
			} else if (tag == "BananaDown")  {
				yMove += accelerate * Time.deltaTime;
				moveVector = new Vector2 (transform.position.x, yMove);
			}
			transform.position = moveVector;
		}			
	}

	void OnEnable(){
		xMove = transform.position.x;
	}

	void OnDisable(){
		CancelInvoke ();
	}

	void OnBecameInvisible(){
		Invoke ("Destroy",0);
	}

	void Destroy(){
		gameObject.SetActive (false);	
	}
}
