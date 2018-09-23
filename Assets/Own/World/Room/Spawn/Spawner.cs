using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	[SerializeField] GameObject Enemy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Spawn () {
		GameObject EnemyInstance = Instantiate(Enemy, transform.position, transform.rotation);
		SendMessageUpwards("RegisterEnemy", EnemyInstance);
	}
}
