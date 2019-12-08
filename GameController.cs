using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public GameObject[] pickups;
    public int pickupCount;

    public Text ScoreText;
    public Text restartText;
    public Text gameOverText;

    private bool gameOver;
    private bool restart;
    private int score;

    private AudioSource audio;
    public AudioClip victoryClip;
    public AudioClip lossClip;

    public ParticleController particleSystem;
    public BGScroller bgScript;

    public bool hardModeEnabled;
    public Text hardModeText;

    void Start()
    {
        gameOver = false;
        restart = false;
        hardModeEnabled = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        audio = GetComponent<AudioSource>();
        particleSystem = GameObject.Find("ParticleController").GetComponent<ParticleController>();
        bgScript = GameObject.Find("Background").GetComponent<BGScroller>();
    }
    
    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene("SpaceShooter");
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            hardModeText.text = "Hard Mode Enabled";
            hardModeEnabled = true;
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                if (hardModeEnabled)
                {
                    hazard.GetComponent<Mover>().speed = -10;
                }
                else if (!hardModeEnabled)
                {
                    hazard.GetComponent<Mover>().speed = -5;
                }
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }

            for (int i = 0; i < pickupCount; i++)
            {
                GameObject pickup = pickups[Random.Range(0, pickups.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;

                Instantiate(pickup, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }



            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'E' to Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;
        if (score >= 250)
        {
            gameOverText.text = "You win! Game created by Kevin Le";
            particleSystem.WinGame();
            bgScript.WinGame();
            gameOver = true;
            restart = true;
        }

        if (score == 250 || score == 260)
        {
            audio.Stop();
            audio.PlayOneShot(victoryClip);
        }
        if (score == 255 || score == 265)
        {
            audio.Stop();
            audio.PlayOneShot(victoryClip);
        }
    }

    public void GameOver()
    {
        gameOverText.text = "You Lose! Try Again...";
        audio.Stop();
        audio.PlayOneShot(lossClip);
        gameOver = true;
    }
}
