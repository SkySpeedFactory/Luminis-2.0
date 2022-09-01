using UnityEngine;

public class EnemyHit : ShootableObjects
{
    public GameObject particlesPrefab;

    public override void OnHit(RaycastHit hit, float damage)    {
        
        GameObject particles = Instantiate(particlesPrefab, hit.point, hit.transform.rotation);
        hit.collider.gameObject.GetComponent<AvatarController>().CalculateDamage(damage);
        Destroy(particles,0.8f);
        print("Enemy");
    }
    
}
