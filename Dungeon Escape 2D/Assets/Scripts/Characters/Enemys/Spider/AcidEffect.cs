using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DCode
{
    public class AcidEffect : MonoBehaviour
    {
        private void Start()
        {
            Destroy(this.gameObject, 5.0f);
        }

        private void Update()
        {
            gameObject.transform.Translate(Vector2.right * 3.0f * Time.deltaTime);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                IDamegeable damage = other.GetComponent<IDamegeable>();

                if(damage != null)
                {
                    damage.Damage();
                    Destroy(this.gameObject);
                }
            }
            
        }
    }
}

