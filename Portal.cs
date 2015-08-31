using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			other.SendMessage("PortalTime");
		}
	}
}
