using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColourPicker : MonoBehaviour, IPointerClickHandler
{
    //Fields
    public Color output;

    //Methods
    public void OnPointerClick(PointerEventData eventData) {
        output = Pick(Camera.main.WorldToScreenPoint(eventData.position), GetComponent<Image>());
        output.a = 1.0f;
        FindObjectOfType<SettingsBoard>().ColourResult.color = output;
    }
    
    private Color Pick(Vector2 screenPoint, Image imageToPick) {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(imageToPick.rectTransform, screenPoint, Camera.main, out Vector2 point);
        point += imageToPick.rectTransform.sizeDelta / 2;
        Texture2D t = GetComponent<Image>().sprite.texture;
        Vector2Int m_point = new((int)((t.width * point.x) / imageToPick.rectTransform.sizeDelta.x), (int)((t.height * point.y) / imageToPick.rectTransform.sizeDelta.y));
        return t.GetPixel(m_point.x , m_point.y );
    }
}
