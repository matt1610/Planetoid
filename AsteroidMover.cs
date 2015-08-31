using UnityEngine;
using System.Collections;

public class AsteroidMover : MonoBehaviour {

	private GameController gameController;

	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		gameController = gameControllerObject.GetComponent<GameController>();
		rigidbody.velocity = transform.forward * gameController.AsteroidSpeed;
	}
}
