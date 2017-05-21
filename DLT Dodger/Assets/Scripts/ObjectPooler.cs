using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour
{

	public static ObjectPooler current;

	public static ObjectPooler Instance {
		get {
			return current;
		}
	}


	public GameObject pooledBanana;
	public GameObject pooledBananaRight;
	public GameObject pooledBananaUp;
	public GameObject pooledBananaLeft;
	public GameObject pooledBananaDown;



	public int pooledAmount = 5;
	public bool willGrow = true;

	public List<GameObject> pooledBananas;
	public List<GameObject> pooledBananasRight;
	public List<GameObject> pooledBananasUp;
	public List<GameObject> pooledBananasLeft;
	public List<GameObject> pooledBananasDown;


	// Use this for initialization
	void Awake ()
	{
		current = this;
	}

	void Start ()
	{
		if (ParentObjects.Instance != null) {
			RePoolObjects ();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void RePoolObjects ()
	{	
		pooledBananas = new List<GameObject> ();
		for (int i = 0; i < pooledAmount; i++) {
			GameObject obj5 = (GameObject)Instantiate (pooledBanana);
			obj5.transform.parent = ParentObjects.Instance.transform;
			obj5.SetActive (false);
			pooledBananas.Add (obj5);
		}

		pooledBananasRight = new List<GameObject> ();
		for (int i = 0; i < pooledAmount; i++) {
			GameObject obj1 = (GameObject)Instantiate (pooledBananaRight);
			obj1.transform.parent = ParentObjects.Instance.transform;
			obj1.SetActive (false);
			pooledBananasRight.Add (obj1);
		}

		pooledBananasUp = new List<GameObject> ();
		for (int i = 0; i < pooledAmount; i++) {
			GameObject obj2 = (GameObject)Instantiate (pooledBananaUp);
			obj2.transform.parent = ParentObjects.Instance.transform;
			obj2.SetActive (false);
			pooledBananasUp.Add (obj2);
		}

		pooledBananasLeft = new List<GameObject> ();
		for (int i = 0; i < pooledAmount; i++) {
			GameObject obj3 = (GameObject)Instantiate (pooledBananaLeft);
			obj3.transform.parent = ParentObjects.Instance.transform;
			obj3.SetActive (false);
			pooledBananasLeft.Add (obj3);
		}

		pooledBananasDown = new List<GameObject> ();
		for (int i = 0; i < pooledAmount; i++) {
			GameObject obj4 = (GameObject)Instantiate (pooledBananaDown);
			obj4.transform.parent = ParentObjects.Instance.transform;
			obj4.SetActive (false);
			pooledBananasDown.Add (obj4);
		}
	}


	public GameObject GetPooledBanana ()
	{
		for (int i = 0; i < pooledBananas.Count; i++) {
			if (!pooledBananas [i].activeInHierarchy) {
				return pooledBananas [i];
			}
		}
		return null;
	}

	public GameObject GetPooledBananaRight ()
	{
		for (int i = 0; i < pooledBananasRight.Count; i++) {
			if (!pooledBananasRight [i].activeInHierarchy) {
				return pooledBananasRight [i];
			}
		}
		return null;
	}

	public GameObject GetPooledBananaUp ()
	{
		for (int i = 0; i < pooledBananasUp.Count; i++) {
			if (!pooledBananasUp [i].activeInHierarchy) {
				return pooledBananasUp [i];
			}
		}
		return null;
	}

	public GameObject GetPooledBananaLeft ()
	{
		for (int i = 0; i < pooledBananasLeft.Count; i++) {
			if (!pooledBananasLeft [i].activeInHierarchy) {
				return pooledBananasLeft [i];
			}
		}
		return null;
	}

	public GameObject GetPooledBananaDown ()
	{
		for (int i = 0; i < pooledBananasDown.Count; i++) {
			if (!pooledBananasDown [i].activeInHierarchy) {
				return pooledBananasDown [i];
			}
		}
		return null;
	}
		

	/* How to get them out
	 * GameObject obj = ObjectPooler.current.GetPooledObject();
	 * if(obj == null) return;
	 * obj.transform.position = transform.position;
	 * obj.transform.rotation = transform.rotation;
	 * obj.SetActive(true);
	*/
}
