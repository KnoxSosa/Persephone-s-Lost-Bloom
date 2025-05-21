using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class HoverEffectWithIndicator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text uiText;
    public TextMeshProUGUI tmpText;
    public Color normalColor = Color.white;
    public Color hoverColor = Color.yellow;

    private bool isSelected = false;

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            if (!isSelected)
            {
                ApplyHoverEffect();
                isSelected = true;
            }
        }
        else
        {
            if (isSelected)
            {
                RemoveHoverEffect();
                isSelected = false;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ApplyHoverEffect();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        RemoveHoverEffect();
    }

    private void ApplyHoverEffect()
    {
        if (uiText != null) uiText.color = hoverColor;
        if (tmpText != null) tmpText.color = hoverColor;
    }

    private void RemoveHoverEffect()
    {
        if (uiText != null) uiText.color = normalColor;
        if (tmpText != null) tmpText.color = normalColor;
    }
}
