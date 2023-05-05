using UnityEngine;
using System.Collections;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;// độ trễ của ngọn lễ bốc lên
    [SerializeField] private float activeTime;// thời gian của ngọn lửa hoạt động 
    private Animator anim;
    private SpriteRenderer spriteRend;

    [Header("SFX")]
    [SerializeField] private AudioClip firetrapSound;

    private bool triggered; //khi lửa kích hoạt
    private bool active; //khi hoạt động 

    private Health playerHealth;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (playerHealth != null && active)
            playerHealth.TakeDamage(damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerHealth = collision.GetComponent<Health>();

            if (!triggered)// nếu lửa chưa hoạt động 
                StartCoroutine(ActivateFiretrap());//phải kích hoạt 

            if (active)
                collision.GetComponent<Health>().TakeDamage(damage);// nhận sát thương
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            playerHealth = null;
    }
    private IEnumerator ActivateFiretrap()
    {
        //bật lên ngọn lửa màu đỏ để thông báo cho người chơi biêt 
        triggered = true;
        spriteRend.color = Color.red;

        //Chờ độ trễ, kích hoạt bẫy, bật hoạt hình, trả lại màu sắc bình thường
        yield return new WaitForSeconds(activationDelay);
        SoundManager.instance.PlaySound(firetrapSound);
        spriteRend.color = Color.white; //biến trở lại ban đầu 
        active = true;
        anim.SetBool("activated", true);

        // Đợi đến X giây, tắt bẫy và đặt lại tất cả các biến và hoạt ảnh
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}