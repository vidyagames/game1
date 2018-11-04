using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barkscript : MinigameRunner {

    public SpriteRenderer faceRenderer;
    public AudioSource barkSound;

    private Sprite idleface;
    private Sprite barkingface;


	// Use this for initialization
	void Start () {
        this.faceRenderer = GetComponent<SpriteRenderer>();
        this.barkSound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey){
            barkSound.Play();
            print("bark");
        }
	}
}
