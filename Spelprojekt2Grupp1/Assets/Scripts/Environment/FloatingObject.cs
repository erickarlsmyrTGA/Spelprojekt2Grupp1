using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    [SerializeField] public float myCycleTime = 1.0f;
    [SerializeField] public float myAmplitude = 0.2f;
    [SerializeField] public AnimationCurve myAnimationCurve;

    private Vector3 myOriginalPos;
    private float myTimeCounter = 0f;    
    private int myTimeSign = 1;

    // Start is called before the first frame update
    void Start()
    {
        myOriginalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        myTimeCounter += Time.deltaTime * myTimeSign;
        if (myTimeCounter > myCycleTime || myTimeCounter < 0)
        {
            myTimeSign = -myTimeSign; // switch direction
        }
                
        float lerpRatio = myTimeCounter / myCycleTime;            // x value of animation curve
        float curveFactor = myAnimationCurve.Evaluate(lerpRatio); // y value of animation curve
        var offset =  new Vector3(0.0f, curveFactor*myAmplitude, 0.0f);
        transform.position = myOriginalPos + offset;
    }
}
