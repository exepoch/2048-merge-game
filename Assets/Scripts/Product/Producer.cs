using System;
using Managers;
using UnityEngine;

namespace Product
{
    public class Producer : ItemBase
    {
        [SerializeField] private ItemBase ground;
        [SerializeField] private int capacity = 10;

        private void Start()
        {
            ItemManager.OnIncreaseCapacity += IncreaseCapacity;

            var closestGround = BaseMapManager.Instance.GetClosest(this);

            transform.position = closestGround.transform.position;
            ground = closestGround;
            closestGround.Set(true);
        }

        private void OnMouseUpAsButton()
        {
            if (capacity <= 0)
            {
                ItemManager.Instance.CreateProducerRandomly();
                
                var grd = ground as Ground;
                grd.Set(false);
                Destroy(gameObject);
                
            }
            
            var nearestGrTile = BaseMapManager.Instance.GetClosest(ground);
            if (nearestGrTile == null)
                return;
            
            if (ItemManager.Instance.CreateAppliance(nearestGrTile))
            {
                capacity--;
                if (capacity <= 0)
                {
                    ItemManager.OnIncreaseCapacity -= IncreaseCapacity;
                }
            }
        }

        private void IncreaseCapacity()
        {
            capacity++;
        }

        private void OnDestroy()
        {
            ItemManager.OnIncreaseCapacity -= IncreaseCapacity;
        }
    }
}