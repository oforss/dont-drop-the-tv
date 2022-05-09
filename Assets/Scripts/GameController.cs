using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public static class GameController {
    public static int startingLives = 3;
    public static int startingLevel = 0;

    public static long score = 0;
    static int level = 0;
    static int lives;

    static float soundVolMax = 1.0f;
    static float soundVolMin = 0.5f;

    static AudioSource audioSource;

    static int killedBirds = 0;
    static int spawnedBirds = 0;

    static int toNextLevel = 10;

    public static string gameOverString = "";

    public static int Level {
        get {
            return level;
        }
        set {
            level = value;
            GameObject.Find("LevelNumber").GetComponent<Text>().text = level.ToString();
        }
    }

    public static int Lives {
        get { return lives; }
        set {
            lives = value;
            GameObject.Find("LivesNumber").GetComponent<Text>().text = lives.ToString();
        }
    }

    public static void NewGame() {
        Level = startingLevel;
        Lives = startingLives;

        if(audioSource == null){ Object.Destroy(audioSource); }
        audioSource = Camera.main.GetComponent<AudioSource>();
        Object.DontDestroyOnLoad(audioSource);
        score = 0;
    }

    public static void KilledBird() {
        killedBirds += 1;

        score += 100 + level * 10 + level * level;
        GameObject.Find("Score").GetComponent<Text>().text = "<size=10>SCORE</size>\n"+score;

        CheckLevel();
    }

    public static void SpawnedBird() {
        spawnedBirds += 1;
        CheckLevel();
    }

    static void CheckLevel() {
        toNextLevel -= 1;
        if(toNextLevel <= 0) { 
            Level += 1;
            toNextLevel = 10 + Level;
        }
    }

    public static void TakeDamageFrom(string thing) {
        Lives -= 1;

        if(Lives <= 0) {
            if(thing == "TV") {
                gameOverString = "YOU DROPPED THE TV.\n<size=10>HOW COULD YOU?</size>";
            }   
            if(thing == "Egg") {
                gameOverString = "YOU GOT SPLATTED.\n<size=10>HOW HUMILIATING</size>";
            }

            Application.LoadLevel("GameOver");
        }
    }

    public static void PlaySound(AudioClip clip, float volumeCoeff = 1) {
        float volume = (Random.value * (soundVolMax - soundVolMin)) + soundVolMin;
        volume *= volumeCoeff;

        audioSource.PlayOneShot(clip, volume);
    }
}
