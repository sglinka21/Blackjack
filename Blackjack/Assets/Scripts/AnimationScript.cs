using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    public float TimeDuring = 1f;

    
    private bool isLerp = false;

    
    private Vector3 startPos;
    private Vector3 endPos;

    
    private float timeStarted;

   
    public IEnumerator StartAnimation(Vector3 endPosition)
    {
        if (isLerp)
        {
            yield return null;
        }
        else
        {
            isLerp = true;
            timeStarted = Time.time;

            startPos = transform.position;
            endPos = endPosition;

            float percentageComplete = 0;
            do
            {
                float timeSinceStarted = Time.time - timeStarted;
                percentageComplete = timeSinceStarted / TimeDuring;

                transform.position = Vector3.Lerp(startPos, endPos, percentageComplete);
                yield return null;
            } while (isLerp && percentageComplete < 1.0f);
            isLerp = false;
        }
    }

    public void StopAnimation()
    {
        isLerp = false;
    }

    void fixedUpdate()
    {
        if (isLerp)
        {
            float timeSinceStarted = Time.time - timeStarted;
            float percentageComplete = timeSinceStarted / TimeDuring;

            transform.position = Vector3.Lerp(startPos, endPos, percentageComplete);

            if (percentageComplete >= 1.0f)
            {
                isLerp = false;
            }
        }
    }
}
