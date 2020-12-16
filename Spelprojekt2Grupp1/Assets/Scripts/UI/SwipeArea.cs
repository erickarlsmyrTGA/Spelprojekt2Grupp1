using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeArea : MonoBehaviour
{
    [SerializeField] RectTransform myRect;
    [SerializeField] RectTransform mySnowRect;
    [SerializeField] RectTransform myRightArrow;
    [SerializeField] RectTransform myLeftArrow;

    // Start is called before the first frame update
    void Start()
    {
        float area = InputManager.ourInstance.TGAGetSwipeThreashold();
        float tenthOfHeight = (Screen.height * 0.1f) / Screen.width;

        myRect.anchorMax = new Vector2(1, 1 - area);
        myRect.offsetMin = new Vector2(0, 0); // new Vector2(left, bottom); 
        myRect.offsetMax = new Vector2(0, 0); // new Vector2(-right, -top)

        mySnowRect.anchorMax = new Vector2(1, 1);
        mySnowRect.anchorMin = new Vector2(0, 1);
        mySnowRect.offsetMin = new Vector2(0, 0); // new Vector2(left, bottom); 
        mySnowRect.offsetMax = new Vector2(0, mySnowRect.offsetMax.y); // new Vector2(-right, -top)

        myRightArrow.anchorMax = new Vector2(1 - tenthOfHeight / 2, 0.5f);
        myRightArrow.anchorMin = new Vector2(1 - tenthOfHeight / 2, 0.5f);

        myLeftArrow.anchorMax = new Vector2(tenthOfHeight / 2, 0.5f);
        myLeftArrow.anchorMin = new Vector2(tenthOfHeight / 2, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
