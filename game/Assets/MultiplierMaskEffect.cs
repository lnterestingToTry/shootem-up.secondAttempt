using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplierMaskEffect : MonoBehaviour
{
    public GameObject mask;
    public RectTransform mask_transform, text_transform;

    // Start is called before the first frame update
    void Start()
    {
        text_transform = GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update()
    {
        mask_transform.anchoredPosition = new Vector2(mask_transform.anchoredPosition[0], mask_transform.anchoredPosition[1] + 0.1f);
        text_transform.anchoredPosition = new Vector2(text_transform.anchoredPosition[0], text_transform.anchoredPosition[1] - 0.1f);
    }
}
