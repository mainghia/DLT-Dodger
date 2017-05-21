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
	private float distance;
	private int score;
	private int highScore;
	public int bananas;
	public Text playingLivesLabel;
	public Text playingBananaLabel;
	public Text pauseLivesLabel;
	public Text pauseBananaLabel;

	//	public UILabel coinLabel;
	//	public UILabel scoreLabel;
	//	public UILabel highScoreLabel;

	//Shop Variables
	//	public static int costume = 0;
	//	public static bool bought0 = false;
	//	public static bool bought1 = false;
	//	public static bool bought2 = false;
	//	public UILabel priceKaiokenLabel;
	//	public UILabel priceNormalLabel;
	//	public UILabel priceGodLabel;
	//	public UILabel shopCoinLabel;
	//	public GameObject notEnoughMoneyNotification;

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
	// UI Control Panel
	//		GameGui.instance.PushPanel ("HomePanel");

	/*	Yes NO dialogue
			AlertPanel.Show("Do you want to ...", (ok)=>{
				if(ok){
					
				}
				else{
					
				}
			},true);
*/
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

	//	void SetCoin(){
	//		playingCoinLabel.text = coins.ToString();
	//	}

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

	//	void SetFinalCoin(){
	//		PlayerPrefs.SetInt ("Coins",coins + PlayerPrefs.GetInt("Coins"));
	//		int finalCoin = PlayerPrefs.GetInt ("Coins");
	//		coinLabel.text = finalCoin.ToString();
	//	}
	//
	//	void SetFinalScore(){
	//		distance = playerController.current.transform.position.x;
	//		score = (int) distance / 2;
	//		scoreLabel.text = score + "m";
	//		if (score > PlayerPrefs.GetInt ("HighScore")) {
	//			PlayerPrefs.SetInt ("HighScore", score);
	//			highScoreLabel.text = PlayerPrefs.GetInt ("HighScore") + "m";
	//			return;
	//		}
	//		highScoreLabel.text = PlayerPrefs.GetInt ("HighScore") + "m";
	//	}

		public void Home(){
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
	//
	//	public void Shop(){
	//		SoundManager.instance.PlayButtonSound ();
	//		home = true;
	//		playing = false;
	//		pause = false;
	//		end = false;
	//		PlayScreen.SetActive (false);
	//		PauseScreen.SetActive (false);
	//		EndScreen.SetActive (false);
	//		HomeScreen.SetActive (false);
	//		coins = PlayerPrefs.GetInt ("Coins");
	//		shopCoinLabel.text = coins.ToString();
	//		if (bought1) {
	//			priceKaiokenLabel.text = "Bought";
	//		}
	//		if (bought2) {
	//			priceGodLabel.text = "Bought";
	//		}
	//		if (bought0) {
	//			priceNormalLabel.text = "Bought";
	//		}
	//		if (costume == 0 && bought0) {
	//			priceNormalLabel.text = "Equipped";
	//			priceNormalLabel.transform.parent.transform.GetChild (0).gameObject.SetActive (true);
	//		} else if (costume == 1 && bought1) {
	//			priceKaiokenLabel.text = "Equipped";
	//			priceKaiokenLabel.transform.parent.transform.GetChild (0).gameObject.SetActive (true);
	//		} else if (costume == 2 && bought2) {
	//			priceGodLabel.text = "Equipped";
	//			priceGodLabel.transform.parent.transform.GetChild (0).gameObject.SetActive (true);
	//		}
	//		ShopScreen.SetActive (true);
	//
	//	}
	//
	//	public void BuyCostume(UILabel obj){
	//		coins = PlayerPrefs.GetInt ("Coins");
	//		if (obj.text.Equals("Costume 2")) {
	//			if (coins >= 50 && !bought1) {
	//				coins -= 50;
	//				PlayerPrefs.SetInt ("Coins",coins);
	//				costume = 1;
	//				bought1 = true;
	//				PlayerPrefs.SetInt ("Costume1",1);
	//				Shop ();
	//				Debug.Log ("Costume 2");
	//			}else if(bought1){
	//				costume = 1;
	//				priceKaiokenLabel.text = "Equipped";
	//				obj.gameObject.transform.parent.transform.GetChild (0).gameObject.SetActive (true);
	//				if (bought0) {
	//					priceNormalLabel.text = "Bought";
	//					priceNormalLabel.transform.parent.transform.GetChild (0).gameObject.SetActive (false);
	//				}
	//				if (bought2) {
	//					priceGodLabel.text = "Bought";
	//					priceGodLabel.transform.parent.transform.GetChild (0).gameObject.SetActive (false);
	//				}
	//			}else if(coins < 50){
	//				notEnoughMoneyNotification.SetActive (true);
	//				ShopScreen.SetActive (false);
	//			}
	//		} else if(obj.text == "Costume 1"){
	//			if(coins >= 50 && !bought0 ) {
	//				coins -= 50;
	//				PlayerPrefs.SetInt ("Coins",coins);
	//				costume = 0;
	//				bought0 = true;
	//				PlayerPrefs.SetInt ("Costume0",1);
	//				Shop ();
	//				Debug.Log ("Costume 1");
	//			}else if(bought0){
	//				costume = 0;
	//				priceNormalLabel.text = "Equipped";
	//				priceNormalLabel.transform.parent.transform.GetChild (0).gameObject.SetActive (true);
	//				if (bought1) {
	//					priceKaiokenLabel.text = "Bought";
	//					priceKaiokenLabel.transform.parent.transform.GetChild (0).gameObject.SetActive (false);
	//				}
	//				if (bought2) {
	//					priceGodLabel.text = "Bought";
	//					priceGodLabel.transform.parent.transform.GetChild (0).gameObject.SetActive (false);
	//				}
	//			}else if(coins < 50){
	//				notEnoughMoneyNotification.SetActive (true);
	//				ShopScreen.SetActive (false);
	//			}
	//		}	else if(obj.text == "Costume 3"){
	//			if(coins >= 300 && !bought2 ) {
	//				coins -= 300;
	//				PlayerPrefs.SetInt ("Coins",coins);
	//				costume = 2;
	//				bought2 = true;
	//				PlayerPrefs.SetInt ("Costume2",1);
	//				Shop ();
	//				Debug.Log ("Costume 3");
	//			}
	//			else if(bought2){
	//				costume = 2;
	//				priceGodLabel.text = "Equipped";
	//				priceGodLabel.transform.parent.transform.GetChild (0).gameObject.SetActive (true);
	//				if (bought0) {
	//					priceNormalLabel.text = "Bought";
	//					priceNormalLabel.transform.parent.transform.GetChild (0).gameObject.SetActive (false);
	//				}
	//				if (bought1) {
	//					priceKaiokenLabel.text = "Bought";
	//					priceKaiokenLabel.transform.parent.transform.GetChild (0).gameObject.SetActive (false);
	//				}
	//			}else if(coins < 300){
	//				notEnoughMoneyNotification.SetActive (true);
	//				ShopScreen.SetActive (false);
	//			}
	//		}
	//	}
	//
	//	public void CloseNotification(){
	//		notEnoughMoneyNotification.SetActive (false);
	//		ShopScreen.SetActive (true);
	//	}
	//
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
		//SetScore ();
	}
	//
	//	public void RePlay(){
	//		UM_AdManager.LoadInterstitialAd();
	//		App.instance.hideBanner ();
	//		SoundManager.instance.PlayButtonSound ();
	//		playing = true;
	//		home = false;
	//		pause = false;
	//		end = false;
	//		PlayScreen.SetActive (true);
	//		PauseScreen.SetActive (false);
	//		EndScreen.SetActive (false);
	//		HomeScreen.SetActive (false);
	//		generatePlatformSpace = 4f;
	//		generateGroundSpace = 4f;
	//		highScore = PlayerPrefs.GetInt ("HighScore");
	//		InstantiateObjects.current.DestroyObjects ();
	//		InstantiateObjects.current.InstanObjects ();
	//
	//		groundLandTotal = InstantiateObjects.current.pObj.transform.Find("GroundLandTotal(Clone)").gameObject;
	//		if (Game.costume == 1) {
	//			groundLandTotal.transform.GetChild (1).gameObject.SetActive (true);
	//			groundLandTotal.transform.GetChild (0).gameObject.SetActive (false);
	//			Debug.Log ("Naruto");
	//		} else if(Game.costume == 0){
	//			groundLandTotal.transform.GetChild (0).gameObject.SetActive (true);
	//			groundLandTotal.transform.GetChild (1).gameObject.SetActive (false);
	//			Debug.Log ("Goku");
	//		} else if(Game.costume == 2){
	//			groundLandTotal.transform.GetChild (0).gameObject.SetActive (true);
	//			groundLandTotal.transform.GetChild (1).gameObject.SetActive (false);
	//			Debug.Log ("Goku");
	//		}
	//
	//		cameraFollow2DPlayer.cam.ResetCamera ();
	//		cameraFollow2DPlayer.cam.ActiveCamera ();
	//		distance = playerController.current.transform.position.x;
	//		SetScore ();
	//		coins = 0;
	//	}
	//
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
	
		public void EndGame(){
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
			//SetFinalScore ();
			//SetFinalCoin ();
			InstantiateObjects.current.DestroyObjects ();
		}

}
