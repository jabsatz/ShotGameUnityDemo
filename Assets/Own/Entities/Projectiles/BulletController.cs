using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    [SerializeField] private bool enemyBullet = false;
    [SerializeField] private float speed = 0.4f;
    [SerializeField] private int damage = 10;
    [SerializeField] private int timeToChange = 10;
    [SerializeField] private Sprite changeSprite;
    private string[] destroyingTags = new string[2];
    private SpriteRenderer m_spriteRenderer;

    void Awake() {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        destroyingTags[0] = "Platform";
        destroyingTags[1] = enemyBullet ? "Player" : "Enemy";
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
        if(col.gameObject.tag == (enemyBullet ? "Player" : "Enemy")) {
            //Target hp calculation
        }
        if(BulletShouldGetDestroyed(col.gameObject.tag)) {
            UnityEngine.Object.Destroy(gameObject);
        }
    }

    private bool BulletShouldGetDestroyed(string collisionTag) {
        return Array.Exists<string>(destroyingTags, tag => tag == collisionTag);
    }

    void DestroyIfOutOfView() {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        if(viewPos.x < -1 || viewPos.x > 2 || viewPos.y < -1 || viewPos.y > 2) {
            UnityEngine.Object.Destroy(gameObject);
        }
    }
}
