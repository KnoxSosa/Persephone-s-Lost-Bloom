using UnityEngine;
using UnityEngine.EventSystems;

public class IndicatorFollower : MonoBehaviour
{
    public RectTransform indicator; // L’indicateur visuel (flèche)
    public Vector2 defaultOffset = new Vector2(-50f, 0f); // Valeur par défaut si pas d'override

    void Update()
    {
        GameObject selected = EventSystem.current.currentSelectedGameObject;

        if (selected != null)
        {
            RectTransform target = selected.GetComponent<RectTransform>();
            if (target != null)
            {
                Vector2 offset = defaultOffset;

                // On vérifie si ce bouton a un offset personnalisé
                IndicatorOffset customOffset = selected.GetComponent<IndicatorOffset>();
                if (customOffset != null)
                {
                    offset = customOffset.offset;
                }

                indicator.position = target.position + (Vector3)offset;

                if (!indicator.gameObject.activeSelf)
                    indicator.gameObject.SetActive(true);
            }
        }
        else
        {
            if (indicator.gameObject.activeSelf)
                indicator.gameObject.SetActive(false);
        }
    }
}
