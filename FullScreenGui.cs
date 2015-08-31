using UnityEngine;
using System.Collections;

public class FullScreenGui : MonoBehaviour {

	private int ScreenWidth;
	private int ScreenHeight;
	
	void Start() {
		
		ScreenWidth = Screen.width;
		ScreenHeight = Screen.height;
		
		guiTexture.pixelInset = new Rect(
			-(ScreenWidth/2), 
			-(ScreenHeight/2), 
			ScreenWidth, 
			ScreenHeight
			);
	}
}
