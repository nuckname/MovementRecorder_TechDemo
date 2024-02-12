using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 direction;

    private GunData gunData;

    void Start()
    {
        //direction = transform.forward;

    }

    void Update()
    {
        //if(gameObject.transform == gunData.maxDistance)
        Move();
    }

    void Move()
    {
        //Vector3 direction = Camera.main.transform.forward;

        //transform.position += direction * speed * Time.deltaTime;

    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }
}
