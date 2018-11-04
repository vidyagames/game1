using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrapper : MonoBehaviour {

    [SerializeField]
    private GameObject[] _bootStrappedObjects; 

    void Awake()
    {
        if (LevelManager.Instance == null) {
            foreach (GameObject GO in _bootStrappedObjects) {
                if (transform.Find(GO.name) == null)
                    Instantiate(GO, transform);
            }
        }
        Destroy(this);
    }
}
