using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyTarget : MonoBehaviour {

	private bool isMouse = false;

	void OnGUI() {
		Event e = Event.current;
		if (e.type == EventType.MouseDown) {
			if (e.button == 0 && e.isMouse)
				isMouse = true;
		}
		if (e.type == EventType.MouseUp) {
			if (e.button == 0 && e.isMouse)
				isMouse = false;
		}
	}
	void Update() {
		if (isMouse) {
			var v3 = Input.mousePosition;
			v3.z = 10.0f;
			v3 = Camera.main.ScreenToWorldPoint(v3);
			//Debug.Log("Mouse Pos: " + v3.ToString());
			v3.z = 0;
			
			this.gameObject.transform.position = v3;
		}
	}
}
