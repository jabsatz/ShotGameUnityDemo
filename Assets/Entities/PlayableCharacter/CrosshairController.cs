using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour {

	[SerializeField] private Camera GUICamera;

	void Start() {
		Cursor.visible = false;
	}

	void Update () {
		Vector2 mousePos = GUICamera.ScreenToWorldPoint(Input.mousePosition);
		transform.position = mousePos;
		transform.Rotate(Vector3.back * Time.deltaTime * 45);
	}
}
