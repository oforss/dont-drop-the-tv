﻿using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {
    public float timer = 5f;

    void Update() {
        timer -= Time.deltaTime;
        if(timer <= 0) {
            Destroy(gameObject);
        }
    }
}
