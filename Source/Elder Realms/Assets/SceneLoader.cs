using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public GameObject Audio;
    public MusicScript Music;
    public GameObject ZoneSwitcher;
    public int currscene;
    public AudioClip[] musics;
    public SaveManager savemanager;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        Music = Audio.GetComponent<MusicScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SwitchScene(int scene)
    {
        SceneManager.LoadScene(scene);
        ZoneSwitch(currscene,scene);
        currscene = scene;
        ZoneSwitcher.GetComponent<ZoneScript>().Announce(scene);
    }
    public void ZoneSwitch(int currscene, int targetscene)
    {
        if (currscene==7&&targetscene==8)
        {
            Music.villageon = false;
            Music.PlayLoop(musics[0]);
        }
        if (currscene == 8 && targetscene == 7)
        {
            Music.PlayLoop(Music.village);
        }
        if (currscene == 10 && targetscene == 8)
        {
            Music.PlayLoop(musics[0]);
        }
    }
    public void Initiate()
    {
        StartCoroutine(InitGame());
    }
    IEnumerator InitGame()
    {
        SwitchScene(2);
        yield return new WaitForSeconds(0.1f);
        if (savemanager.GetSave())
        {
            SwitchScene(3);
        }
        else
        {
            SwitchScene(12);
        }
        Audio.GetComponent<MusicScript>().InitGameMusic();
        savemanager.Load();
    }
}
