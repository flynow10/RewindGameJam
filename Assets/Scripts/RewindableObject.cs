using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Assets.Scripts
{
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class RewindableObject : MonoBehaviour
    {
        public bool _rewinding;
        private Vector3 startPos;
        private Quaternion startRot;
        public float speed;
        private GameObject SpinningClock;
        public GameObject TimeGhost;
        public GameObject TimeGhostPrefab;

        private void Start()
        {
            startPos = transform.position;
            startRot = transform.rotation;
            SpinningClock = GameObject.Find("ClockRoot").transform.GetChild(0).gameObject;
        }

        private void Update()
        {
            if (_rewinding)
            {
                SpinningClock.SetActive(true);
                transform.position = Vector3.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
                if (GameObject.Find("Player 1") != null && GameObject.Find("Player 2") != null)
                {
                    Physics.IgnoreCollision(this.GetComponent<Collider>(),
                        GameObject.Find("Player 1").GetComponent<PlayerController>().Type == playerType.Runner
                            ? GameObject.Find("Player 1").GetComponent<Collider>()
                            : GameObject.Find("Player 2").GetComponent<Collider>(), true);
                }

                transform.rotation = Quaternion.RotateTowards(transform.rotation, startRot,
                    speed * 30 * Time.deltaTime);
                GetComponent<Rigidbody>().isKinematic = true;
                Time.timeScale = 0.5f;
                VolumeProfile slowMo = GameObject.Find("SlowMo").GetComponent<Volume>().profile;
                Vignette vignette = (Vignette)slowMo.components.Find((x) => { return x.name == "Vignette(Clone)"; });
                vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 0.5f, 24f * Time.deltaTime);
                if (Vector3.Distance(transform.position, startPos) < 0.1 && transform.rotation == startRot)
                {
                    Destroy(TimeGhost);
                    SpinningClock.SetActive(false);
                    GetComponent<Rigidbody>().isKinematic = false;
                    if (GameObject.Find("Player 1") != null && GameObject.Find("Player 2") != null)
                    {
                        Physics.IgnoreCollision(this.GetComponent<Collider>(),
                            GameObject.Find("Player 1").GetComponent<PlayerController>().Type == playerType.Runner
                                ? GameObject.Find("Player 1").GetComponent<Collider>()
                                : GameObject.Find("Player 2").GetComponent<Collider>(), false);
                    }

                    _rewinding = false;
                    Global.REWINDING = false;
                    Time.timeScale = 1;
                }
            }

            if (!_rewinding && !Global.REWINDING)
            {
                VolumeProfile slowMo = GameObject.Find("SlowMo").GetComponent<Volume>().profile;
                Vignette vignette = (Vignette)slowMo.components.Find((x) => { return x.name == "Vignette(Clone)"; });
                vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 0f, 24f * Time.deltaTime);
            }

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (TimeGhost == null)
                {
                    TimeGhost = Instantiate(TimeGhostPrefab, startPos, startRot);
                    TimeGhost.transform.localScale = transform.localScale * 1.01f;
                    TimeGhost.GetComponent<MeshFilter>().mesh = GetComponent<MeshFilter>().sharedMesh;
                }
            }
        }
    }
}
