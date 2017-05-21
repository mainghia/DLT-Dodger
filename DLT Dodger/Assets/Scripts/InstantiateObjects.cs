using UnityEngine;
using System.Collections;

public class InstantiateObjects : MonoBehaviour
{
	public GameObject mapTotal;
	public static InstantiateObjects current;

	GameObject obj, obj1, obj2, obj3, obj4, obj5, obj6;


	public GameObject parentObj;
	public GameObject pObj;

	void Awake ()
	{
		current = this;
	}
	// Use this for initialization
	void Start ()
	{
		//pObj =  Instantiate (parentObj) as GameObject;

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void InstanObjects ()
	{
		pObj = Instantiate (parentObj) as GameObject;
		ObjectPooler.current.RePoolObjects ();
		obj = Instantiate (mapTotal) as GameObject;
		obj.transform.parent = ParentObjects.Instance.transform;
		obj.transform.localPosition = new Vector3 (-1f, transform.localPosition.y, transform.localPosition.z);
		obj.SetActive (true);
	}

	public void ReInstanObjects ()
	{
		pObj = Instantiate (parentObj) as GameObject;
		ObjectPooler.current.RePoolObjects ();
		obj = Instantiate (mapTotal) as GameObject;
		obj.transform.parent = ParentObjects.Instance.transform;
		obj.transform.localPosition = new Vector3 (-1f, transform.localPosition.y, transform.localPosition.z);
		obj.SetActive (true);
	}

	public void SpawnBananaRight (float space)
	{
		obj1 = (GameObject)ObjectPooler.current.GetPooledBananaRight ();
		if (obj1 != null) {
			obj1.transform.parent = ParentObjects.Instance.transform;
			int rd = Random.Range (0, 3);
			switch (rd) {
			case 0:
				obj1.transform.position = new Vector3 (space, -65f, 0f);
				break;
			case 1:
				obj1.transform.position = new Vector3 (space, -10f, 0f);
				break;
			case 2:
				obj1.transform.position = new Vector3 (space, 40f, 0f);
				break;
			default:
				break;
			}
			obj1.SetActive (true);
		}	
	}

	public void SpawnBananaLeft (float space)
	{
		obj2 = (GameObject)ObjectPooler.current.GetPooledBananaLeft ();
		if (obj2 != null) {
			obj2.transform.parent = ParentObjects.Instance.transform;
			int rd = Random.Range (0, 3);
			switch (rd) {
			case 0:
				obj2.transform.position = new Vector3 (space, -65f, 0f);
				break;
			case 1:
				obj2.transform.position = new Vector3 (space, -10f, 0f);
				break;
			case 2:
				obj2.transform.position = new Vector3 (space, 40f, 0f);
				break;
			default:
				break;
			}
			obj2.SetActive (true);
		}	
	}

	public void SpawnBananaUp (float space)
	{
		obj3 = (GameObject)ObjectPooler.current.GetPooledBananaUp ();
		if (obj3 != null) {
			obj3.transform.parent = ParentObjects.Instance.transform;
			int rd = Random.Range (0, 3);
			switch (rd) {
			case 0:
				obj3.transform.position = new Vector3 (-50f, space, 0f);
				break;
			case 1:
				obj3.transform.position = new Vector3 (0f, space, 0f);
				break;
			case 2:
				obj3.transform.position = new Vector3 (50f, space, 0f);
				break;
			default:
				break;
			}

			obj3.SetActive (true);
		}	
	}

	public void SpawnBananaDown (float space)
	{
		obj4 = (GameObject)ObjectPooler.current.GetPooledBananaDown ();
		if (obj4 != null) {
			obj4.transform.parent = ParentObjects.Instance.transform;
			int rd = Random.Range (0, 3);
			switch (rd) {
			case 0:
				obj4.transform.position = new Vector3 (-50f, space, 0f);
				break;
			case 1:
				obj4.transform.position = new Vector3 (0f, space, 0f);
				break;
			case 2:
				obj4.transform.position = new Vector3 (50f, space, 0f);
				break;
			default:
				break;
			}

			obj4.SetActive (true);
		}	
	}

	public void SpawnBanana (Vector3 targetPos)
	{
		obj6 = (GameObject)ObjectPooler.current.GetPooledBanana ();
		if (obj6 != null) {
			obj6.transform.parent = ParentObjects.Instance.transform;
			obj6.transform.position = targetPos;
			obj6.SetActive (true);
		}
	}
		
	//	public void SpawnCoins(float space){
	//		obj5 = (GameObject) ObjectPooler.current.GetPooledCoins();
	//		if (obj5 != null) {
	//			obj5.transform.parent = ParentObjects.Instance.transform;
	//			obj5.transform.position = new Vector3 (space, Random.Range(-1f,1.5f), 0f);
	//			obj5.SetActive (true);
	//		}
	//	}

	public void DestroyObjects ()
	{
		Destroy (pObj);
	}


}
