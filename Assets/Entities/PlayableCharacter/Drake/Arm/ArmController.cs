using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour {
    [SerializeField] private GameObject Bullet;
    private SpriteRenderer m_SpriteRenderer;

    void Awake() {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        PointToTarget();
        if(ShouldFire()) {
            Fire();
        }
    }

    private void PointToTarget() {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 armPosition = new Vector2(
            transform.position.x,
            transform.position.y
        );
        Vector2 armToTarget = armPosition - mousePosition;
        float angle = Vector2.SignedAngle(Vector2.left, armToTarget);

	    m_SpriteRenderer.flipY = ShouldFlip(angle);

        Quaternion quat = Quaternion.identity;
        quat.eulerAngles = new Vector3(0, 0, angle);
        transform.rotation = quat;
    }

    private bool ShouldFlip(float angle) {
        return angle > 90 || angle < -90;
    }

    private bool ShouldFire() {
        return Input.GetButtonDown("Fire1");
    }

    private void Fire() {
        Object.Instantiate(Bullet, transform.position, transform.rotation);
    }
}
