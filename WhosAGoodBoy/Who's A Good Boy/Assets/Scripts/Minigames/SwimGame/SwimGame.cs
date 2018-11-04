using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SwimGame : MinigameRunner {

    public GameObject WhirlpoolPrefab;
    public BoxCollider2D WhirlpoolGenerationBoundsBox;
    public float BaseWhirlpoolSpawnTime = 1f;
    public float BaseStamina = 10f;
    public Slider StaminaSlider;
    public Interactable Goal;
    private float _difficultyAdjustedWhirlpoolSpawnTime;
    private float _maxStamina;
    private float _stamina;


    private float _timeSinceLastWhirlpool;

    private void OnEnable()
    {
        Goal.GainedFocus += OnTouchedExit;
    }
    private void OnDisable()
    {
        Goal.GainedFocus -= OnTouchedExit;
    }

    private void OnTouchedExit(object sender, System.EventArgs args)
    {
        Passed();
    }

    // Use this for initialization
    void Start () {
        _difficultyAdjustedWhirlpoolSpawnTime = BaseWhirlpoolSpawnTime - BaseWhirlpoolSpawnTime * LevelManager.Instance.MinigameDifficulty / 15;
        _stamina = _maxStamina = BaseStamina - BaseStamina * LevelManager.Instance.MinigameDifficulty / 20;
        SpawnPools(LevelManager.Instance.MinigameDifficulty);
	}
	
	// Update is called once per frame
	void Update () {
        _timeSinceLastWhirlpool += Time.deltaTime;
        if (_timeSinceLastWhirlpool > _difficultyAdjustedWhirlpoolSpawnTime) {
            _timeSinceLastWhirlpool = 0f;
            SpawnPools(1);
        }
        _stamina -= Time.deltaTime;
        StaminaSlider.value = Mathf.InverseLerp(0, _maxStamina, _stamina);

        if (_stamina <= 0f)
            Failed();
	}


    private void SpawnPools(int numberOfPools)
    {
        for (int i = 0; i < numberOfPools; i++) {
            var nextX = Random.Range(WhirlpoolGenerationBoundsBox.bounds.min.x, WhirlpoolGenerationBoundsBox.bounds.max.x);
            var nextY = Random.Range(WhirlpoolGenerationBoundsBox.bounds.min.y, WhirlpoolGenerationBoundsBox.bounds.max.y);

            var newPool = Instantiate(WhirlpoolPrefab, new Vector3(nextX, nextY), Quaternion.identity).GetComponent<Whirlpool>();
            newPool.BeginLifecycle();
        }
    }
}
