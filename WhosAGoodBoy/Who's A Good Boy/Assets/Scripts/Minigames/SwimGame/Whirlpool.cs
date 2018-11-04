using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlpool : MonoBehaviour {

    public PlayerController controller;
    [SerializeField] ParticleSystem particles;
    [SerializeField] float lifeTime;
    private Vector2 _movementVector;
    private bool _playing;


    private void OnTriggerStay2D(Collider2D collision)
    {
        controller.Movement.Speed = 1;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        controller.Movement.Speed = CharacterMovement.DEFAULT_SPEED;
    }

    public void BeginLifecycle()
    {
        _movementVector = new Vector2(Random.Range(-.15f, .15f), Random.Range(-.15f, .15f));
        StartCoroutine(Lifecycle());
    }

    private void Update()
    {
        if (!_playing)
            return;
        transform.Translate(_movementVector * Time.deltaTime);
    }

    private IEnumerator Lifecycle()
    {

        particles.Play();
        _playing = true;
        yield return new WaitForSeconds(lifeTime + lifeTime * Random.Range(-.2f, .2f));
        Despawn();
    }

    public void Despawn()
    {
        StartCoroutine(Despawn_Timed(2f));
    }

    private IEnumerator Despawn_Timed(float despawnTime)
    {
        particles.Stop();
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}
