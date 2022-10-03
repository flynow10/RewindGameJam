using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Assets.Scripts
{
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
    public class PlayerController : MonoBehaviour
    {
        public playerType Type;
        public playerNumber Number;
        public float Speed;
        public float XInput;
        public float YInput;
        float _boost;
        public Rigidbody Rb;
        public PlayerController OtherPlayer;
        public RewindableObject lastObject;
        public GameObject PlayerTimeGhostPrefab;
        private GameObject PlayerTimeGhost;
        public bool Rewinding, GameStarted;
        private GameObject SpinningClock;
        public CanvasManager manager;
        public Material Player2Material, Player1Material;
        public GameObject PlayerExplosion;
        public TMP_Text Tag;

        private void OnValidate()
        {
            if (OtherPlayer != null)
                OtherPlayer.Speed = Speed;
        }

        private void Start()
        {
            SpinningClock = GameObject.Find("ClockRoot").transform.GetChild(0).gameObject;
            manager = GameObject.Find("EndRoot").transform.GetChild(0).GetComponent<CanvasManager>();
            SpinningClock.SetActive(false);
            if (Global.FLIPROLE && Type == playerType.Chaser)
            {
                Type = playerType.Runner;
            }
            else
            {
                if (Global.FLIPROLE && Type == playerType.Runner)
                {
                    Type = playerType.Chaser;
                }
            }

            switch (Type)
            {
                case playerType.Chaser:
                {
                    Tag.text = "Chaser";
                    break;
                }
                case playerType.Runner:
                {
                    Tag.text = "Runner";
                    break;
                }
            }
            Invoke("HideText", 6);
        }

        void HideText()
        {
            Tag.gameObject.SetActive(false);
        }

        
        private void Update()
        {
            if (GameStarted)
            {
                if (Type == playerType.Runner)
                {
                    _boost = 75f;
                }
                else
                {
                    _boost = 0f;
                }

                if (!Rewinding && !Global.WINNER)
                {
                    switch (Number)
                    {
                        case playerNumber.Player1:
                            XInput = Input.GetAxisRaw("Player1 X");
                            YInput = Input.GetAxisRaw("Player1 Y");
                            break;
                        case playerNumber.Player2:
                            XInput = Input.GetAxisRaw("Player2 X");
                            YInput = Input.GetAxisRaw("Player2 Y");
                            break;
                    }
                }
                else
                {
                    XInput = 0;
                    YInput = 0;
                }

                if (!Global.WINNER)
                {
                    switch (Number)
                    {
                        case playerNumber.Player1:
                        {
                            GetComponent<MeshRenderer>().material = Player1Material;
                            break;
                        }
                        case playerNumber.Player2:
                        {
                            GetComponent<MeshRenderer>().material = Player2Material;
                            break;
                        }
                    }

                    Vignette vignette;
                    VolumeProfile slowMo;
                    if (Rewinding)
                    {
                        SpinningClock.SetActive(true);
                        transform.position = Vector3.MoveTowards(transform.position, PlayerTimeGhost.transform.position,
                            3 * Time.deltaTime);
                        GetComponent<Collider>().enabled = false;
                        transform.rotation = Quaternion.RotateTowards(transform.rotation,
                            PlayerTimeGhost.transform.rotation,
                            3 * 30 * Time.deltaTime);
                        Time.timeScale = 0.6f;
                        slowMo = GameObject.Find("SlowMo").GetComponent<Volume>().profile;
                        vignette = (Vignette) slowMo.components.Find((x) => x.name == "Vignette(Clone)");
                        vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 0.5f, 24f * Time.deltaTime);
                        if (Vector3.Distance(transform.position, PlayerTimeGhost.transform.position) < 0.1 &&
                            transform.rotation == PlayerTimeGhost.transform.rotation)
                        {
                            GetComponent<Collider>().enabled = true;
                            Destroy(PlayerTimeGhost);
                            SpinningClock.SetActive(false);
                            Rewinding = false;
                            Global.REWINDING = false;
                        }
                    }

                    if (!Rewinding && !Global.REWINDING)
                    {
                        Time.timeScale = 1;
                        slowMo = GameObject.Find("SlowMo").GetComponent<Volume>().profile;
                        vignette = (Vignette) slowMo.components.Find((x) => x.name == "Vignette(Clone)");
                        vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 0f, 24f * Time.deltaTime);
                    }

                    if (!Rewinding)
                    {
                        switch (Number)
                        {
                            case playerNumber.Player1:
                            {
                                switch (Type)
                                {
                                    case playerType.Runner:
                                    {
                                        if (Input.GetKeyDown(KeyCode.F) && lastObject != null)
                                        {
                                            lastObject._rewinding = true;
                                            Global.REWINDING = true;
                                            lastObject = null;
                                        }

                                        break;
                                    }
                                    case playerType.Chaser:
                                    {
                                        if (Input.GetKeyDown(KeyCode.E) && PlayerTimeGhost != null)
                                        {
                                            Rewinding = true;
                                            Global.REWINDING = true;
                                        }

                                        if (Input.GetKeyDown(KeyCode.F))
                                        {
                                            if (PlayerTimeGhost != null)
                                            {
                                                Destroy(PlayerTimeGhost);
                                            }

                                            PlayerTimeGhost = Instantiate(PlayerTimeGhostPrefab, transform.position,
                                                transform.rotation);
                                        }

                                        break;
                                    }
                                }

                                break;
                            }
                            case playerNumber.Player2:
                            {
                                switch (Type)
                                {
                                    case playerType.Runner:
                                    {
                                        if (Input.GetKeyDown(KeyCode.K) && lastObject != null)
                                        {
                                            lastObject._rewinding = true;
                                            Global.REWINDING = true;
                                            lastObject = null;
                                        }

                                        break;
                                    }
                                    case playerType.Chaser:
                                    {
                                        if (Input.GetKeyDown(KeyCode.O) && PlayerTimeGhost != null)
                                        {
                                            Rewinding = true;
                                            Global.REWINDING = true;
                                        }

                                        if (Input.GetKeyDown(KeyCode.K))
                                        {
                                            if (PlayerTimeGhost != null)
                                            {
                                                Destroy(PlayerTimeGhost);
                                            }

                                            PlayerTimeGhost = Instantiate(PlayerTimeGhostPrefab, transform.position,
                                                transform.rotation);
                                        }

                                        break;
                                    }
                                }

                                break;
                            }
                        }
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            Rb.velocity = Vector3.zero;
            switch (YInput)
            {
                case 1:
                    Rb.velocity+= Rb.transform.forward * (Speed + _boost) * Time.deltaTime;
                    break;
                case -1:
                    Rb.velocity += -Rb.transform.forward * (Speed + _boost) * Time.deltaTime;
                    break;
            }

            switch (XInput)
            {
                case 1:
                    Rb.velocity += Rb.transform.right * (Speed + _boost) * Time.deltaTime;
                    break;
                case -1:
                    Rb.velocity += -Rb.transform.right * (Speed + _boost) * Time.deltaTime;
                    break;
            }
        }

        public void Die()
        {
            GameObject particlesystem = Instantiate(PlayerExplosion, transform.position, transform.rotation);
            int i = 0;
            if (PlayerPrefs.HasKey("Mute") && PlayerPrefs.GetInt("Mute") == 1)
            {
                particlesystem.GetComponent<AudioSource>().Stop();
            }

            foreach (Material material in particlesystem.GetComponent<Renderer>().materials)
            {
                if (i == 1)
                    material.SetColor("_BaseColor", new Color(GetComponent<MeshRenderer>().material.GetColor("Color_31628686").r, GetComponent<MeshRenderer>().material.GetColor("Color_31628686").g, GetComponent<MeshRenderer>().material.GetColor("Color_31628686").b, 0.5f));
                else
                {
                    material.SetColor("_BaseColor", GetComponent<MeshRenderer>().material.GetColor("Color_31628686"));
                }
                i++;
            }
            gameObject.SetActive(false);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Moveable")
            {
                lastObject = collision.gameObject.GetComponent<RewindableObject>();
            }

            if (collision.gameObject.tag == "Player" && Type == playerType.Chaser && !Global.WINNER)
            {
                Global.WINNER = true;
                manager.ShowWinner();
                if (Number == playerNumber.Player1)
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
        }
    }
}
