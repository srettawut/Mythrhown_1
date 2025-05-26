using UnityEngine;
using UnityEngine.EventSystems;

public class SimpleJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform joystickBG;
    public RectTransform joystickHandle;

    public float Horizontal => inputVector.x;
    public float Vertical => inputVector.y;

    private Vector2 inputVector;

    public Vector2 Input => inputVector;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBG, eventData.position, eventData.pressEventCamera, out pos))
        {
            float x = pos.x / (joystickBG.sizeDelta.x / 2);
            float y = pos.y / (joystickBG.sizeDelta.y / 2);
            inputVector = new Vector2(x, y);
            inputVector = (inputVector.magnitude > 1) ? inputVector.normalized : inputVector;

            joystickHandle.anchoredPosition = new Vector2(
                inputVector.x * (joystickBG.sizeDelta.x / 2),
                inputVector.y * (joystickBG.sizeDelta.y / 2)
            );
        }
    }

    public void OnPointerDown(PointerEventData eventData) => OnDrag(eventData);

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        joystickHandle.anchoredPosition = Vector2.zero;
    }
}

