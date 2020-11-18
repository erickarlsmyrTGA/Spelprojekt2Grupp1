using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement
{
    public IEnumerator MoveInDirection(Transform aTransform, Vector3 aDirection, float aSpeed)
    {
        Vector3 position = aTransform.position;
        Vector3 target = position + aDirection;
        float divider = Mathf.Abs(Vector3.Distance(position, target));

        float percentage = 0.0f;
        while (percentage < 1.0f)
        {
            aTransform.position = Vector3.Lerp(position, target, percentage);
            percentage += Time.deltaTime * aSpeed / divider;
            yield return null;
        }
        aTransform.position = target;
    }
}
