using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barkscript : MinigameRunner {

    public SpriteRenderer faceRenderer;
    public AudioSource barkSound;

    public Sprite idleface;
    public Sprite barkingface;

    public float rattle = .0f;
    public readonly float rattleIncrement = .01f;
    public int rattleCount = 0;

    private int lastFrameRattled;


	// Use this for initialization
	void Start () {
        this.faceRenderer = GetComponent<SpriteRenderer>();
        this.barkSound = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")){  //randomize bark pitch bark, and ramp up the rattlin'
            lastFrameRattled = Time.frameCount;
            barkSound.pitch = 1.2f + GenerateNormalRandom(0f, .05f, -1, 1);
            barkSound.Play();
            faceRenderer.sprite = barkingface;
            this.rattleCount += 1;
            if (rattleCount > 10){
                this.rattle += rattleIncrement;
            }
        }
        if (Input.GetButtonUp("Fire1")){ // close mouth
            faceRenderer.sprite = idleface;
        }
        if (rattleCount > 10){ //START A RATTLIN
            this.doRattle();
        }

        if (Time.frameCount - lastFrameRattled > 120){ //calm down
            this.rattleCount = 0;
        }
    }

    void doRattle()
    {
        this.transform.Translate(new Vector2(
            GenerateNormalRandom(0f, rattle/10, -rattle, rattle),
            GenerateNormalRandom(0f, rattle/10, -rattle, rattle)
            )
        );
    }

    // I KNOW
    private static float GenerateNormalRandom(float mean, float sigma, float min, float max)
    {
        float rand1 = Random.Range(0.0f, 1.0f);
        float rand2 = Random.Range(0.0f, 1.0f);
        float n = Mathf.Sqrt(-2.0f * Mathf.Log(rand1)) * Mathf.Cos((2.0f * Mathf.PI) * rand2);
        float generatedNumber = mean + sigma * n;
        generatedNumber = Mathf.Clamp(generatedNumber, min, max);
        return generatedNumber;
    }

    public override void Won() { }
    public override void Lost() { }

}
