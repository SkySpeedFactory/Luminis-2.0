using UnityEngine;

public class Bullet_9mm : MonoBehaviour
{
    private float damage = 2f;
    private float speed;
    private Vector3 startPosition;
    private Vector3 startForward;    

    private bool isInitialized = false;
    private float startTime = -1f;

    public void Initialize(Transform startPoint, float speed)
    {
        this.startPosition = startPoint.position;
        this.startForward = startPoint.forward;
        this.speed = speed;
        isInitialized = true;
        startTime = -1f;
    }

    private Vector3 FindPointInFlight(float time)
    {
        Vector3 movementVector = (startForward * time * speed);
        return startPosition + movementVector;
    }

    private bool CastRayBetweenPoints(Vector3 startPoint, Vector3 endPoint, out RaycastHit hit)
    {
        return Physics.Raycast(startPoint, endPoint - startPoint, out hit, (endPoint - startPoint).magnitude);
    }

    private void OnHit(RaycastHit hit, float damage)
    {
        this.damage = damage;
        ShootableObjects shootableObjects = hit.transform.GetComponent<ShootableObjects>();
        if (shootableObjects)
        {
            shootableObjects.OnHit(hit, damage);            
        }
        Destroy(gameObject, 1);
    }
    private void FixedUpdate()
    {
        if (!isInitialized)
        {
            return;
        }
        if (startTime < 0)
        {
            startTime = Time.time;
        }
        
        RaycastHit hit;
        float currentTime = Time.time - startTime;
        float prevTime = currentTime - Time.fixedDeltaTime;
        float nextTime = currentTime + Time.fixedDeltaTime;

        Vector3 currentPoint = FindPointInFlight(currentTime);              
        Vector3 nextPoint = FindPointInFlight(nextTime);

        if (prevTime > 0)
        {
            Vector3 prevPoint = FindPointInFlight(prevTime);
            if (CastRayBetweenPoints(prevPoint, nextPoint, out hit))
            {
                OnHit(hit, damage);
            }
        }

        if (CastRayBetweenPoints(currentPoint, nextPoint, out hit))
        {
            OnHit(hit, damage);
        }
    }
    private void Update()
    {
        if (!isInitialized || startTime <0)
        {
            return;
        }
        float currentTime = Time.time - startTime;
        Vector3 currentPoint = FindPointInFlight(currentTime);
        transform.position = currentPoint;
    }
}
