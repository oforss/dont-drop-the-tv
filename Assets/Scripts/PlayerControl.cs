using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    public float moveSpeed = 4f;
    public float minThrowPower = 150f;
    public float maxThrowPower = 500f;
    public float chargeSpeed = 200f;

    Animator anim;

    public AudioClip huppSound1;
    public AudioClip huppSound2;
    public AudioClip hooSound;

    float offset = 0.3f;

    bool isThrowing = false;
    bool isCarrying = false;
    bool isWalking = false;

    int chargeDir = 1;
    float currentThrowPower = 0f;
    public float CurrentThrowPower {
        get { return currentThrowPower; }
        set {
            currentThrowPower = value;
            powerMeter.value = (float)(currentThrowPower - minThrowPower) / (maxThrowPower - minThrowPower);
        }
    }

    Slider powerMeter;

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name == "TV") {
            // PICK UP TV

            isCarrying = true;

            other.transform.position = transform.position + new Vector3(0, 0.15f, 0);
            other.transform.rotation = Quaternion.Euler(0, 0, 0);
            other.gameObject.transform.parent = transform;

            other.collider.attachedRigidbody.gravityScale = 0;
            other.collider.attachedRigidbody.isKinematic = true;
            other.collider.isTrigger = true;

            CurrentThrowPower = 0;

            GameController.PlaySound(huppSound2);
        }
    }

    void Start() {
        GameController.NewGame();
        anim = transform.Find("Sprite").GetComponent<Animator>();
        powerMeter = GameObject.Find("PowerMeter").GetComponent<Slider>();
    }
    
    void Update () {

        // MOVEMENT

        float input = Input.GetAxisRaw("Horizontal");

        if(input != 0 && !isThrowing) {
            if(!isWalking) {
                isWalking = true;
                anim.SetBool("Moving", true);
            }
            Vector3 pos = transform.position;

            pos.x += input * moveSpeed * Time.deltaTime;

            float screenRatio = (float)Screen.width / Screen.height;
            float widthOrtho = screenRatio * Camera.main.orthographicSize;
            float maxPos = widthOrtho - offset;

            if(pos.x > maxPos)  { pos.x = maxPos; }
            if(pos.x < -maxPos) { pos.x = -maxPos; }

            transform.position = pos;
        }
        
        if(input == 0 && isWalking || isThrowing) {
            isWalking = false;
            anim.SetBool("Moving", false);
        }

        // THROW TV

        if(Input.GetButtonDown("Fire1") && isCarrying && !isThrowing) {
            // Start throwing
            currentThrowPower = minThrowPower;

            isThrowing = true;
            anim.SetBool("Throwing", true);
        }

        if(Input.GetButton("Fire1") && isThrowing) {
            // Charge throw
            if(currentThrowPower >= maxThrowPower || currentThrowPower <= minThrowPower) { chargeDir *= -1; }
            CurrentThrowPower += chargeDir * chargeSpeed * Time.deltaTime;

            if(currentThrowPower > maxThrowPower) { CurrentThrowPower = maxThrowPower; }
            if(currentThrowPower < minThrowPower) { CurrentThrowPower = minThrowPower; }
        }

        if(Input.GetButtonUp("Fire1") && isThrowing) {
            // Release!
            Transform tv = transform.Find("TV");

            powerMeter.enabled = false;

            GameController.PlaySound(huppSound1, 0.6f);

            ((TVBehaviour)tv.GetComponent(typeof(TVBehaviour))).Throw(currentThrowPower);

            isCarrying = false;
            isThrowing = false;

            anim.SetBool("Throwing", false);
        }

    }
}
