using UnityEngine;
using System.Collections;

public class GuiControl : MonoBehaviour {

    
    void OnMouseDown() {

		GameObject thePlayer = GameObject.Find("Player");
		PlayerController playerController = thePlayer.GetComponent<PlayerController>();

        if (gameObject.name == "UpArrow") 
        {
            playerController.WeaponNumber++;
        }

        if (gameObject.name == "DownArrow")
        {
            playerController.WeaponNumber--;

        }
    }
}
