using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SliderDragEvents : MonoBehaviour, IPointerUpHandler
{

    public void OnPointerUp(PointerEventData eventData)
    {
        GameManager.ourInstance.myAudioManager.PlaySFXClip("Pop_Press");
    }
}