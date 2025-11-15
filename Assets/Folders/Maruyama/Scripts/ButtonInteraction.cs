using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;

[RequireComponent(typeof(Image))]
public class ButtonInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("カラー設定")]
    [SerializeField] Color hoverColor = new Color(1f, 1f, 0.8f);

    [Header("アニメーション設定")]
    [SerializeField] float hoverScale = 1.05f;
    [SerializeField] float hoverDuration = 0.2f;
    [SerializeField] Ease easeType = Ease.OutQuad;

    [Header("ステージ情報")]
    [SerializeField] int stageNumber;
    [SerializeField] int bestMoveCount;

    public static event Action<int, int> OnStageHovered;  // stageNumber, moveCount を通知

    Image _image;
    Color normalColor;
    Vector3 _defaultScale;
    Tweener _colorTween;
    Tweener _scaleTween;

    void Start()
    {
        _image = GetComponent<Image>();
        _defaultScale = transform.localScale;
        normalColor = _image.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Animate(true);
        OnStageHovered?.Invoke(stageNumber, bestMoveCount);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Animate(false);
        OnStageHovered?.Invoke(-1, -1); // UIをリセット
    }

    private void Animate(bool isHover)
    {
        if (_image == null) return;

        _colorTween?.Kill();
        _scaleTween?.Kill();

        if (isHover)
        {
            _colorTween = _image.DOColor(hoverColor, hoverDuration).SetEase(easeType);
            _scaleTween = transform.DOScale(_defaultScale * hoverScale, hoverDuration).SetEase(easeType);
        }
        else
        {
            _colorTween = _image.DOColor(normalColor, hoverDuration).SetEase(easeType);
            _scaleTween = transform.DOScale(_defaultScale, hoverDuration).SetEase(easeType);
        }
    }
}
