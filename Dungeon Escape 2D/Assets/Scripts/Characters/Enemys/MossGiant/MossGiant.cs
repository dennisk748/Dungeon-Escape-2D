using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DCode
{
    public class MossGiant : EnemyBehaviour, IDamegeable
    {

        public int health { get; set; }

        public override void Initializer()
        {
            base.Initializer();
            health = Health;
        }

        public override void Update()
        {
            base.Update();
        }


        public void Die()
        {
            animator.SetTrigger("DeathTrigger");
            Destroy(this.gameObject, 2.0f);
            DiamondBurst();
        }

        public void Damage()
        {
            animator.SetTrigger("HitTrigger");
            isHit = true;
            animator.SetBool("InCombat", true);

            health--;
            if (health <= 0)
            {
                Die();
            }
        }

    }
}
