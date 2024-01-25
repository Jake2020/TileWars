using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColourPicker : MonoBehaviour, IPointerClickHandler
{
    //Fields
    public Color output;

    [SerializeField]
    private string team;

    //Methods
    public void OnPointerClick(PointerEventData eventData) {
        output = Pick(Camera.main.WorldToScreenPoint(eventData.position), GetComponent<Image>());
        output.a = 1.0f;
        if (team == "team1") {
            FindObjectOfType<SettingsBoard>().ColorResultTeam1.color = output;
        } else {
            FindObjectOfType<SettingsBoard>().ColorResultTeam2.color = output;
        }
    }
    
    private Color Pick(Vector2 screenPoint, Image imageToPick) {
        //Debug.Log("Screen Point: " + screenPoint);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(imageToPick.rectTransform, screenPoint, Camera.main, out Vector2 localPoint);
        //Debug.Log("Local Point: " + localPoint);

        localPoint += imageToPick.rectTransform.sizeDelta / 2;
        //Debug.Log("Adjusted Local Point: " + localPoint);

        Texture2D texture = imageToPick.sprite.texture;
        Vector2Int texturePoint = new Vector2Int(
            Mathf.Clamp((int)(texture.width * (localPoint.x / imageToPick.rectTransform.sizeDelta.x)), 0, texture.width - 1),
            Mathf.Clamp((int)(texture.height * (localPoint.y / imageToPick.rectTransform.sizeDelta.y)), 0, texture.height - 1)
        );

        Debug.Log("Texture Point: " + texturePoint);

        return texture.GetPixel(texturePoint.x, texturePoint.y);
    }

}
