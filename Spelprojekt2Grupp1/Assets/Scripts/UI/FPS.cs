using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    [SerializeField] Text myText;
    Queue<float> myQueue = new Queue<float>();


    void Update()
    {
        myQueue.Enqueue(1 / Time.deltaTime);

        if (myQueue.Count > 60)
        {
            myQueue.Dequeue();
        }

        myText.text = ((int)(myQueue.Sum() / myQueue.Count())).ToString() + "\t";
    }
}
