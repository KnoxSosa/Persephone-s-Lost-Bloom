using UnityEngine;

public class ParallaxScroller : MonoBehaviour
{
    public GameObject cam;
    public float parallaxEffect = 0.5f;

    private float startPos;
    private float backgroundWidth = 24.6666f; // 592px ÷ 24 PPU

    void Start()
    {
        if (cam == null)
            cam = Camera.main.gameObject;

        startPos = transform.position.x;
    }

    void Update()
    {
        float distance = cam.transform.position.x * parallaxEffect;
        float movement = cam.transform.position.x * (1 - parallaxEffect);

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (movement > startPos + backgroundWidth)
        {
            startPos += backgroundWidth;
        }
        else if (movement < startPos - backgroundWidth)
        {
            startPos -= backgroundWidth;
        }
    }
}
