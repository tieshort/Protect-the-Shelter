using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem", menuName = "ScriptableObject/Shop Item")]
public class ShopItem : ScriptableObject
{
    public int Price;
    public string ItemName;
    public GameObject Item;
    public GameObject AvailableItem;
    public GameObject UnavailableItem;
}
