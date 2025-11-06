using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

/// <summary>
/// ホバー時に色とスケールをDOTweenでアニメーション
/// </summary>
[RequireComponent(typeof(Image))]
public class ButtonInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("カラー設定")]
    [SerializeField] Color hoverColor = new Color(1f, 1f, 0.8f);

    [Header("アニメーション設定")]
    [SerializeField] float hoverScale = 1.05f;
    [SerializeField] float hoverDuration = 0.2f;
    [SerializeField] Ease easeType = Ease.OutQuad;

    Image _image;
    Color normalColor;
    Vector3 _defaultScale;
    Tweener _colorTween;
    Tweener _scaleTween;

    void Awake()
    {
        _image = GetComponent<Image>();
        _defaultScale = transform.localScale;
        normalColor = _image.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_image == null) return;

        _colorTween?.Kill();
        _colorTween = _image.DOColor(hoverColor, hoverDuration).SetEase(easeType);

        _scaleTween?.Kill();
        _scaleTween = transform.DOScale(_defaultScale * hoverScale, hoverDuration).SetEase(easeType);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_image == null) return;

        _colorTween?.Kill();
        _colorTween = _image.DOColor(normalColor, hoverDuration).SetEase(easeType);

        _scaleTween?.Kill();
        _scaleTween = transform.DOScale(_defaultScale, hoverDuration).SetEase(easeType);
    }
}
