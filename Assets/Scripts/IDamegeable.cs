using UnityEngine;

public interface IDamegeable
{
    void OnDamege(float damage, Vector3 hitPoint, Vector3 hitNormal);
}
