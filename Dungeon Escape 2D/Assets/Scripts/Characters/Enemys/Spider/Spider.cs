using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DCode
{
    public class Spider : EnemyBehaviour, IDamegeable
    {
        public int health { get; set; }
        public GameObject AcidEffect;
        Vector3 direction;

        public override void Initializer()
        {
            base.Initializer();
            health = Health;
        }

        public override void Update()
        {
            base.Update();
            direction = player.transform.position - transform.position;
            
        }


        public override void Movement()
        {
          
        }

        public void Damage()
        {
            Debug.Log("spider hit");
            health--;
            if (health <= 0)
            {
                Die();
            }
        }

        public void Attack()
        {
            if (direction.x <= 8.0f)
            {
                Instantiate(AcidEffect, transform.position, Quaternion.identity);
            }
        }

        public void Die()
        {
            animator.SetTrigger("DeathTrigger");
            Destroy(this.gameObject, 2.0f);
            DiamondBurst();
        }
        
    }
}
