using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public GameObject Audio;
    public GameObject ZoneSwitcher;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SwitchScene(int scene)
    {
        SceneManager.LoadScene(scene);
        ZoneSwitcher.GetComponent<ZoneScript>().Announce(scene);
    }
    public void Initiate()
    {
        StartCoroutine(InitGame());
    }
    IEnumerator InitGame()
    {
        SwitchScene(2);
        yield return new WaitForSeconds(0.1f);
        SwitchScene(3);
        Audio.GetComponent<MusicScript>().InitGameMusic();
    }
}
