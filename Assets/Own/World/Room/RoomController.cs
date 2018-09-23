using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour {
	[SerializeField] GameObject[] gates;
	List<GameObject> enemies = new List<GameObject>();
	bool roomStarted = false;
	
	// Update is called once per frame
	void Update () {
		Debug.Log(enemies.Count);
		for(int i = 0; i < enemies.Count; i++) {
			if(enemies[i] == null) enemies.Remove(enemies[i]);
		}
		if(enemies.Count == 0 && roomStarted) EndRoom(); 
	}

	public void StartRoom() {
		if(!roomStarted) {
			roomStarted = true;
			BroadcastMessage("Lock");
			BroadcastMessage("Spawn");
		}
	}

	public void EndRoom() {
		BroadcastMessage("Unlock");
	}
	
	private void RegisterEnemy(GameObject enemy) {
		enemies.Add(enemy);
	}
}
