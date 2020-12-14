using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    [SerializeField] Text myText;
    Queue<int> myQueue = new Queue<int>();


    void Update()
    {
        myQueue.Enqueue((int)(1 / Time.deltaTime));

        if (myQueue.Count > 144)
        {
            myQueue.Dequeue();
        }

        myText.text = (myQueue.Sum()/myQueue.Count()).ToString() + "\t";
    }
}
