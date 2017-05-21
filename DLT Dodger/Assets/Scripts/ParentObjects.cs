using UnityEngine;
using System.Collections;

public class ParentObjects : MonoBehaviour {
	static ParentObjects instance;
	public static ParentObjects Instance {
		get {
			return instance;
		}
	}

	void Awake(){
		instance = this;
	}
}
