using UnityEngine;
using DG.Tweening;
using TMPro;

public class TextFlow : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] Color _color;
    [SerializeField] float duration;

    void Start()
    {
        if (_text == null)
        {
            Debug.LogError("TextFlow: _text Ç™ê›íËÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒÅB");
            return;
        }

        _text.DOColor(_color, duration)
             .SetLoops(-1, LoopType.Yoyo);
    }
}
