using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;// hủy cung tên sau 1 khoảng thời gian
    private float lifetime;
    private Animator anim;
    private BoxCollider2D coll;

    private bool hit;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;
    }
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        base.OnTriggerEnter2D(collision); // thực thi các tập lệnh trong từ các lệnh cha trước 
        coll.enabled = false;

        if (anim != null)
            anim.SetTrigger("explode"); //Khi đối tượng là 1 quả cầu lửa thì phát nổ 
        else
            gameObject.SetActive(false); //khi va phải đối tượng nào khác , hủy kích hoạt mũi tên
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}