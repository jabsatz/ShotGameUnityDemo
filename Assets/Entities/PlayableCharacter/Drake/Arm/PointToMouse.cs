using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToMouse : MonoBehaviour {
    private SpriteRenderer m_SpriteRenderer;

    void Awake () {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
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
}
