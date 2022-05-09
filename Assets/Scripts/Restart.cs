using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour {
    void Update () {
        if(Input.GetKeyDown("r")) {
            Destroy(GameObject.Find("GameCamera"));
            Application.LoadLevel("Game");
        }
    }
}
