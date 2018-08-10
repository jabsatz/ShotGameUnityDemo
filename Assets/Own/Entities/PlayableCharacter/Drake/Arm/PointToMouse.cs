using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToMouse : MonoBehaviour {
    private SpriteRenderer m_SpriteRenderer;

    void Awake () {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        Vector2 armRotation = new Vector2(
            -Input.GetAxisRaw("Horizontal"),
            -Input.GetAxisRaw("Vertical")
        );

        if(armRotation != Vector2.zero) {
            float angle = Vector2.SignedAngle(Vector2.left, armRotation);

            m_SpriteRenderer.flipY = ShouldFlip(angle);

            Quaternion quat = Quaternion.identity;
            quat.eulerAngles = new Vector3(0, 0, angle);
            transform.rotation = quat;
        }
    }

    private bool ShouldFlip(float angle) {
        return angle > 90 || angle < -90;
    }
}
