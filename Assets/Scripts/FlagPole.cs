using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class FlagPole : MonoBehaviour
    {
        public SerializableDecimal timeTilPoint = new SerializableDecimal();

        public Slider slider;
        public Text text;
        public Transform canvas;
        public CanvasManager manager;
        public bool CountingDown;
        public bool OtherSide;
        public bool sentWinnerMessage = false;

        private void Start()
        {
            timeTilPoint.value = 2m;
            manager = GameObject.Find("EndRoot").transform.GetChild(0).GetComponent<CanvasManager>();
        }
        private IEnumerator CountDown()
        {
            CountingDown = true;
            while (timeTilPoint.value > 0)
            {
                yield return new WaitForSeconds(0.05f);
                timeTilPoint.value -= 0.05M;
            }
            CountingDown = false;
        }

        public void LookAtCamera()
        {
            if (OtherSide)
            {
                canvas.localPosition = new Vector3(canvas.localPosition.x, canvas.localPosition.y, -10.5f);
            }
            canvas.LookAt(GameObject.Find("Main Camera").transform.position);
            Vector3 lookat = GameObject.Find("Main Camera").transform.position - transform.position;
            lookat.y = 0;
            transform.rotation = Quaternion.LookRotation(lookat);
            transform.Rotate(Vector3.up,90);
        }
        private void Update()
        {
            slider.value = (float)timeTilPoint.value;
            text.text = Math.Ceiling(timeTilPoint.value).ToString();
            if (timeTilPoint.value == 0m && !Global.WINNER)
            {
                Global.WINNER = true;
                canvas.gameObject.SetActive(false);
                manager.ShowWinner();
                if (GameObject.Find("Player 1").GetComponent<PlayerController>().Type == playerType.Runner)
                {
                    GameObject.Find("Player 2").GetComponent<PlayerController>().Die();
                    Global.PLAYER1++;
                }
                else
                {
                    GameObject.Find("Player 1").GetComponent<PlayerController>().Die();
                    Global.PLAYER2++;
                }
            }

            if (Global.WINNER && !sentWinnerMessage)
            {
                sentWinnerMessage = true;

            }
        }
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (collision.gameObject.GetComponent<PlayerController>().Type == playerType.Runner && !CountingDown && !Global.WINNER)
                {
                    timeTilPoint.value = 2M;
                    StartCoroutine(CountDown());
                    
                    canvas.gameObject.SetActive(true);
                }
            }
        }
        private void OnTriggerExit(Collider collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (collision.gameObject.GetComponent<PlayerController>().Type == playerType.Runner)
                {
                    StopAllCoroutines();
                    CountingDown = false;
                    canvas.gameObject.SetActive(false);
                    timeTilPoint.value = 2M;
                }
            }
        }
    }
}
