using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	private GameController gameController;

	void Start() {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		gameController = gameControllerObject.GetComponent<GameController>();
	}

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player") {

			GameObject thePlayer = GameObject.Find("Player");
			PlayerController playerController = thePlayer.GetComponent<PlayerController>();

			if(gameObject.tag == "GreenPower") {
				playerController.greenPower = true;
			}

			if(gameObject.tag == "ForceFieldPower") {
				playerController.forceField = true;
			}

			if(gameObject.tag == "NewWeapon") {
				if (playerController.WeaponNumber != playerController.Weapons.Length) {
					playerController.WeaponNumber++;
				}

			}

			if (gameObject.tag == "SpeedUp") {
				gameController.UpdateSpeed(0.2f);
			}

			if (gameObject.tag == "SlowMo") {
				Time.timeScale = 0.5f;
				GameObject.Find ("Track").SendMessage ("SlowMo");
			}

			gameController.AsteroidSpeed -= 0.25f;

			GameObject.Destroy(gameObject);

		} // End If
	}// End function
}
