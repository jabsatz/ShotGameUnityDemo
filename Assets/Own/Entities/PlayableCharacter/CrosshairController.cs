using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour {

	[SerializeField] private Camera GUICamera;
	private SpriteRenderer m_SpriteRenderer;

	void Start() {
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
		Cursor.visible = false;
	}

	void Update () {
        m_SpriteRenderer.enabled = CharacterController2D.active;
		Vector2 mousePos = GUICamera.ScreenToWorldPoint(Input.mousePosition);
		transform.position = mousePos;
		transform.Rotate(Vector3.back * Time.deltaTime * 45);
	}
}
