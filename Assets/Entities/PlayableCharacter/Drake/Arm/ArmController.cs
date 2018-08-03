using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour {
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform GunTip;
    [SerializeField] private GameObject Gust;
    [SerializeField] private Transform BoostTip;
    private bool canAltFire = true;
    private CharacterController2D PlayerController;

    void Awake() {
        PlayerController = transform.parent.gameObject.GetComponent<CharacterController2D>();
    }

    void Update() {
        if(ShouldFire()) Fire();
        if(ShouldAltFire()) AltFire();
    }

    private bool ShouldFire() {
        return Input.GetButtonDown("Fire1");
    }

    private void Fire() {
        Object.Instantiate(Bullet, GunTip.transform.position, GunTip.transform.rotation);
    }

    private bool ShouldAltFire() {
        return Input.GetButtonDown("Fire2");
    }

    private void AltFire() {
        Vector2 boostDirection = (GunTip.transform.position - transform.position).normalized;
        PlayerController.BoostTo(boostDirection, 30, 10);
        Object.Instantiate(Gust, BoostTip.transform.position, BoostTip.transform.rotation);
        canAltFire = false;
    }

    public void RefreshAltFire() {
        canAltFire = true;
    }
}
