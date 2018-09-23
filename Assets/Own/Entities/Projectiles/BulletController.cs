using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    public bool enemyBullet = false;
    public float speed = 0.4f;
    public int damage = 10;
    public int timeToChange = 10;
    public Sprite changeSprite;
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
        if(col.isTrigger) {
            return;
        }
        if(col.gameObject.tag == (enemyBullet ? "Player" : "Enemy")) {
            col.gameObject.SendMessage("takeDamage", damage);
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
        if(viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1) {
            UnityEngine.Object.Destroy(gameObject);
        }
    }
}
