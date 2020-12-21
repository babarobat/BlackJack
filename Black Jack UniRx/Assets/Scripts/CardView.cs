using UnityEngine;
using UnityEngine.UI;

public class CardView:MonoBehaviour
{
    [SerializeField]
    private Image m_Image;

    public void SetImage(Sprite sprite)
    {
        m_Image.sprite = sprite;
    }
}