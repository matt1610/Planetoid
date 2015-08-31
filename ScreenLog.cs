using UnityEngine;
using System.Collections;

namespace Matt {

	public class ScreenLog : MonoBehaviour {

		public IEnumerator LogToScreen(string message) {

			gameObject.guiText.text = message;
			yield return new WaitForSeconds (3);
			gameObject.guiText.text = "";

		}

	}

}