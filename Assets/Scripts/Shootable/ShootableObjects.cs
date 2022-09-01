using UnityEngine;

public abstract class ShootableObjects : MonoBehaviour
{
    public abstract void OnHit(RaycastHit hit, float damage);
}
