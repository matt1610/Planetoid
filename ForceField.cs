using UnityEngine;
using System.Collections;

public class ForceField : MonoBehaviour {

	private int Counter = 0;
	public int MaxHits;

	void OnTriggerEnter(Collider other) {
		
		if(other.tag == "Asteroid") {
			Counter++;
			if (Counter > MaxHits) {
				Destroy(gameObject);
				GameObject thePlayer = GameObject.Find("Player");
				PlayerController playerController = thePlayer.GetComponent<PlayerController>();
				playerController.forceField = false;
				playerController.forceFieldCreated = false;
			}
		}
		
		
	}
}
