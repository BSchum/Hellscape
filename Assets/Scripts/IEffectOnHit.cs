using UnityEngine;
using System.Collections;

public interface IEffectOnHit
{
    IEnumerator EffectOnHit(Collider other);
}
