using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour {
    [SerializeField] private float m_JumpForce = 400f;
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    [SerializeField] private bool m_AirControl = false;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private Transform m_CeilingCheck;
    [SerializeField] private Collider2D m_CrouchDisableCollider;
    [SerializeField] private Healthbar m_Healthbar;

    private Vector2 boostingDirection;
    private int boostingTimeLeft = 0;
    const float k_GroundedRadius = .2f;
    private bool m_Grounded;
    const float k_CeilingRadius = .2f;
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;
    private Vector3 m_Velocity = Vector3.zero;
    private Animator m_Animator;
    private SpriteRenderer m_SpriteRenderer;
    private float initialGravity;
    private GameObject m_Arm;
    private ArmController m_ArmController;
    public static bool active = true;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false;

    private void Awake() {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_ArmController = gameObject.GetComponentInChildren<ArmController>();

        if(OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if(OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();

        initialGravity = m_Rigidbody2D.gravityScale;
    }

    void Start() {
        if(GameMaster.lastCheckpointPos != new Vector2()) transform.position = GameMaster.lastCheckpointPos;
    }


    private void Update() {
        active = !m_Animator.GetCurrentAnimatorStateInfo(0).IsTag("Frozen");
    }

    private void FixedUpdate() {
        HandleGrounding();
    }

    public void Move(float move, bool crouch, bool jump, bool locked) {
        if(!crouch && HasPlatformOverHead()) crouch = true;

        if(Boosting()) {
            Boost();
        }

        if(locked) {
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(
                m_Rigidbody2D.velocity,
                new Vector2(0, m_Rigidbody2D.velocity.y),
                ref m_Velocity,
                m_MovementSmoothing
            );
        }

        if(CanMove(locked)) {
            if(m_CrouchDisableCollider != null)
                m_CrouchDisableCollider.enabled = crouch;
            if(crouch != m_wasCrouching) {
                m_wasCrouching = crouch;
                OnCrouchEvent.Invoke(crouch);
            }

            if(crouch) move *= m_CrouchSpeed;

            Vector3 targetVelocity = new Vector2(
                move * 10f,
                m_Rigidbody2D.velocity.y
            );

            m_Rigidbody2D.velocity = Vector3.SmoothDamp(
                m_Rigidbody2D.velocity,
                targetVelocity,
                ref m_Velocity,
                m_MovementSmoothing
            );

        }
        if(ShouldFlip(move)) Flip();

        if(ShouldJump(jump)) Jump();

        SetAnimatorParameters();
    }

    private bool ShouldFlip(float move) {
        return active && move != 0 && m_FacingRight != move > 0;
    }

    private bool CanMove(bool locked) {
        return active && !locked && (m_Grounded || m_AirControl);
    }

    private bool ShouldJump(bool jump) {
        return m_Grounded && jump;
    }

    private void Jump() {
        m_Grounded = false;
        m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
    }

    private void SetAnimatorParameters() {
        float move = m_Rigidbody2D.velocity.x;
        if(!m_FacingRight) move = -move;
        m_Animator.SetInteger(
            "Direction",
            move >= 0.1 ? 1 : move <= -0.1 ? -1 : 0
        );
        m_Animator.SetBool("Grounded", m_Grounded);
        m_Animator.SetFloat("VSpeed", m_Rigidbody2D.velocity.y);
        m_Animator.SetFloat("HSpeed", Mathf.Abs(m_Rigidbody2D.velocity.x));
    }

    private bool HasPlatformOverHead() {
        return Physics2D.OverlapCircle(
            m_CeilingCheck.position,
            k_CeilingRadius,
            m_WhatIsGround);
    }

    private void Flip() {
        m_FacingRight = !m_FacingRight;
        m_SpriteRenderer.flipX = !m_SpriteRenderer.flipX;
    }

    private void HandleGrounding() {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(
            m_GroundCheck.position,
            k_GroundedRadius,
            m_WhatIsGround
        );

        for(int i = 0; i < colliders.Length; i++) {
            if(colliders[i].gameObject != gameObject) {
                m_Grounded = true;
                if(!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }

    private bool Boosting() {
        return boostingTimeLeft > 0;
    }

    private void Boost() {
        m_Rigidbody2D.velocity = boostingDirection*boostingTimeLeft*0.1f;
        boostingTimeLeft--;
        if(boostingTimeLeft<10) m_AirControl = true;
        if(!Boosting()) StopBoosting();
    }

    private void StopBoosting() {
        if(m_Grounded && m_ArmController) m_ArmController.RefreshAltFire();
        m_Rigidbody2D.gravityScale = initialGravity;
    }

    public void BoostTo(object[] args) {
        BoostTo((Vector2) args[0], (int) args[1], (float) args[2]);
    }

    private void BoostTo(Vector2 where, int duration, float magnitude) {
        m_AirControl = false;
        boostingDirection = where*magnitude;
        boostingTimeLeft = duration;
        m_Rigidbody2D.gravityScale = 0;
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "Hazard") {
            Electrocute();
        }
    }

    private void Electrocute() {
        m_Healthbar.SetHealth(0);
        m_Animator.SetTrigger("Electrocuted");
        m_Rigidbody2D.velocity = new Vector2(0, 0);
        m_Rigidbody2D.gravityScale = 0;
    }

    public void Destroy() {
        m_Animator.SetTrigger("Death");
    }
}