using System;
using System.Collections;
using UnityEditor.UI;
using UnityEngine;

public class MushroomAttack1 : ActionBehavior
{
    Transform target;
    Animator animator;

    [SerializeField] float waitTimeForCharging = 1f; // ���� �ð�
    [SerializeField] GameObject projectilePrefab; // ����ü
    [SerializeField] float projectileRange = 180; // ����ü�� �߻�� ����
    [SerializeField] int loopCount = 2; // ���� �ݺ� Ƚ��    
    [SerializeField] float RightAngle = -60f;
    [SerializeField] float LeftAngle = 150;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void OnStart()
    {
        StartCoroutine(ChargingPattern());
    }

    public override void OnUpdate()
    {
        // �÷��̾��� ���� ��ġ �������� flip �ϴ� �ڵ�
        if (transform.position.x < target.position.x)
        {
            spriteRenderer.flipX = true;
        }

        else
        {
            spriteRenderer.flipX = false;
        }
    }
    public override void OnEnd()
    {
        // ������ ������ �� �ʱ�ȭ �ؾ��ϴ� �ڵ尡 �ִٸ� ���⼭ �����Ѵ�.
    }

    IEnumerator ChargingPattern()
    {
        // �⸦ ������
        // �� ������ ���ϸ��̼� ���� -> �ִ��� ����ϰ� ���� �غ� ��
        animator.SetTrigger("AI");

        yield return new WaitForSeconds(waitTimeForCharging);

        for (int i = 0; i < loopCount; i++)
        {
            Fire();
            yield return new WaitForSeconds(1f);
        }

        animator.SetTrigger("Stun");
        yield return new WaitForSeconds(1f);

        IsPatternEnd = true;
    }

    private void Fire()
    {
        // �� ��ġ�� TargetLocation ��ġ�� ���ؼ� flip
        // �� ��ġ�� Target ���ؼ� ����, ������ ����

        float currenAngle = SelectAngleByPlayerPosition(); 

        float deltaAngle = projectileRange / 10;

        for(int i =0; i < 10; i++) // 10 = numberofprojectils
        {
            GameObject projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            // �ﰢ�Լ��� �̿��ؼ� x,y ��ǥ�� �˸� �ش� ������ ���� �� �ִ�.
            float dirX = Mathf.Cos(currenAngle * Mathf.Deg2Rad);
            float dirY = Mathf.Sin(currenAngle * Mathf.Deg2Rad);

            Vector2 Spawn = new Vector2(transform.position.x + dirX, transform.position.y + dirY);

            Vector2 dir = (Spawn - (Vector2)transform.position).normalized;

            if (projectileInstance.TryGetComponent<Rigidbody2D>(out var rb))
            {
                rb.linearVelocity = dir * 5f;
            }

            currenAngle += deltaAngle;
        }
    }

    private float SelectAngleByPlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (transform.position.x < player.transform.position.x)
        {
            return RightAngle;
        }

        else
        {
            return LeftAngle;
        }
    }
}
