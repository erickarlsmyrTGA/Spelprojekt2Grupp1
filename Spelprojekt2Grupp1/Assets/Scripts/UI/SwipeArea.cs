using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeArea : MonoBehaviour
{
    [SerializeField] RectTransform myRect;
    // Start is called before the first frame update
    void Start()
    {
        float area = InputManager.ourInstance.TGAGetSwipeThreashold();

        myRect.anchorMax = new Vector2(1, 1 - area);
        myRect.offsetMin = new Vector2(0, 0); // new Vector2(left, bottom); 
        myRect.offsetMax = new Vector2(0, 0); // new Vector2(-right, -top)
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
