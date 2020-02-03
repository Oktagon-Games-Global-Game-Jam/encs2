using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOFollower : MonoBehaviour
{
    [SerializeField]
    public GameObject m_GoFollower;
    [SerializeField]
    public GameObject m_GoToFollow;
    public Canvas canvas;


    // Update is called once per frame
    void Update()
    {
        /*
        //m_GoFollower.transform.position = m_GoToFollow.transform.position;
        Vector2 pos = m_GoToFollow.transform.position;  // get the game object position
        Vector2 viewportPoint = Camera.main.WorldToViewportPoint(pos);  //convert game object position to VievportPoint

        // set MIN and MAX Anchor values(positions) to the same position (ViewportPoint)
        ((RectTransform)m_GoFollower.transform).anchorMin = viewportPoint;
        ((RectTransform)m_GoFollower.transform).anchorMax = viewportPoint;*/


        //this is your object that you want to have the UI element hovering over
        //GameObject WorldObject;

        //this is the ui element
        RectTransform UI_Element;

        //first you need the RectTransform component of your canvas
        RectTransform CanvasRect = canvas.GetComponent<RectTransform>();

        //then you calculate the position of the UI element
        //0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.

        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(m_GoToFollow.transform.position);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

        //now you can set the position of the ui element
        ((RectTransform)m_GoFollower.transform).anchoredPosition = WorldObject_ScreenPosition;
    }
}
