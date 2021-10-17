using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DCode
{
    public class DiamondBehaviour : MonoBehaviour
    {
        public int gems = 1;

        [SerializeField] GameObject animatedDiamondPrefab;
        [SerializeField] Transform target;

        [Space]
        [Header("Available coins ( coins to pool)")]
        [SerializeField] int maxCoins;
        Queue<GameObject> diamondQueue = new Queue<GameObject>();

        [Space]
        [Header("Animation settings")]
        [SerializeField] [Range(0.5f, 0.9f)] float minAnimDuration;
        [SerializeField] [Range(0.9f, 2f)] float maxAnimDuration;
        [SerializeField] Ease easeType;

        Vector3 targetPosition;

        private void Awake()
        {

            //prepare pool
            PrepareCoins();
        }

        private void Update()
        {
            targetPosition = target.position;
        }

        void PrepareCoins()
        {
            GameObject diamond;
            for (int i = 0; i < maxCoins; i++)
            {
                diamond = Instantiate(animatedDiamondPrefab);
                diamond.transform.parent = transform;
                diamond.SetActive(false);
                diamondQueue.Enqueue(diamond);
            }
        }

        void Animate( Vector3 collectedDiamondPosition, int amount)
        {
            for(int i = 0; i < amount; i++)
            {
                //Check if theres diamond in the pool
                if(diamondQueue.Count > 0)
                {
                    //Extract a diamond from the pool
                    GameObject diamond = diamondQueue.Dequeue();
                    diamond.SetActive(true);

                    //move diamond
                    diamond.transform.position = collectedDiamondPosition;

                    //animate move diamond
                    float duration = Random.Range(minAnimDuration, maxAnimDuration);
                    diamond.transform.DOMove(targetPosition, duration)
                        .SetEase(easeType)
                        .OnComplete(() => {
                           // executes when diamond reaches target
                           diamond.SetActive(false);
                           diamondQueue.Enqueue(diamond);

                           GameManager.Instance.player.Diamonds++;
                       });
                }
            }
        }

        /**private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Player")
            {
                Player player = collision.GetComponent<Player>();

                if(player != null)
                {
                    player.AddGems(gems);
                    Animate(collision.transform.position, 1);
                    Destroy(this.gameObject);
                }
               
            }
        }**/

        public void AddDiamonds(Vector3 collectedDiamondPos , int amount)
        {
            Animate(collectedDiamondPos, amount);
        }
    }

}