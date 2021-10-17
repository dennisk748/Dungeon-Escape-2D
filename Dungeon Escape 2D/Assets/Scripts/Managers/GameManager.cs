using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DCode
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;

        public static GameManager Instance { get { return _instance; } }

        public bool hasKey;
        public Player player { get; private set; }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }

            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

    }
}
