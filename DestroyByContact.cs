using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;
	private int AsteroidStrength = 0;
	private int Counter = 0;

	void Start() {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if(gameController == null) {
			Debug.Log("Cannot Find Game Controller Script");
		}
	}

	void OnTriggerEnter(Collider other) {

		if(other.tag == "Boundary") {
			return;
		}

		Instantiate (explosion,transform.position,transform.rotation);

		if(other.tag == "Player") {
			GameObject.Find("Player").SendMessage("ApplyDamage");
		}

		gameController.AddScore (scoreValue);
		Destroy(gameObject);

	}
}
