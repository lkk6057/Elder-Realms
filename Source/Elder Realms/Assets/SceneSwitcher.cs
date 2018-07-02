using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwitcher : MonoBehaviour {
    public GameObject loader;
    public GameObject audio;
	// Use this for initialization
	void Start () {
        loader = GameObject.FindGameObjectWithTag("Loader");
        audio = GameObject.FindGameObjectWithTag("Audio");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Switch()
    {
        audio.GetComponent<AudioSource>().Stop();
        loader.GetComponent<SceneLoader>().Initiate();

    }
}
