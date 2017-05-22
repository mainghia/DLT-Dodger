using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	static CameraController _instance;
	public int cameraZoomIn = 150;
	public int cameraZoomOut = 200;
	public int zoomSpeed = 10;

	public static CameraController instance {
		get {
			return _instance;
		}
	}

	void Awake ()
	{
		if (_instance != null) {
			Debug.LogError ("Multiple Camera Instances Exist.");
			Destroy (this.gameObject);
		} else {
			_instance = this;
		}
	}

	public void Reset (){
		Camera.main.orthographicSize = 200;
	}

	public void CameraZoomOnDie(){
		if (PlayerController.instance.isDead) {
			StartCoroutine (CameraZoomCoroutine ());
		}
	}

	private IEnumerator CameraZoomCoroutine(){
		Camera.main.orthographicSize = Mathf.Lerp (Camera.main.orthographicSize, cameraZoomIn, Time.deltaTime * zoomSpeed);
		yield return new WaitForSeconds (1f);
	}

	public void ScreenShake() {
		if(!PlayerController.instance.isDead)
		StartCoroutine(AnimateScreenShake());
	}

	private IEnumerator AnimateScreenShake() {
		Vector3 originalPosition = transform.position;
		float time = 0;
		while (time < 0.15f)
		{
			transform.position = new Vector3(originalPosition.x + Random.Range(-3f, 3f), transform.position.y, transform.position.z);
			time += Time.deltaTime;
			yield return null;
		}
		transform.position = originalPosition;
	}

}
