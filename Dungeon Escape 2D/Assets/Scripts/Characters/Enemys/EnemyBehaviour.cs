using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamekit2D;

namespace DCode
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class EnemyBehaviour : MonoBehaviour
    {
        static Collider2D[] s_colliderCaches = new Collider2D[16];

        public Vector3 moveVector { get { return m_moveVector; } }

        [Tooltip("If the sprite face left on the spritesheet, enable this. Otherwise, leave disabled")]
        public bool spriteFaceLeft = false;

        public Transform player;
        public GameObject Diamonds;
        public Transform point_A, point_B;
        public LayerMask groundedLayerMask;
        protected SpriteRenderer spriteRenderer;
        protected Animator animator;

        
        protected Vector3 currentTarget;
        public int Health;
        public float Speed;
        public int Gems;
        public float range;
        protected bool isHit = false;

        public Vector2 Velocity { get; protected set; }
        Rigidbody2D m_Rigidbody2D;
        Vector2 m_PreviousPosition;
        Vector2 m_CurrentPosition;
        Vector2 m_NextMovement;

        //as we flip the sprite instead of rotating/scaling the object, this give the forward vector according to the sprite orientation
        protected Vector2 m_SpriteForward;
        protected Bounds m_LocalBounds;
        protected Vector3 m_moveVector;

        private void Awake()
        {

            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();

            m_PreviousPosition = m_Rigidbody2D.position;
            m_CurrentPosition = m_Rigidbody2D.position;

            m_SpriteForward = spriteFaceLeft ? Vector2.left : Vector2.right;
            if (spriteRenderer.flipX) m_SpriteForward = -m_SpriteForward;
        }


        public virtual void Initializer()
        {
            animator = GetComponentInChildren<Animator>();

            m_LocalBounds = new Bounds();
            int count = m_Rigidbody2D.GetAttachedColliders(s_colliderCaches);
            for(int i = 0; i < count; i++)
            {
                m_LocalBounds.Encapsulate(transform.InverseTransformBounds(s_colliderCaches[i].bounds));
            }
        }

        public virtual void Start()
        {
            Initializer();

            SceneLinkedSMB<EnemyBehaviour>.Initialise(animator, this);
        }

        public virtual void Update()
        {
            m_PreviousPosition = m_Rigidbody2D.position;
            m_CurrentPosition = m_PreviousPosition + m_NextMovement;
            Velocity = (m_CurrentPosition - m_PreviousPosition) / Time.deltaTime;

            m_Rigidbody2D.MovePosition(m_CurrentPosition);
            m_NextMovement = Vector2.zero;

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !animator.GetBool("InCombat"))
            {
                return;
            }

            //Movement();
            FacePlayer();
        }
        private void FixedUpdate()
        {
            Move(m_moveVector * Time.deltaTime);
        }


        public void Move(Vector2 movement)
        {
            m_NextMovement += movement;
        }


        public void SetHorizontalSpeed(float HorizontalSpeed)
        {
            m_moveVector.x = HorizontalSpeed * m_SpriteForward.x;
        }

        public bool ObstaclesCheck()
        {
            Vector3 castingPosition = (Vector2)(transform.position + m_LocalBounds.center) + m_SpriteForward * m_LocalBounds.extents.x;
            Debug.DrawLine(castingPosition, castingPosition + Vector3.down * (m_LocalBounds.extents.y + 0.2f));

            if (!Physics2D.CircleCast(castingPosition, 0.1f, Vector2.down, m_LocalBounds.extents.y + 0.2f, groundedLayerMask.value))
            {
                return true;
            }

            return false;

        }

        public virtual void Movement()
        {
            if (currentTarget == point_A.position)
            {
                spriteRenderer.flipX = true;

            }
            else
            {
                spriteRenderer.flipX = false;
            }
            if (transform.position == point_A.position)
            {
                currentTarget = point_B.position;
                animator.SetTrigger("IdleTrigger");
            }
            else if (transform.position == point_B.position)
            {
                currentTarget = point_A.position;
                animator.SetTrigger("IdleTrigger");
            }

            if (!isHit)
            {
                transform.position = Vector3.MoveTowards(transform.position, currentTarget, Speed * Time.deltaTime);
            }

            float playerInRange = Vector3.Distance(player.transform.position, transform.position);
            if(playerInRange > range)
            {
                isHit = false;
                animator.SetBool("InCombat", false);
            }
        }

        public void FacePlayer()
        {
            Vector3 direction = player.transform.position - transform.position;

            if (direction.x < 0f && animator.GetBool("InCombat") && !spriteRenderer.flipX)
            {
                spriteRenderer.flipX = true;
            }
            else if (direction.x > 0f && animator.GetBool("InCombat") && spriteRenderer.flipX)
            {
                spriteRenderer.flipX = false;
            }
        }
        public void DiamondBurst()
        {
            for (int i = 0; i < Gems; i++)
            {
                Instantiate(Diamonds, transform.position, Quaternion.identity);
            }

        }
    }

}

