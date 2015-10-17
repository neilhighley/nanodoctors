using UnityEngine;
using System.Collections;
using System;

public class PlayerNewScript : MonoBehaviour {
    private int KeyStage = 0;

    public GameObject LungSign1;
    public GameObject LungSign2;

    public GameObject WelcomeText;
    public GameObject DocText;
    public float MaxAudio = 1;
    public AudioContainer AudioContainer;

	// Use this for initialization
	void Start () {
        WelcomeText.SetActive(false);
        DocText.SetActive(false);
        
    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
       // Debug.Log("Trigger enter:"+other.name+"/"+KeyStage);
        
        if (other.name == "Trigger1")
        {
            if (KeyStage == 0)
            {
                LungSign1.SetActive(true);
                KeyStage = 1;
            }
           
        }
        if (other.name == "TriggerBase")
        {
            if (KeyStage == 1)
            {
                DocText.SetActive(true);
                LungSign1.SetActive(false);
                LungSign2.SetActive(true);
                KeyStage = 2;
            }
            if (KeyStage == 0)
            {
                WelcomeText.SetActive(true);

            }
        }
        if (other.name == "Room1Audio")
        {
            ChangeVolume("Room1Audio", MaxAudio);
            ChangeVolume("Room2Audio", 0f);
            ChangeVolume("Room3Audio", 0f);

        }
        if (other.name == "Room2Audio")
        {
            ChangeVolume("Room2Audio", MaxAudio);
            ChangeVolume("Room1Audio", 0f);
            ChangeVolume("Room3Audio", 0f);
        }
        if (other.name == "Room3Audio")
        {
            ChangeVolume("Room2Audio", 0f);
            ChangeVolume("Room1Audio", 0f);
            ChangeVolume("Room3Audio", MaxAudio);
        }
    }

    private void ChangeVolume(string v,float vol)
    {
        var snds = AudioContainer.GetComponentsInChildren<AudioContainer>();
        //Debug.Log("number of sounds found:" + snds.Length);
        foreach (var sndObj in snds)
        {
            Debug.Log("SNDOBJ:" + sndObj.name+":"+v);
            if (sndObj.name == v)
            {
                Debug.Log("Found audio :" + v);
                var audioSrc = sndObj.GetComponent<AudioSource>();
                audioSrc.volume = vol;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {

        //Debug.Log("Trigger exit:" + other.name + "/" + KeyStage);
        if (other.name == "TriggerBase")
        {
            if (KeyStage == 2)
            {
                DocText.SetActive(false);
               
            }
            if (KeyStage == 0)
            {
                WelcomeText.SetActive(false);

            }

        }
        if (other.name == "Room1Audio")
        {
            ChangeVolume("Room1Audio", MaxAudio / 10);

        }
        if (other.name == "Room2Audio")
        {
            ChangeVolume("Room2Audio", MaxAudio / 10);
        }
        if (other.name == "Room3Audio")
        {
            ChangeVolume("Room3Audio", MaxAudio / 10);
        }
    }
 }
