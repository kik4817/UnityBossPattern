using Unity.VisualScripting;
using UnityEngine;

public class DealOnContact : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] private int applyDamage = 5;

    [SerializeField] private ParticleSystem[] contactVFX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // �浹�� ����� �������� �� �� �ִ� ������Ʈ���� �Ѵ�. Idamagable �������̽��� ������ �ʾҴٸ� MushRoom.TakeDamage collision.
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.TryGetComponent<IDamagable>(out var damagable)) // iDamagable�������̽��� ����� ��� Ŭ������ �۵��Ѵ�.
       {
            SetApplyDamage();
            damagable.TakeDamage(applyDamage);

            if (contactVFX != null)
            {
                foreach (var effect in contactVFX)
                {
                    effect.Play();
                }
            }

            //Destroy(gameObject);
       }


    }

    private void SetApplyDamage()
    {
        
    }
    
}
