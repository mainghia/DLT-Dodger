using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
	//Game State Variables
	public static bool pause = false;
	public static bool playing = true;
	public static bool end = false;
	public static bool home = false;

	//Screen Variables
	public GameObject HomeScreen;
	public GameObject PauseScreen;
	public GameObject PlayScreen;
	public GameObject EndScreen;
	public GameObject ShopScreen;

	//Map Generate Variables
	public Transform generateRightPoint;
	public Transform generateUpPoint;
	public Transform generateLeftPoint;
	public Transform generateDownPoint;
	public bool spawnCoroutine = false;
	public bool spawnItemCoroutine = false;
	public Vector3 lastItemPos;
	public Transform item11;
	public Transform item21;
	public Transform item31;
	public Transform item12;
	public Transform item22;
	public Transform item32;
	public Transform item13;
	public Transform item23;
	public Transform item33;



	//	public GameObject groundLandTotal;

	//Score Variables
	private int score;
	private int highScore;
	public int bananas;
	public Text playingLivesLabel;
	public Text playingBananaLabel;
	public Text pauseLivesLabel;
	public Text pauseBananaLabel;
	public Text endBananaLabel;
	public Text endHighScoreLabel;


	//Manager Variables
	//public GameObject Instantiator;
	static Game _instance;

	public static Game instance {
		get {
			return _instance;
		}
	}

	void Awake ()
	{
		if (_instance != null) {
			Debug.LogError ("Multiple Game Instances Exist.");
			Destroy (this.gameObject);
		} else {
			_instance = this;
		}

	}

	void Start ()
	{		
		home = true;
		pause = false;
		end = false;
		playing = false;
		highScore = PlayerPrefs.GetInt ("HighScore");
		if (highScore == 0) {
			highScore = 0;
			PlayerPrefs.SetInt ("HighScore", 0);
		}	
		bananas = 0;
//		SoundManager.instance.PlayTitleMusic();
	}

	void Update ()
	{
		if (playing) {
			SetScore ();
			SetLives ();
		}
	}


	void FixedUpdate ()
	{
		if (!spawnCoroutine && playing) {
			StartCoroutine (SpawnThingsRight ());
		}
	}

	public void SpawnBananaItems ()
	{
		Debug.Log ("Random");
		if (!spawnItemCoroutine) {
			int rd = Random.Range (0, 9);
			switch (rd) {
			case 0:
				StartCoroutine (SpawnBananaCoroutine (item11.position));
				break;
			case 1:
				StartCoroutine (SpawnBananaCoroutine (item21.position));
				break;
			case 2:
				StartCoroutine (SpawnBananaCoroutine (item31.position));
				break;
			case 3:
				StartCoroutine (SpawnBananaCoroutine (item12.position));
				break;
			case 4:
				StartCoroutine (SpawnBananaCoroutine (item22.position));
				break;
			case 5:
				StartCoroutine (SpawnBananaCoroutine (item32.position));
				break;
			case 6:
				StartCoroutine (SpawnBananaCoroutine (item13.position));
				break;
			case 7:
				StartCoroutine (SpawnBananaCoroutine (item23.position));
				break;
			case 8:
				StartCoroutine (SpawnBananaCoroutine (item33.position));
				break;
			default:
				break;
			}

		}
	}

	IEnumerator SpawnBananaCoroutine (Vector3 targetPos)
	{
		if (lastItemPos != targetPos) {
			Debug.Log (targetPos);
			spawnItemCoroutine = true;
			InstantiateObjects.current.SpawnBanana (targetPos);
			yield return new WaitForSeconds (0f);
			spawnItemCoroutine = false;
			lastItemPos = targetPos;
		} else {
			SpawnBananaItems ();
		}
	}

	IEnumerator SpawnThingsRight ()
	{
		spawnCoroutine = true;
		int rd = Random.Range (0, 10);
		switch (rd) {
		case 0:
			InstantiateObjects.current.SpawnBananaRight (generateRightPoint.position.x);
			break;
		case 1:
			InstantiateObjects.current.SpawnBananaUp (generateUpPoint.position.y);
			break;
		case 2:
			InstantiateObjects.current.SpawnBananaLeft (generateLeftPoint.position.x);		
			break;
		case 3:
			InstantiateObjects.current.SpawnBananaDown (generateDownPoint.position.y);		
			break;
		case 4:
			InstantiateObjects.current.SpawnBananaDown (generateDownPoint.position.y);
			InstantiateObjects.current.SpawnBananaDown (generateDownPoint.position.y);
			break;
		case 5:
			InstantiateObjects.current.SpawnBananaDown (generateDownPoint.position.y);
			InstantiateObjects.current.SpawnBananaRight (generateRightPoint.position.x);
			break;
		case 6:
			InstantiateObjects.current.SpawnBananaUp (generateUpPoint.position.y);
			InstantiateObjects.current.SpawnBananaDown (generateDownPoint.position.y);
			break;
		case 7:
			InstantiateObjects.current.SpawnBananaDown (generateDownPoint.position.y);
			InstantiateObjects.current.SpawnBananaDown (generateDownPoint.position.y);
			InstantiateObjects.current.SpawnBananaRight (generateRightPoint.position.x);		
			break;
		case 8:
			InstantiateObjects.current.SpawnBananaLeft (generateLeftPoint.position.x);	
			InstantiateObjects.current.SpawnBananaRight (generateRightPoint.position.x);			
			break;
		case 9:
			InstantiateObjects.current.SpawnBananaLeft (generateLeftPoint.position.x);
			InstantiateObjects.current.SpawnBananaDown (generateDownPoint.position.y);
			InstantiateObjects.current.SpawnBananaRight (generateRightPoint.position.x);			
			break;
		case 10:
			InstantiateObjects.current.SpawnBananaUp (generateUpPoint.position.y);
			InstantiateObjects.current.SpawnBananaDown (generateDownPoint.position.y);
			InstantiateObjects.current.SpawnBananaRight (generateRightPoint.position.x);		
			break;
		default:
			break;

		}
		yield return new WaitForSeconds (1.3f);
		spawnCoroutine = false;
	}

	void SetScore ()
	{
		playingBananaLabel.text = bananas.ToString ();
	}

	void SetLives ()
	{
		playingLivesLabel.text = Health.instance.lives.ToString ();
	}

	void SetPauseScore ()
	{
		pauseBananaLabel.text = bananas.ToString ();
	}

	void SetPauseLives ()
	{
		pauseLivesLabel.text = Health.instance.lives.ToString ();
	}

	void SetFinalBanana ()
	{
		endBananaLabel.text = bananas.ToString ();
	}

	void SetFinalScore ()
	{
		if (bananas > PlayerPrefs.GetInt ("HighScore")) {
			PlayerPrefs.SetInt ("HighScore", bananas);
			endHighScoreLabel.text = PlayerPrefs.GetInt ("HighScore").ToString ();
			return;
		}
		endHighScoreLabel.text = PlayerPrefs.GetInt ("HighScore").ToString ();
	}

	public void Home ()
	{
		//SoundManager.instance.PlayButtonSound ();
		//SoundManager.instance.PlayTitleMusic();
		home = true;
		playing = false;
		pause = false;
		end = false;
		PlayScreen.SetActive (false);
		PauseScreen.SetActive (false);
		EndScreen.SetActive (false);
		//ShopScreen.SetActive (false);
		HomeScreen.SetActive (true);
		highScore = PlayerPrefs.GetInt ("HighScore");
		InstantiateObjects.current.DestroyObjects ();
	}

	public void Play ()
	{
		//SoundManager.instance.PlayButtonSound ();
		playing = true;
		home = false;
		pause = false;
		end = false;
		PlayScreen.SetActive (true);
		PauseScreen.SetActive (false);
		EndScreen.SetActive (false);
		HomeScreen.SetActive (false);
		bananas = 0;
		highScore = PlayerPrefs.GetInt ("HighScore");
		InstantiateObjects.current.InstanObjects ();	     
		SetScore ();
	}

	public void RePlay ()
	{
			
		//SoundManager.instance.PlayButtonSound ();
		playing = true;
		home = false;
		pause = false;
		end = false;
		PlayScreen.SetActive (true);
		PauseScreen.SetActive (false);
		EndScreen.SetActive (false);
		HomeScreen.SetActive (false);			
		highScore = PlayerPrefs.GetInt ("HighScore");
		InstantiateObjects.current.DestroyObjects ();
		InstantiateObjects.current.InstanObjects ();
		PlayerController.instance.Reset ();
		SetScore ();
		bananas = 0;
	}

	public void Pause ()
	{
		//SoundManager.instance.PlayButtonSound ();
		if (!pause) {
			pause = true;
			home = false;
			playing = false;
			end = false;
			PlayScreen.SetActive (false);
			PauseScreen.SetActive (true);
			EndScreen.SetActive (false);
			HomeScreen.SetActive (false);
			SetPauseLives ();
			SetPauseScore ();
		} else {
			home = false;
			pause = false;
			playing = true;
			PlayScreen.SetActive (true);
			PauseScreen.SetActive (false);	
		}
	}

	public void EndGame ()
	{
		//App.instance.showInterstitialAd ();
		home = false;
		pause = false;
		playing = false;
		end = true;
		PlayScreen.SetActive (false);
		PauseScreen.SetActive (false);
		EndScreen.SetActive (true);
		HomeScreen.SetActive (false);
		highScore = PlayerPrefs.GetInt ("HighScore");
		SetFinalScore ();
		SetFinalBanana ();
		InstantiateObjects.current.DestroyObjects ();
	}

}
