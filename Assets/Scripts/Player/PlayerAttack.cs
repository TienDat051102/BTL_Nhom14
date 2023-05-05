using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;//khoảng thời gian phải trôi qua trước khi chúng ta thực hiện phát bắn tiếp 
    [SerializeField] private Transform firePoint;//vị trị mà các viên đạn được bắn ra 
    [SerializeField] private GameObject[] fireballs;//chứa các quả cầu lửa mình tạo ra 
    [SerializeField] private AudioClip fireballSound;

    private Animator anim;
    private PlayerMovement playerMovement;// chuyển động của người chơi 
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack()//kiểm tra có nhấn chuột trái không
            && Time.timeScale > 0)
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(fireballSound);
        anim.SetTrigger("attack");// gọi đến hoạt ảnh khè ra lửa 
        cooldownTimer = 0;

        fireballs[FindFireball()].transform.position = firePoint.position;//set vị trí của fireball trong firePoint
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));//lấy thành phần đường đạn và sử dụng hướng đã đặt để gửi nó theo hướng mà người chươi đang đối mặt 
    }
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)//kiểm tra active, nếu quả 1 được bắn cta sẽ ko phải chờ nữa mà bắn được luôn quả 2 
                return i;
        }
        return 0;
    }
}