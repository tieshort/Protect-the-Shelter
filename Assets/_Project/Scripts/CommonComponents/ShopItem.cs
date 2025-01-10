using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "ShopItem", menuName = "ScriptableObject/Shop Item")]
public class ShopItem : ScriptableObject
{
    public int Price;
    public LocalizedString ItemName;
    public GameObject Item;
    public GameObject AvailableItem;
    public GameObject UnavailableItem;
}
