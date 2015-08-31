using UnityEngine;
using System.Collections;
using UnityEngine;
using System.Collections;

public class Preferences : MonoBehaviour {
	
	public int TouchInputInt;
	public int AccelerometerInt;
	public int KeyBoardControlInt;
	
	void Start() {
		if (PlayerPrefs.HasKey("TouchInputInt")) {
			TouchInputInt = PlayerPrefs.GetInt ("TouchInputInt");
			AccelerometerInt = PlayerPrefs.GetInt ("AccelerometerInt");
		} else {
			TouchInputInt = 0;
			AccelerometerInt = 1;
		}
		MoveX ();
	}

	void MoveX() {
		if(TouchInputInt == 0) {
			GameObject.Find("Tick").animation.Play("MoveTickDown");
		} else {
			GameObject.Find("Tick").animation.Play("MoveTickUp");
		}
	}
	
	void OnMouseDown() {
		
		if(gameObject.name == "UseTouchInput") {
			TouchInputInt = 1;
			AccelerometerInt = 0;
		}
		
		if(gameObject.name == "UseAccelerometer") {
			TouchInputInt = 0;
			AccelerometerInt = 1;
		}

		MoveX ();

		PlayerPrefs.SetInt("TouchInputInt", TouchInputInt);
		PlayerPrefs.SetInt("AccelerometerInt", AccelerometerInt);
		PlayerPrefs.SetInt("KeyBoardControlInt", KeyBoardControlInt);
		PlayerPrefs.Save();
		
	}
}