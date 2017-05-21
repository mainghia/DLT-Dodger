using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public int jumpRange = 55;

	public Animator ani;
	SpriteRenderer sprtRen;
	Collider2D col;
	Vector2 firstPressPos;
	Vector2 secondPressPos;
	Vector2 currentSwipe;
	public float swipeDistance = 0.5f;
	public int cameraZoomIn = 150;
	public int cameraZoomOut = 200;
	public int zoomSpeed = 10;
	public bool isJumping = false;
	public bool isHit = false;
	public bool isDead = false;
	public Vector2 moveDirection;
	static PlayerController _instance;

	public static PlayerController instance {
		get {
			return _instance;
		}
	}

	void Awake ()
	{
		if (_instance != null) {
			Debug.LogError ("Multiple PlayerController Instances Exist.");
			Destroy (this.gameObject);
		} else {
			_instance = this;
		}
	}

	void Start ()
	{
		sprtRen = GetComponent<SpriteRenderer> ();
		col = GetComponent<Collider2D> ();
	}


	void Update ()
	{
		if (Game.playing && !isDead) {
			if (!isJumping) {
				moveDirection = Vector2.zero;
			}
			DetectSwipe ();
		}
	}

	public void Die ()
	{
		if (Game.playing)
			StartCoroutine (DieCoroutine ());
	}

	IEnumerator DieCoroutine ()
	{
		isDead = true;
		ani.SetBool ("isDead", true);
		transform.position -= (Vector3) moveDirection * Time.deltaTime * jumpRange;
		Camera.main.orthographicSize = Mathf.Lerp (Camera.main.orthographicSize, cameraZoomIn, Time.deltaTime * zoomSpeed);
		yield return new WaitForSeconds (1f);
		Camera.main.orthographicSize = Mathf.Lerp (Camera.main.orthographicSize, cameraZoomOut, Time.deltaTime * zoomSpeed);
		Game.playing = false;
		Game.end = true;
		Game.instance.EndGame ();
	}


	public void Invulnerable ()
	{
		if (Game.playing)
			StartCoroutine (InvulnerableCoroutine ());
	}

	IEnumerator InvulnerableCoroutine ()
	{
		isHit = true;
		ani.SetBool ("isHit", isHit);
		sprtRen.color = Color.red;
		yield return new WaitForSeconds (0.2f);
		sprtRen.color = Color.white;
		yield return new WaitForSeconds (0.2f);
		sprtRen.color = Color.red;
		isHit = false;
		ani.SetBool ("isHit", isHit);
		yield return new WaitForSeconds (0.1f);
		sprtRen.color = Color.white;
	}

	IEnumerator Jump ()
	{
		isJumping = true;
		ani.SetBool ("isJumping", isJumping);
		transform.position += (Vector3)moveDirection * jumpRange;
		LimitPosition ();
		yield return new WaitForSeconds (0.3f);
		isJumping = false;
		ani.SetBool ("isJumping", isJumping);
	}

	void LimitPosition ()
	{
		if (transform.position.x < -45) {
			transform.position = new Vector3 (-45, transform.position.y, transform.position.z);
		}
		if (transform.position.x > 65) {
			transform.position = new Vector3 (65, transform.position.y, transform.position.z);
		}
		if (transform.position.y < -55) {
			transform.position = new Vector3 (transform.position.x, -55, transform.position.z);
		}
		if (transform.position.y > 55) {
			transform.position = new Vector3 (transform.position.x, 55, transform.position.z);
		}
	}


	public void DetectSwipe ()
	{
		if (Input.GetMouseButtonDown (0)) {
			firstPressPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		}
		if (Input.GetMouseButtonUp (0)) {
			secondPressPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			currentSwipe = new Vector2 (secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
			currentSwipe.Normalize ();

			//swipe upwards
			if (currentSwipe.y > 0 && currentSwipe.x > -swipeDistance && currentSwipe.x < swipeDistance) {
				//Debug.Log ("up swipe");
				moveDirection = new Vector2 (0, 1);
				StartCoroutine (Jump ());
			}
			//swipe down
			if (currentSwipe.y < 0 && currentSwipe.x > -swipeDistance && currentSwipe.x < swipeDistance) {
				//Debug.Log ("down swipe");
				moveDirection = new Vector2 (0, -1);
				StartCoroutine (Jump ());
			}
			//swipe left
			if (currentSwipe.x < 0 && currentSwipe.y > -swipeDistance && currentSwipe.y < swipeDistance) {
				//Debug.Log ("left swipe");
				moveDirection = new Vector2 (-1, 0);
				StartCoroutine (Jump ());
			}
			//swipe right
			if (currentSwipe.x > 0 && currentSwipe.y > -swipeDistance && currentSwipe.y < swipeDistance) {
				//Debug.Log ("right swipe");
				moveDirection = new Vector2 (1, 0);
				StartCoroutine (Jump ());
			}
		}
	}
}
