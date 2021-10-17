using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DCode
{
    public class PlayerCharacterAnimation : MonoBehaviour
    {
        private Animator m_Animator;
        private Animator swing_Animator;


        private void Start()
        {
            m_Animator = GetComponent<Animator>();
            swing_Animator = transform.GetChild(1).GetComponent<Animator>();
        }

        public void Move(float move)
        {
            m_Animator.SetFloat("Horizontal", move);
        }
        public void Jump(bool jumping)
        {
            m_Animator.SetBool("Jumping", jumping);
        }
        public void SwordAttack()
        {
            m_Animator.SetTrigger("Swing");
            swing_Animator.SetTrigger("SwingEffect");
        }
       public void Death()
        {
            m_Animator.SetTrigger("DeathTrigger");
        }
    }
}

