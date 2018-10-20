using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions{

    public static Collider2D GetClosest(this Collider2D[] colliders, Vector3 relativeToHere)
    {
        if (colliders.Length < 1)
            return null;
        float minDistance = Mathf.Infinity;
        Collider2D closestCollider = colliders[0];
        foreach (Collider2D col in colliders)
        {
            float distance = Vector2.Distance(relativeToHere, col.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestCollider = col;
            }
        }
        return closestCollider;
    }
}
