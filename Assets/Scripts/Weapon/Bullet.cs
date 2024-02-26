using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 direction;

    [SerializeField]
    private GhostRecorder ghostRecorder;

    private GhostData newGhost;

    private GunData gunData;

    void Start()
    {
        //ghostRecorder.CreateNewGhost();
        ghostRecorder.CreateNewGhost();

        ghostRecorder.isRecording = true;
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
    private void OnCollisionEnter(Collision collision)
    {
        ghostRecorder.isRecording = false;
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }
}
