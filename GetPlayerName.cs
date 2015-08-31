using UnityEngine;
using System.Collections;

public class GetPlayerName : MonoBehaviour {

	private string PlayerName = "Enter Name";
	private float ScreenWidth;
	public int Level;
	private int TopVal;
	public bool TextFieldEnabled = true;

	private bool HasPlayerName;
	private bool IsGetNameLevel;

	// Use this for initialization
	void Start () {

		if (PlayerPrefs.HasKey("PlayerName")) {
			HasPlayerName = true;
		}
		
		if (Application.loadedLevel == 7) {
			IsGetNameLevel = true;
		}
		
		if(!HasPlayerName) {
			if (!IsGetNameLevel) {
//				if (iPhone.generation.ToString().IndexOf("iPad") == -1) {
					Application.LoadLevel("GetName");
//				}
			}
		}
		
		
		
		ScreenWidth = Screen.width / 2;
		
		if (PlayerPrefs.HasKey("PlayerName")) {
			PlayerName = PlayerPrefs.GetString ("PlayerName");
		}
		
		if (Level == 7) {
			TopVal = 350;
			OnGUI ();
		}
		
		if (Level == 1) {
			TopVal = Screen.height / 2 + Screen.height / 4;
			OnGUI ();
		}


	}

	void DisableText() {
		TextFieldEnabled = false;
		Time.timeScale = 1.0f;
		GameObject.Find("Track").SendMessage("PauseTrack");
	}

	void OnGUI() {

		GUIStyle myStyle = new GUIStyle(GUI.skin.textField);
		myStyle.alignment = TextAnchor.MiddleCenter;
		myStyle.fontSize = 30;
		
		if (TextFieldEnabled) {
			PlayerName = GUI.TextField(new Rect(ScreenWidth / 2, TopVal, ScreenWidth, 80), PlayerName, 25, myStyle);
		}
		
		if (GUI.changed) {
			PlayerPrefs.SetString("PlayerName", PlayerName);
			PlayerPrefs.Save();

			Debug.Log(PlayerName);
		}

	}
}