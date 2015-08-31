using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {
	
	public GameObject PauseMenu;
	private GameController gameController;
	private int GetLevel;
	private int Moved = 0;
	private int once = 0;
	private string UserName;

	void Start() {
		GetLevel = PlayerPrefs.GetInt("Level");

		if (PlayerPrefs.HasKey("PlayerName")) {
			UserName = PlayerPrefs.GetString ("PlayerName");
		} else {
			UserName = "Not Yet Defined";
		}

	}

	void OnMouseDown() {

		if (Application.loadedLevel == 1 || Application.loadedLevel == 4 | Application.loadedLevel == 5) { // If in the game
			GameObject GameCont = GameObject.Find("GameController");
			GameController gameController = GameCont.GetComponent<GameController>();

			if(gameObject.name == "PauseButton") {

				audio.Play();

				
				if(gameController.Paused) {
					Time.timeScale = 1.0f;
					gameController.Paused = false;
					PauseMenu.SetActive(false);
					gameController.audio.Play();
					audio.Play();
				} else {
					Time.timeScale = 0.0f;
					gameController.Paused = true;
					PauseMenu.SetActive(true);
					gameController.audio.Pause();
				}
				GameObject.Find ("Track").SendMessage ("PauseTrack");
				
			}// End if
		}

		if(gameObject.name == "StartGameTrigger") {
			Application.LoadLevel ("Main");
		}

		if(gameObject.name == "ContinueTrigger") {
			if(GetLevel == 2) {
				Application.LoadLevel ("Level2");
			} else if (GetLevel == 3) {
				Application.LoadLevel ("Level3");
			} else {
				Application.LoadLevel ("Main");
			}
		}

		if(gameObject.name == "InstructionsTrigger") {
			Application.LoadLevel ("Instructions");
		}

		if(gameObject.tag == "CloseInstructions") {
			Debug.Log ("Tapped");
			GameObject.Find("GameController").SendMessage("InstructionsPausePlay");
		}

		if(gameObject.name == "OptionsTrigger") {
			Application.LoadLevel ("Options");
		}

		if(gameObject.name == "MainMenu") {
			Time.timeScale = 1.0f;
			Application.LoadLevel ("GUI");
		}

		if(gameObject.name == "CreditsTrigger") {
			Application.LoadLevel ("Credits");

		}

		if(gameObject.name == "NextSlide") {

			if(Moved > 1) {
				Application.LoadLevel("GUI");
				return;
			}

			GameObject.Find("Instructions").SendMessage("MoveLeft");
			Moved++;

		}

		if(gameObject.name == "HighScores") {
			Application.OpenURL("http://www.starkeydev.com/games/PlanetoidScores/");
		}

		if(gameObject.name == "OK") {
			gameObject.SetActive(false);
		}



	}
}