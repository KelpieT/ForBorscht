using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private float speed;
    protected float damage;
    private float lifeTime = 10f;
    private float timer;

    public void Init(float speed, float damage)
    {

        this.speed = speed;
        this.damage = damage;
    }

    private void FixedUpdate()
    {
        Move();
        timer += Time.fixedDeltaTime;
        if (timer > lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(ConstParams.TAG_ENEMY))
        {
            ColideWithEnemy(other);
            return;
        }
        else if (other.gameObject.CompareTag(ConstParams.TAG_WALL))
        {
            ColideWithWall(other);
            return;
        }
    }

    private void ColideWithWall(Collider other)
    {
        Destroy(gameObject);
    }

    private void ColideWithEnemy(Collider other)
    {
        IDamageble damageble;
        if (other.TryGetComponent<IDamageble>(out damageble))
        {
            damageble.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        // Vector3 moveStep = transform.forward * speed * Time.deltaTime;
        rb.velocity = transform.forward * speed;
    }

}
