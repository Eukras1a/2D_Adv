using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("����")]
    public int damage;
    public float attackRange;
    public float attackRate;


    //TODO:�ĳ�enter��exit
    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.GetComponent<Character>()?.TakeDamage(this);
    }
}
