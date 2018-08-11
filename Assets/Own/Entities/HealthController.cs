using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {

	public int health = 100;
	public Healthbar healthbar;

	public void takeDamage(int damage) {
		health -= damage;
		if(healthbar) healthbar.SetHealth(health);
		if(health <= 0) gameObject.SendMessage("Destroy");
		gameObject.BroadcastMessage("DamageFlash");
	}
}
