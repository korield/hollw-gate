    using UnityEngine;

    public class Player : MonoBehaviour
    {
        [Header("Camera Stuff")]
        [SerializeField] private GameObject _cameraFollowObject;
        private CameraFollowOBJECT cameraFollowObject;  
        [Header("Movement")]
        public float moveSpeed = 15f;

        [Header("Jump")]
        public float jumpForce = 40f;          
        public float fallMultiplier = 10f;
        public float lowJumpMultiplier = 10f;
        public float ascentMultiplier = 3.5f;  

        [Header("Camera")]
        public Camera mainCamera;

        [Header("Animation")]
        public int animation_attack_time = 2;

        [Header("Attack")]
        public Transform attackPoint;
        public LayerMask enemyLayer;
        public float attackRange = 0.5f;
        public int attackDamage = 5;
        public float attackRate = 2f;
        float nextAttackTime = 0f;
        public int maxHealth_Player = 5;
        int currentHealth_Player;

        private Rigidbody2D rb;
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            if (mainCamera == null) mainCamera = Camera.main;

            if (_cameraFollowObject != null)
            {
                cameraFollowObject = _cameraFollowObject.GetComponent<CameraFollowOBJECT>();   
            }
            currentHealth_Player = maxHealth_Player;
      
        }

        void Update()
        {
            Time.timeScale = 2f;

            if(Time.time >= nextAttackTime){

                 if (Input.GetKeyDown(KeyCode.Mouse0)){
                     Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
            float moveInput = 0f;
            // Links: A oder Pfeil Links
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
                moveInput -= 1f;
                Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
                transform.rotation = Quaternion.Euler(rotator);
            }
            // Rechts: D oder Pfeil Rechts
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
                moveInput += 1f;
                Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
                transform.rotation = Quaternion.Euler(rotator);
            }
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

            // Springen: Space oder W oder Pfeil Hoch
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && Mathf.Abs(rb.linearVelocity.y) < 0.01f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }

            if (rb.linearVelocity.y > 0)
            {
                rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (ascentMultiplier - 1f) * Time.deltaTime;
            }

            // Low-jump nur wenn keine der Sprungtasten gehalten wird
            if (rb.linearVelocity.y < 0 && !Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.UpArrow))
            {
                rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1f) * Time.deltaTime;
            }
        
        
       
       
        }

        public void Attack()
        {
            //Attack Animation muss man hier noch machen aber Peter ist zu faul
            Collider2D [] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            foreach(Collider2D enemy in hitEnemies){
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);

            }
        }
        void OnDrawGizmosSelected()
        {
             if (attackPoint == null)
                 return;

             Gizmos.DrawWireSphere(attackPoint.position, attackRange);
            

        }
        public void TakeDamage_Player(int damage)
        {
            currentHealth_Player -= damage;
            if (currentHealth_Player <= 0)
            {
                Die_Player();
            }
        }
        void Die_Player()
        {
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
        }

    }