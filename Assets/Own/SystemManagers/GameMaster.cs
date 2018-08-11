using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

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

	public void EndGameIn(int cycles) {
		StartCoroutine(EndGameLoop(cycles));
	}

	IEnumerator EndGameLoop(int cycles) {
		for(int i = 0; i <= cycles; i++) {
			yield return null;
		}
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
	}
}
