using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject[] hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	public GUIText speedText;
	public int PowerUpEvery;
	public GameObject[] PowerUps;
	public float AsteroidSpeed;
	private int hazardType = 0;

    private int PowerUpNumber = 0;
	private int waveCounter = 0;
	private int score;
	private bool gameOver;
	private bool restart;
	private bool LightSpeed = false;
	public bool Instructions = false;
	public int FirstTime = 0;
	private int BossGlitch = 0;

	public bool Paused = false;

	public int Level;
	public GameObject Boss;
	public GameObject Portal;

	public GameObject InstructionAccel;
	public GameObject InstructionsTouch;

	private string PlayerName;

	private bool UsingTouchControls;

	void Start() {
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";

		if (PlayerPrefs.HasKey("FirstTime")) {
			FirstTime = PlayerPrefs.GetInt("FirstTime");
		}

		StartCoroutine (SpawnWaves());
		SpawnPowerUp ();

		score = 0;
		UpdateScore ();

		if (PlayerPrefs.HasKey("PlayerName")) {
			PlayerName = PlayerPrefs.GetString("PlayerName");
		} else {
			PlayerName = "NoName";
		}

		if(FirstTime == 0) {
			startWait = 10;
		}


		if(Level == 1) {
			WWW www = new WWW("http://limitless-harbor-7022.herokuapp.com/api?NAME="+PlayerName+"&SCORE=555&LEVEL=1");
		}



	}

	IEnumerator SpawnWaves() {

		GameObject thePlayerControl = GameObject.Find("Player");
		PlayerController playerControl = thePlayerControl.GetComponent<PlayerController>();
		
		//		Instructions
		if (FirstTime == 0 && Level == 1) {

			Time.timeScale = 0.0f;

			if (playerControl.UseTouchControls) {
				InstructionsTouch.SetActive(true);
				UsingTouchControls = true;
			} else {
				InstructionAccel.SetActive(true);
				UsingTouchControls = false;
			}
		}

		yield return new WaitForSeconds(startWait);

		while(!LightSpeed && FirstTime == 1) {

			// Create Asteroids
			for (int i = 0; i < hazardCount; i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValues.x,spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard[hazardType], spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
			}

			//Create powerups
			waveCounter++;
			if (waveCounter == PowerUpEvery) {
				if (PowerUpNumber < PowerUps.Length - 1) {
					PowerUpNumber++;
				} else {
					PowerUpNumber = 0;
				}
				SpawnPowerUp();
				waveCounter = 0;
			}

			// Pause between waves
			yield return new WaitForSeconds(waveWait); 

			GameObject thePlayer = GameObject.Find("Player");
			PlayerController playerController = thePlayer.GetComponent<PlayerController>();
			playerController.fireRate -= 0.005f;

			//Make stuff harder
			AsteroidSpeed -= 0.25f;

			UpdateSpeed(0.2f);

			hazardCount++;
			if (spawnWait > 0.15f) {
				spawnWait -= 0.05f;
			}

		} // End While Loop
	}

	public void InstructionsPausePlay() {
		Time.timeScale = 1.0f;
		InstructionAccel.SetActive(false);

		if (UsingTouchControls) {
			InstructionsTouch.SetActive(false);
		} else {
			InstructionAccel.SetActive(false);
		}

		FirstTime = 1;
		PlayerPrefs.SetInt ("FirstTime", FirstTime);
		PlayerPrefs.Save ();
	}
	
	public void UpdateSpeed(float amount) {
		GameObject theTrack = GameObject.Find("Track");
		TrackScript trackController = theTrack.GetComponent<TrackScript>();
		trackController.trackSpeed += amount;
		speedText.text = (trackController.trackSpeed/5.0f-0.20f).ToString("n2");

		if ((trackController.trackSpeed/5.0f-0.20f) > 0.5f) {
			hazardType = 1;
		}

		if ((trackController.trackSpeed/5.0f-0.20f) >= 1.0f) {
			LightSpeedAchieved();
		}
	}

	void SetLightSpeed() {
		LightSpeed = true;
	}

	public void LightSpeedAchieved() {

		if (Level < 3) {
			LightSpeed = true;
			Vector3 portalPosition = new Vector3(-0.32f,0f,5.33f);
			Quaternion portalRotation = Quaternion.Euler (90f,0f,0f);
			Instantiate(Portal, portalPosition, portalRotation);
		}

		if (Level == 3) {
			if (BossGlitch < 1) {
				hazardType ++;
				Vector3 bossPosition = new Vector3(0.5f,0.0f,0.0f);
				Quaternion bossRotation = Quaternion.identity;
				Instantiate(Boss, bossPosition, bossRotation);
				BossGlitch++;
			}
		}

	}

	IEnumerator NextLevel() {

		GameObject.Find ("ScreenLog").SendMessage ("LogToScreen","LEVEL COMPLETE");

		WWW www = new WWW("http://limitless-harbor-7022.herokuapp.com/api?NAME="+PlayerName+"&SCORE="+score+"&LEVEL="+Level+"");

		yield return new WaitForSeconds (3);

		if (Level == 1) {
			PlayerPrefs.SetInt("Level", 2);
			PlayerPrefs.Save();
			Application.LoadLevel("Level2");
		}

		if (Level == 2) {
			PlayerPrefs.SetInt("Level", 3);
			PlayerPrefs.Save();
			Application.LoadLevel("Level3");
		}

		if (Level == 3) {
			GameObject.Find ("ScreenLog").SendMessage ("LogToScreen","GAME COMPLETE!!!");
			yield return new WaitForSeconds(7);
			Application.LoadLevel("Credits");
		}


	}

	public void SpawnPowerUp() {
		Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValues.x,spawnValues.x), spawnValues.y, spawnValues.z);
		Quaternion spawnRotation = Quaternion.identity;
        Instantiate(PowerUps[PowerUpNumber], spawnPosition, spawnRotation);
	}

	public void AddScore(int newScoreValue) {
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore() {
		scoreText.text = "" + score;
	}

	public void GameOver () {
		gameOverText.text = "Game Over";
		gameOver = true;
		restartText.text = "Restart";
		restart = true;
	}
}
