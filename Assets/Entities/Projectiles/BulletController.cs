using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    [SerializeField] private float speed = 0.4f;
    [SerializeField] private int damage = 10;
    [SerializeField] private int timeToChange = 10;
    [SerializeField] private Sprite changeSprite;
    private SpriteRenderer m_spriteRenderer;

    void Awake() {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if(timeToChange > 0) {
            timeToChange--;
        } else {
            m_spriteRenderer.sprite = changeSprite;
        }
        float direction = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
        transform.position = transform.position + new Vector3(
			Mathf.Cos(direction) * speed,
			Mathf.Sin(direction) * speed,
			0
		);
        DestroyIfOutOfView();
    }

    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log(col);
        if(col.gameObject.tag == "Enemy") {
            //Enemy hp calculation
        }
        if(col.gameObject.tag != "Player") {
            Object.Destroy(gameObject);
        }
    }

    void DestroyIfOutOfView() {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        if(viewPos.x < -1 || viewPos.x > 2 || viewPos.y < -1 || viewPos.y > 2) {
            Object.Destroy(gameObject);
        }
    }
}
