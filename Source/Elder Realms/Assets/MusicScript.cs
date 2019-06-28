using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MusicScript : MonoBehaviour {
    public AudioClip music;
    public AudioClip village;
    public AudioSource source;
    public GameObject Loader;
    public bool menu = true;
    public bool game = true;
    public bool villageon;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        Loader.GetComponent<SceneLoader>().SwitchScene(1);
        StartCoroutine(playmusic());
        villageon = true;
    }
	
	// Update is called once per frame
	void Update () {
        
	}
    IEnumerator playmusic()
    {
        source.clip = music;
        source.Play();
        yield return new WaitForSeconds(music.length);
        if (menu)
        {
            StartCoroutine(playmusic());
        }
        
    }
    public void InitGameMusic()
    {
        StartCoroutine(gamemusic());
        menu = false;
        game = true;
        villageon = false;
    }
    public void RestartVillage()
    {
        StartCoroutine(gamemusic());
    }
    IEnumerator gamemusic()
    {
        
        source.clip = village;
        source.Play();
        yield return new WaitForSeconds(village.length);
        if (game&&villageon)
        {
            StartCoroutine(gamemusic());
        }
    }
    public void PlayLoop(AudioClip music)
    {
        StopCoroutine("MusicLoop");
        StartCoroutine(MusicLoop(music));
    }
    IEnumerator MusicLoop(AudioClip music)
    {
        source.Stop();
        source.clip = music;
        source.Play();
        yield return new WaitForSeconds(source.clip.length);
        if (game&&source.clip == music)
        {
            StartCoroutine(MusicLoop(music));
        }
    }
}
