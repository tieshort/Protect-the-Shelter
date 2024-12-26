using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;
    [SerializeField] private ShopItem item;
    private GameObject flyingObject;
    private Plane groundPlane = new(Vector3.up, Vector3.zero);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    private void Update()
    {
        if (flyingObject != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);

                worldPosition.x = Mathf.Round(worldPosition.x * 2) / 2;
                worldPosition.z = Mathf.Round(worldPosition.z * 2) / 2;

                flyingObject.transform.position = worldPosition;

                if (Input.GetMouseButtonDown(0))
                {
                    Build(flyingObject.transform.position, flyingObject.transform.rotation);
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    Destroy(flyingObject);
                    item = null;
                }
            }
        }
    }

    private void Build(Vector3 pos, Quaternion rot)
    {
        Destroy(flyingObject);
        Instantiate(item.Item, pos, rot);
        GameManager.Instance.PlayerMoney -= item.Price;
        item = null;
    }

    public void SetObjectToBuild(ShopItem _item)
    {
        if (GameManager.Instance.PlayerMoney >= _item.Price)
        {
            if (flyingObject != null)
            {
                Destroy(flyingObject);
            }

            item = _item;
            flyingObject = Instantiate(item.AvailableItem);
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    private BuildManager(){}
}
