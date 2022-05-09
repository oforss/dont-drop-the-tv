using UnityEngine;
using System.Collections;

public class Egg : MonoBehaviour {
    public GameObject eggSplat;
    
    public AudioClip crackSound;
    public AudioClip arghSound;

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name == "TV") {
            Splat(other.gameObject, new Vector3(0, 0.15f, 0));
        }
        if(other.gameObject.name == "Ground") {
            Splat();
        }
        if(other.gameObject.name == "Player") {
            GameController.TakeDamageFrom("Egg");
            GameController.PlaySound(arghSound);
            Splat(other.gameObject, new Vector3(0, 0.45f, 0));
        }
    }
    
    void Splat() {
        Instantiate(eggSplat, transform.position, Quaternion.Euler(0, 0, 0));
        GameController.PlaySound(crackSound);
        Destroy(gameObject);
    }

    void Splat(GameObject parent, Vector3 newPosition) {
        Quaternion rot = parent.transform.rotation;
        newPosition += parent.transform.position;
        
        GameController.PlaySound(crackSound);
        
        GameObject s = (GameObject)Instantiate(eggSplat, newPosition, rot);
        s.transform.parent = parent.transform;
        Destroy(gameObject);
    }
}   
