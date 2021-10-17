using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DCode
{
    public class SpiderAnimationEffect : MonoBehaviour
    {
        Spider spider;
        

        private void Start()
        {
            spider = GetComponentInParent<Spider>();
        }


        public void Fire()
        {
            spider.Attack();
        }
    }
}
