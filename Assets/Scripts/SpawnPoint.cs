using UnityEngine;

namespace Assets.Scripts
{
    public class SpawnPoint : MonoBehaviour
    {
        public playerType Type;
        void Start()
        {
            if (GameObject.Find("Player 1").GetComponent<PlayerController>().Type == Type)
            {
                GameObject.Find("Player 1").transform.position = transform.position;
            }
            else
            {
                GameObject.Find("Player 2").transform.position = transform.position;
            }
        }
    }
}
