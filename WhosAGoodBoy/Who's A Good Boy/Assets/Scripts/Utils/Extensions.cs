using System;
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


    public static void Raise(this EventHandler handler, object sender, EventArgs args)
    {
        if (handler != null)
        {
            handler(sender, args);
        }
    }

    public static void Raise<T>(this EventHandler<T> handler, object sender, T args) where T: EventArgs
    {
        if (handler != null)
        {
            handler(sender, args);
        }
    }
}
