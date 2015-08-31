using UnityEngine;
using System.Collections;

public class ScaleUI : MonoBehaviour {

	private float Amountx;
	private float Amounty;
	private float Amountw;
	private float Amounth;
	private int ScreenWidth;
	private int ScreenHeight;

	void Start() {
		
		ScreenWidth = Screen.width;
		ScreenHeight = Screen.height;
		
		guiTexture.pixelInset = new Rect(
			-(ScreenWidth/2), 
			-(ScreenHeight/2), 
			ScreenWidth * 3, 
			ScreenHeight
			);
	}
	
	void MoveLeft() {

		Amountx = gameObject.guiTexture.pixelInset.x - ScreenWidth;
		Amounty = gameObject.guiTexture.pixelInset.y;
		Amountw = gameObject.guiTexture.pixelInset.width;
		Amounth = gameObject.guiTexture.pixelInset.height;

		gameObject.guiTexture.pixelInset = new Rect (
			Amountx,Amounty,Amountw,Amounth
			);
	}
}
