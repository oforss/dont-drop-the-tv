using UnityEngine;
using System.Collections;

public class BirdBehaviour : MonoBehaviour {
    public GameObject eggPrefab;
    
    public int direction;
    public float moveSpeed;

    public AudioClip squakSound;

    public bool hasEgg;
    float eggTimer = 0f;

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name == "TV") {
            // Invert log's vertical movement
            Vector2 v = other.attachedRigidbody.velocity;
            v = new Vector2(v.x, -0.5f * v.y);
            other.attachedRigidbody.velocity = v;
            GameController.PlaySound(squakSound, 0.8f);
            GameController.KilledBird();

            Destroy(gameObject);
        }
    }

    void Start() {
        if(direction == 1) {
            // Flip sprite animation
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        if(hasEgg) {
            eggTimer = ((Random.value * 5f) + 2) / moveSpeed;
        }
    }
    
    void Update () {
        if(hasEgg) {
            eggTimer -= Time.deltaTime;
            if(eggTimer <= 0) {
                DropEgg();
            }
        }

        Vector3 pos = transform.position;

        pos.x += direction * moveSpeed * Time.deltaTime;

        transform.position = pos;
    }

    void DropEgg() {
        Instantiate(eggPrefab, transform.position, Quaternion.Euler(0,0,0));
        hasEgg = false;
    }
}
