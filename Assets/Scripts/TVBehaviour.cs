using UnityEngine;
using System.Collections;

public class TVBehaviour : MonoBehaviour {
    public float gravity = 2f;

    public float maxThrowTorque = 20f;
    public float maxOffshoot = 30f;

    public AudioClip ohNoSound;

    public Sprite brokenTV;

    bool safe = true;

    void OnCollisionEnter2D(Collision2D coll) {
        if(coll.gameObject.name == "Ground" && !safe) {
            safe = true;

            GameController.PlaySound(ohNoSound, 0.7f);
        
            GameController.TakeDamageFrom("TV");
            BrokenTV();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.name == "Player") {
            safe = false;

            transform.GetComponent<Collider2D>().isTrigger = false;
        }
    }

    void BrokenTV() {
        SpriteRenderer rend = GameObject.Find("TVSprite").GetComponent<SpriteRenderer>();
        rend.sprite = brokenTV;
    }

    void Update() {
        float screenRatio = (float)Screen.width / Screen.height;
        float widthOrtho = screenRatio * Camera.main.orthographicSize;
        float maxPos = widthOrtho - 0.15f;
        bool shouldFlip = false;
        Vector3 p = transform.position;

        if(transform.position.x > maxPos)  { 
            shouldFlip = true; 
            p.x = maxPos;  
        }
        if(transform.position.x < -maxPos) { 
            shouldFlip = true; 
            p.x = -maxPos; 
        }

        if(shouldFlip) {
            Vector2 v = transform.GetComponent<Rigidbody2D>().velocity;
            v = new Vector2(-0.5f * v.x, v.y);
            transform.GetComponent<Rigidbody2D>().velocity = v;
            transform.position = p;
        }
    }

    public void Throw(float power) {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
            
        float torque  = (Random.value * 2 * maxThrowTorque) - maxThrowTorque;
        float offshoot = (Random.value * 2 * maxOffshoot) - maxOffshoot;

        rb.isKinematic = false;
        rb.gravityScale = gravity;
        rb.AddForce(new Vector2(offshoot, power));
        rb.AddTorque(torque);
        transform.parent = null;
    }
}
