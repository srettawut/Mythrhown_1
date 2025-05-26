using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public AudioSource theMusic;
    public AudioSource hitSound;
    public AudioSource tapSound;
    public AudioSource comboBreakSound;
    public AudioSource applauseMusic;

    public bool startPlaying;

    public BeatScroller theBS;

    public BeatScroller theGBS;

    public static GameManager instance;

    public int currentScore;
    public int scorePerNote = 50;
    public int scorePerGoodNote = 100;
    public int scorePerPerfectNote = 300;

    private int combo;
    public int comboDisplay=0;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    public TMP_Text comboText;
    public TMP_Text scoreText;
    public TMP_Text multiText;
    public TMP_Text startText;

    public float totalNotes;
    public float catchNotes;

    public float goodNotes;
    public float perfectNotes;
    public float missedNotes;

    public GameObject PauseScreen;
    public GameObject resultsScreen;
    public TMP_Text accuracyText,catchsText, goodsText, perfectsText, missesText, rankText, finalScoreText,highestComboText;

    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        comboText.text = "0";
        scoreText.text = "Score: 0";
        currentMultiplier = 1;      

        totalNotes = FindObjectsOfType<NoteObject>().Length;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {      
        if (combo > comboDisplay)
        {
            comboDisplay = combo;
        }
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                theBS.hasStarted = true;
                theGBS.hasStarted = true;
                startText.gameObject.SetActive(false);

                theMusic.Play();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void NoteHit()
    {
        //Debug.Log("Hit On Time");

        combo++;
        comboText.text = "COMBOx "+combo.ToString();

        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }

        }

        multiText.text = "Multiplier: x" + currentMultiplier;

        //currentScore += scorePerNote * currentMultiplier;
        scoreText.text = "Score: " + currentScore;
    }

    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();

        goodNotes++;
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();

        perfectNotes++;
    }

    public void CatchHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();

        catchNotes++;
    }
    public void NoteMissed()
    {
        //Debug.Log("Missed Note");
        if (combo > 20)
        {
            playComboBreakSound();
        }
        combo = 0;
        currentMultiplier = 1;
        multiplierTracker = 0;

        multiText.text = "Multiplier: x" + currentMultiplier;

        missedNotes++;
    }

    public void playHitSound()
    {
        hitSound.Play();     
    }

    public void playTapSound()
    {
        tapSound.Play();
    }

    public void playComboBreakSound()
    {
        comboBreakSound.Play();
    }

    public void showResultScreen()
    {
        if (!resultsScreen.activeInHierarchy)
        {
            resultsScreen.SetActive(true);
            theMusic.Stop();
            applauseMusic.Play();

            catchsText.text = catchNotes.ToString();
            goodsText.text = goodNotes.ToString();
            perfectsText.text = perfectNotes.ToString();
            missesText.text = missedNotes.ToString();

            float totalHit = goodNotes + perfectNotes + catchNotes;
            float percentHit = (totalHit / totalNotes) * 100f;

            accuracyText.text = percentHit.ToString("F2") + "%";

            string rankVal = "F";

            if (percentHit > 40)
            {
                rankVal = "D";
                if (percentHit > 70)
                {
                    rankVal = "C";
                    if (percentHit > 85)
                    {
                        rankVal = "B";
                        if (percentHit > 93)
                        {
                            rankVal = "A";
                            if (percentHit > 97)
                            {
                                rankVal = "S";
                                if (percentHit == 100)
                                {
                                    rankVal = "SS";
                                }
                            }
                        }
                    }
                }

            }

            rankText.text = rankVal;

            finalScoreText.text = currentScore.ToString();
            highestComboText.text = comboDisplay.ToString();
        }
    }

    private void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }
    private void PauseGame()
    {
        isPaused = true;
        theMusic.Pause();
        Time.timeScale = 0f;
        PauseScreen.SetActive(true);
    }

    private void ResumeGame()
    {
        isPaused = false;
        theMusic.UnPause();
        Time.timeScale = 1f;
        PauseScreen.SetActive(false);
    }

    public void OnVirtualKeyPressed(string key)
    {
        KeyCode keyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), key);
        NoteObject[] notes = FindObjectsOfType<NoteObject>(); // หรือใช้ List เก็บไว้

        foreach (NoteObject note in notes)
        {
            note.TrySimulateKey(keyCode);
        }
    }

}
