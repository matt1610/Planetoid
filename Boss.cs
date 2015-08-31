using UnityEngine;
using System.Collections;

[System.Serializable]
public class BossBoundary {
	public float xMin, xMax, zMin, zMax;
}

public class Boss : MonoBehaviour {

	public BossBoundary boundary;
	public float tilt;
	public float speed;
	private float xMove = 1;
	private float yMove = 1;
	private bool Down = true;
	public GameObject explosion;
	public int BossLife = 150;
	public GameObject BossExplosion;

	void Start () {
		
	}

	void Update () {

		if(Down == true) {
			xMove -= 0.01f;
		}

		if(xMove < -0.95f) {
			Down = false;
			Debug.Log (Down);
		}

		if(xMove > 0.95f) {
			Down = true;
		}

		if(Down == false) {
			xMove += 0.01f;
		}

	}

	void FixedUpdate() {

		float moveHorizontal = xMove;
		float moveVertical = yMove;
		MoveShip(moveHorizontal, moveVertical);
		
		rigidbody.position = new Vector3 (
			
			Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
			
			);
		
		rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -tilt);
	}

	void MoveShip(float moveHorizontal, float moveVertical) {
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rigidbody.velocity = movement * speed;
	}

	void OnTriggerEnter(Collider other) {
		
		if(other.tag == "PlayerProjectile") {
			Instantiate (explosion,transform.position,transform.rotation);
			
			BossLife -= 1;
			
			if(BossLife < 5) {
				Destroy(gameObject);
				Instantiate (BossExplosion,transform.position,transform.rotation);
//				GameObject.Find("GameController").SendMessage("SetLightSpeed");
				GameObject.Find ("GameController").SendMessage ("NextLevel");
			}

			Destroy(other.gameObject);

		}

		
	}






















}
