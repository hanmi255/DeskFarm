using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class AnimalAI : MonoBehaviour
{
    [SerializeField] private GameObject egg;
    [SerializeField] private GameObject AfterGrow;
    [SerializeField] private float speed = 0.4f;
    [SerializeField] private Vector2 timerRange = new(2f, 5f);
    [SerializeField] private float eggSpawnDelay = 20f;
    [SerializeField] private float growDelay = 30f;

    private Animator animator;
    private Rigidbody2D rigidBody;
    private Transform eggs;

    private float stateTimer;
    private float currentStateDuration;
    private float eggSpawnTimer;
    private float growTimer;
    private bool isWalking;
    private int direction = 1; // 1 for right, -1 for left

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        eggs = GameObject.Find("MainScene").transform.Find("Eggs");

        SetNewState();
    }

    private void FixedUpdate()
    {
        stateTimer += Time.fixedDeltaTime;
        eggSpawnTimer += Time.fixedDeltaTime;
        growTimer += Time.fixedDeltaTime;

        if (stateTimer >= currentStateDuration)
        {
            stateTimer = 0f;
            // 设置新的状态
            SetNewState();
        }

        if (eggSpawnTimer >= eggSpawnDelay)
        {
            eggSpawnTimer = 0f;

            // 创建蛋
            GameObject e = Instantiate(egg, eggs);
            e.transform.position = transform.position;
        }

        if (AfterGrow != null && growTimer >= growDelay)
        {
            growTimer = 0f;

            // 原地成长
            Instantiate(AfterGrow, transform.position,Quaternion.identity,transform.parent);
            Destroy(gameObject);
        }


        HandleMovement();
    }

    private void SetNewState()
    {
        currentStateDuration = Random.Range(timerRange.x, timerRange.y);

        // 切换移动状态
        isWalking = !isWalking;

        // 设置随机移动方向
        if (isWalking)
        {
            direction = Random.Range(0, 2) == 0 ? 1 : -1;
            UpdateVisualDirection();
        }

        // 更新动画状态
        animator.SetBool("isWalking", isWalking);
    }

    private void HandleMovement()
    {
        if (isWalking)
        {
            Vector2 velocity = new(direction * speed, rigidBody.velocity.y);
            rigidBody.velocity = velocity;
        }
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }
    }

    private void UpdateVisualDirection()
    {
        transform.localScale = new Vector3(-direction, 1, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Left")
        {
            direction = 1;
            UpdateVisualDirection();
        }
        if (collision.gameObject.name == "Right")
        {
            direction = -1;
            UpdateVisualDirection();
        }
    }
}
