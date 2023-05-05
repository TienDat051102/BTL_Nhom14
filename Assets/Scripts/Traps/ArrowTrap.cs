using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;// thời gian bắn cung 
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    private float cooldownTimer;//thời gian hồi chiêu

    [Header("SFX")]
    [SerializeField] private AudioClip arrowSound;

    private void Attack()
    {
        cooldownTimer = 0;

        SoundManager.instance.PlaySound(arrowSound);
        arrows[FindArrow()].transform.position = firePoint.position;// khi bắn phải đặt mũi tên trong firepoint
        arrows[FindArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();//hướng của đường đạn 
    }
    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)// nếu thời gian hồi chiêu lến hơn hồi chiêu (của người chơi )thì tấn công
            Attack();
    }
}