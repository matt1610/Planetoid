using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	public float speed;
	public float tilt;
	public Boundary boundary;
	public bool Testing;

	public GameObject shot;
	public GameObject greenShot;
    public GameObject[] Weapons;
	public float[] WeaponFireRate;
    public int WeaponNumber = 0;
	public bool forceField = false;
	public GameObject ForceFieldObject;
	public int Health;
	public GameObject playerExplosion;

	public Transform FireSpawn;
	public GameObject FireDamage;

	public bool UseTouchControls = false;
	
	public Transform shotSpawn;
	public float fireRate;
	private float ogFireRate;
	private GameObject PreferencesController;

	private GameObject ForceFieldInstance;
	private float nextFire;
	private int poweredShots = 0;
	public bool greenPower = false;
	public bool forceFieldCreated = false;

	public float deviceHorizontal;
	public float deviceVertical;

	void Start () {
		ogFireRate = fireRate;

		GameObject PreferencesObject = GameObject.Find("PreferencesController");
		Preferences prefControl = PreferencesObject.GetComponent<Preferences>();

		if(prefControl.TouchInputInt == 1) {
			UseTouchControls = true;
		} else {
			UseTouchControls = false;
		}

		if (!UseTouchControls) {
			GameObject.Find("CF-Platformer-3-buttons").SetActive(false);
		}

		deviceHorizontal = Input.acceleration.x;
		deviceVertical = Input.acceleration.y;

//		GameObject.Find ("ScreenLog").SendMessage ("LogToScreen","This Message!");
	}

	void Update() {

		if (UseTouchControls) {
			if (CFInput.GetButton ("Fire2") && Time.time > nextFire) {
				FireEvent ();
			}
		} 

		if (!UseTouchControls) {
			if (CFInput.GetButton ("Fire1") && Time.time > nextFire) {
				FireEvent ();
			}
		} 

		if(Testing) {
			if (Input.GetButton ("Fire1") && Time.time > nextFire) {
				FireEvent ();
			}
		}

		if (forceField == true && forceFieldCreated == false) {
			GameObject ForceFieldInstance = Instantiate(ForceFieldObject, transform.position,shotSpawn.rotation) as GameObject;
			ForceFieldInstance.transform.parent = transform;
			forceFieldCreated = true;
		}

//		Change Weapons
		if (Input.GetKeyDown("space"))
		{
			if (WeaponNumber == Weapons.Length -1) {
				WeaponNumber = 0;
			} else {
				WeaponNumber++;
			}
		}
	}


	void FireEvent() {
		audio.Play();
		nextFire = Time.time + fireRate;
		
		if (greenPower) {
			fireRate = 0.01f;
			Instantiate (Weapons[3],shotSpawn.position,shotSpawn.rotation);
			poweredShots++;
			if (poweredShots > 300) {
				poweredShots = 0;
				greenPower = false;
				fireRate = ogFireRate;
			}
		} else {
			Instantiate(Weapons[WeaponNumber], shotSpawn.position, shotSpawn.rotation);
			fireRate = WeaponFireRate[WeaponNumber];
		}
	}
	
	void FixedUpdate() {

		if (UseTouchControls) {
			float moveHorizontal = CFInput.GetAxis ("Horizontal");
			float moveVertical = CFInput.GetAxis ("Vertical");
			MoveShip(moveHorizontal, moveVertical);
		}

		if (!UseTouchControls) {
			float moveHorizontal = (Input.acceleration.x * 2);
			float moveVertical = (Input.acceleration.y * 2);
			MoveShip(moveHorizontal - deviceHorizontal, moveVertical - deviceVertical);
		}

//		!!! Make sure to disable Keyboard control in the UI when building for mobile!!
		if (Testing) {
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
			MoveShip(moveHorizontal, moveVertical);
		}

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

	void ApplyDamage() {
		Health -= 15;

		GameObject.Find("HealthAmount").guiText.text = Health + "%";

		if (Health < 25) {
			GameObject FireDamageInstance = Instantiate(FireDamage,FireSpawn.position,FireSpawn.rotation) as GameObject;
			FireDamageInstance.transform.parent = FireSpawn.transform;
			GameObject.Find ("HealthAmount").guiText.material.color = Color.red;
		}

		GameObject GameCont = GameObject.Find ("GameController");
		GameController gameController = GameCont.GetComponent<GameController>();

		if (Health < 1) {
			Instantiate (playerExplosion,gameObject.transform.position,gameObject.transform.rotation);
			gameController.GameOver();
			Destroy(gameObject);
		}

	}

	void PortalTime() {
		gameObject.animation.Play ("IntoPortal");
		GameObject.Find ("GameController").SendMessage ("NextLevel");
	}






















}
