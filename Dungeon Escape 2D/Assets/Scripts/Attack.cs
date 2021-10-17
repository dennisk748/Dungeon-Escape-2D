using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DCode
{
    public class Attack : MonoBehaviour
    {
        bool canDamage = true;
        public void OnTriggerEnter2D(Collider2D other)
        {

            IDamegeable hit = other.GetComponent<IDamegeable>();

            if (hit != null)
            {
                if (canDamage)
                {
                    hit.Damage();
                    canDamage = false;
                    StartCoroutine(CanDamageCoroutine());
                } 
            }
        }

        IEnumerator CanDamageCoroutine()
        {
            yield return new WaitForSeconds(0.5f);
            canDamage = true;
        }
    }

}