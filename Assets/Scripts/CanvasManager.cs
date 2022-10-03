using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Text Blue, Orange, CountDown;
    public AudioSource StartBeat, GOOOOO;
    public Text[] ToBeHidden;
    private bool showingWinner;
    public float speed;
    public decimal countDown = 5m;

    public void Start()
    {
        StartCoroutine(startCountDown());
        if (!PlayerPrefs.HasKey("Mute") || PlayerPrefs.GetInt("Mute") == 0)
        {
            StartBeat.Play();
        }
    }

    IEnumerator startCountDown()
    {
        while (countDown > 0)
        {
            yield return new WaitForSeconds(1);
            countDown--;
            CountDown.text = countDown.ToString();
            if (!PlayerPrefs.HasKey("Mute") || PlayerPrefs.GetInt("Mute") == 0)
            {
                if (countDown == 0)
                {
                    GOOOOO.Play();
                }
                else
                {
                    StartBeat.Play();
                }
            }
        }
        CountDown.gameObject.SetActive(false);
        GameObject.Find("Player 1").GetComponent<PlayerController>().GameStarted = true;
        GameObject.Find("Player 2").GetComponent<PlayerController>().GameStarted = true;
    }
    public void ShowWinner()
    {
        showingWinner = true;
        Invoke("NextGame", 1);
    }

    public void NextGame()
    {
        showingWinner = false;
        Invoke("Restart", 1);
    }
    private void Update()
    {
        Blue.text = Global.PLAYER1.ToString();
        Orange.text = Global.PLAYER2.ToString();
        if (showingWinner)
        {
            foreach (Text text in ToBeHidden)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(text.color.a,1f,speed * Time.deltaTime));
            }
        }
        else
        {
            foreach (Text text in ToBeHidden)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(text.color.a, 0f, speed * Time.deltaTime));
            }
        }
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
    public void Restart()
    {
        Global.FLIPROLE = !Global.FLIPROLE;
        Global.REWINDING = false;
        SceneManager.LoadScene(1);
    }
}
