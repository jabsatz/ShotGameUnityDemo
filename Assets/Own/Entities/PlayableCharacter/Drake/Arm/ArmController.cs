using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour {
    [SerializeField] private int boostDuration = 30;
    [SerializeField] private int boostMagnitude = 10;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform GunTip;
    [SerializeField] private GameObject Gust;
    [SerializeField] private Transform BoostTip;
    [SerializeField] private Sprite ChargedSprite;
    [SerializeField] private Sprite EmptySprite;
    private bool canAltFire = true;
    private CharacterController2D PlayerController;
    private SpriteRenderer m_SpriteRenderer;
    private bool active = true;

    void Awake() {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        PlayerController = transform.parent.gameObject.GetComponent<CharacterController2D>();
        PlayerController.OnLandEvent.AddListener(RefreshAltFire);
    }

    void Update() {
        active = CharacterController2D.active;
        m_SpriteRenderer.enabled = active;
        m_SpriteRenderer.sprite = canAltFire ? ChargedSprite : EmptySprite;
        if(ShouldFire()) Fire();
        if(ShouldAltFire()) AltFire();
    }

    private bool ShouldFire() {
        return active && Input.GetButtonDown("Fire1");
    }

    private void Fire() {
        Object.Instantiate(Bullet, GunTip.transform.position, GunTip.transform.rotation);
    }

    private bool ShouldAltFire() {
        return active && Input.GetButtonDown("Fire2") && canAltFire;
    }

    private void AltFire() {
        Vector2 boostDirection = (GunTip.transform.position - transform.position).normalized;
        PlayerController.BoostTo(boostDirection, boostDuration, boostMagnitude);
        Object.Instantiate(Gust, BoostTip.transform.position, BoostTip.transform.rotation);
        canAltFire = false;
    }

    public void RefreshAltFire() {
        canAltFire = true;
    }
}
