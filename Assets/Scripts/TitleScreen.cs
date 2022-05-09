using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {
    float newColorTime = 7f;
    float currentTime = 0f;

    Color currentColor;

    void Start () {
        currentColor = RandomMutedColor();
        Camera.main.backgroundColor = currentColor;
        currentColor = RandomMutedColor();
    }

    void Update () {
        currentTime += Time.deltaTime;

        if(currentTime >= newColorTime) {
            currentColor = RandomMutedColor();
            currentTime -= newColorTime;
        }

        Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor,
                                                 currentColor,
                                                 Time.deltaTime/newColorTime);

        if(Input.GetButtonDown("Fire1")) {
            PlayButton();
        }
    }

    Color RandomMutedColor() {
        return new Color((Random.value * 0.5f) + 0.1f,
                         (Random.value * 0.5f) + 0.1f,
                         (Random.value * 0.5f) + 0.1f);
    }

    public void PlayButton() {
        Application.LoadLevel("Game");
    }
}
