using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    [SerializeField]protected FSM _fsm;   //创建状态机
    protected Rigidbody2D _rigidbody2D;
    protected Transform _transform;
    private float flipEpsilon = 0.1f;
    
    public bool TakeDamage(int damage)
    {
        int damageHealth = health - damage;
        if (damageHealth > 0)
        {
            health = damageHealth;
            return false;
        }
        else
        {
            return true;
        }
    }

    protected void InitializeEnemy()
    {
        _transform = GetComponent<Transform>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Flip()
    {
        if (_rigidbody2D.velocity.x < -flipEpsilon)
        {
            _transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (_rigidbody2D.velocity.x > flipEpsilon)
        {
            _transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullets"))
        {
            FlashColor();
            if (TakeDamage(WeaponSlot.Instance.currentWeapon.GetComponent<Weapon>().damageValue))
            { ;
                EffectsManager.Instance.PlayExplosion(this.transform.position);
                Destroy(gameObject);
            }
        }
    }

    private void FlashColor()
    {
        this.GetComponent<SpriteRenderer>().color = Color.red;
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        yield return new WaitForSeconds(flipEpsilon);
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
