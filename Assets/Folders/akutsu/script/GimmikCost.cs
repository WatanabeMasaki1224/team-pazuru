using UnityEngine;

// 三角につける必要なスクリプトがないため、MonoBehaviourを継承しています。
/// <summary>
/// ギミックがコスト制になった時に参照するクラス
/// </summary>
public class GimmikCost : MonoBehaviour
{
    [SerializeField] int _cost = 1;
    public int Cost => _cost;
}
