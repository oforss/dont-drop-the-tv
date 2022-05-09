using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverText : MonoBehaviour {
    Text goText;
    Text scoreText;

    void Start () {
        goText = GetComponent<Text>();
        scoreText = GameObject.Find("FinalScore").GetComponent<Text>();

        goText.text = GameController.gameOverString + "\n\n\n<size=25>'R' TO PLAY AGAIN</size>";
        scoreText.text = "<size=15>YOUR FINAL SCORE WAS</size>\n" + GameController.score;
    }
}
