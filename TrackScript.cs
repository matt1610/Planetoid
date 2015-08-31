using UnityEngine;
using System.Collections;

public class TrackScript : MonoBehaviour {

	public float trackSpeed;
	private bool TrackSlow = false;
	private bool SlowMotion = false;
	private bool TrackPause = false;

	void Update() {
		if(!TrackSlow && !TrackPause) {
			transform.position = new Vector3 (transform.position.x,transform.position.y,transform.position.z-trackSpeed);
		}
		if(SlowMotion && !TrackPause) {
			transform.position = new Vector3 (transform.position.x,transform.position.y,(transform.position.z-trackSpeed)/10);
		}
	}

	void PauseTrack() {
		if (!TrackPause) {
			TrackPause = true;
		} else {
			TrackPause = false;
		}
	}

	void SlowMo() {
		TrackSlow = true;
		if(SlowMotion) {
			SlowMotion = false;
		} else {
			SlowMotion = true;
		}
	}
}
