using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	private static GameMaster instance;
	public static Vector2 lastCheckpointPos;

	void Awake() {
		if(instance == null) {
			instance = this;
			DontDestroyOnLoad(instance);
		} else {
			Destroy(gameObject);
		}
	}
}
