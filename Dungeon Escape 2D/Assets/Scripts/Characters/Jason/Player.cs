using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace DCode 
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]

    public class Player : MonoBehaviour, IDamegeable

    {
        Rigidbody2D m_RigidBody;
        SpriteRenderer PlayerspriteRenderer;
        SpriteRenderer SwingEffectspriteRenderer;
        PlayerCharacterAnimation playerAnimation;
        DiamondBehaviour diamondBehaviour;

        public playRandomAudio footSteps;
        public AudioSource coinClip;
        public AudioSource deathClip;
        public AudioSource hitClip;
        public AudioSource swordSwingClip;
        public float groundedraycastDistance = 1f;
        public float groundAcceleration = 4f;
        public float JumpForce = 5.0f;
        public bool _grounded = false;
        public int Health;
        private int _d = 0;
        public int Diamonds { get { return _d; } set { _d = value; UIManager.Instance.HUDGemCount(Diamonds); } }
        bool JumpState = false;
        public LayerMask mask;
        public int health { get; set; }

        // Start is called before the first frame update
        void Start()
        {
            m_RigidBody = GetComponent<Rigidbody2D>();
            playerAnimation = GetComponent<PlayerCharacterAnimation>();
            PlayerspriteRenderer = GetComponentInChildren<SpriteRenderer>();
            SwingEffectspriteRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
            diamondBehaviour = FindObjectOfType<DiamondBehaviour>();
            health = Health;
        }

        // Update is called once per frame
        void Update()
        {

            //walking and running
            float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
            if (horizontalInput < 0f)
            {
                Vector3 newRotation = SwingEffectspriteRenderer.transform.localRotation.eulerAngles;
                Vector3 newPosition = SwingEffectspriteRenderer.transform.localPosition;
                newPosition.x = -0.17f;
                newRotation.z = 144.644f;
                Quaternion newRot = Quaternion.Euler(newRotation);
                SwingEffectspriteRenderer.transform.localPosition = newPosition;
                SwingEffectspriteRenderer.transform.localRotation = newRot;
                PlayerspriteRenderer.flipX = true;
                SwingEffectspriteRenderer.flipX = true;
            }
            else if (horizontalInput > 0f)
            {
                Vector3 newRotation = SwingEffectspriteRenderer.transform.localRotation.eulerAngles;
                Vector3 newPosition = SwingEffectspriteRenderer.transform.localPosition;
                newPosition.x = 0.17f;
                newRotation.z = -144.644f;
                Quaternion newRot = Quaternion.Euler(newRotation);
                SwingEffectspriteRenderer.transform.localPosition = newPosition;
                SwingEffectspriteRenderer.transform.localRotation = newRot;
                PlayerspriteRenderer.flipX = false;
                SwingEffectspriteRenderer.flipX = false;
            }

            //Attack
            SwingSword();

            //jumping
            if (Input.GetKey(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("Jump") && _grounded == true)
            {
                m_RigidBody.velocity = new Vector2(m_RigidBody.velocity.x, JumpForce);
                _grounded = false;
                JumpState = true;
                playerAnimation.Jump(true);
                StartCoroutine(ResetJumpStateRoutine());
            }

            m_RigidBody.velocity = new Vector2(horizontalInput * groundAcceleration, m_RigidBody.velocity.y);
            playerAnimation.Move(horizontalInput);

            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, groundedraycastDistance, mask);

            if (hitInfo.collider != null)
            {

                if (JumpState == false)
                {
                    playerAnimation.Jump(false);
                    _grounded = true;
                }

            }

        }
        IEnumerator ResetJumpStateRoutine()
        {
            yield return new WaitForSeconds(1.2f);
            JumpState = false;
        }
        public void SwingSword()
        {
            if (CrossPlatformInputManager.GetButtonDown("Attack") && _grounded)
            {
                swordSwingClip.Play();
                playerAnimation.SwordAttack();
            }
        }

        public void Damage()
        {
            if(health <= 0)
            {
                return; 
            }
            health--;
            hitClip.Play();
            UIManager.Instance.UIHealth(health);
            if(health <= 0)
            {
                Death();
            }
        }
        public void Death()
        {
            deathClip.Play();
            playerAnimation.Death();
        }

        public void AddGems(int gems)
        {
            Diamonds += gems;
            UIManager.Instance.HUDGemCount(Diamonds);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("diamond"))
            {
                //AddGems(1);
                coinClip.Play();
                diamondBehaviour.AddDiamonds(other.transform.position, 1);
                Destroy(other.gameObject);
            }
        }

        public void PlayFootstep()
        {
            footSteps.PlayRandomSound();
        }
    }
}
