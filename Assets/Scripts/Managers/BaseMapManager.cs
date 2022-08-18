using System.Collections.Generic;
using Product;
using UnityEngine;

namespace Managers
{
    public class BaseMapManager : MonoBehaviour
    {
        public static BaseMapManager Instance;

        private void Awake() => Instance = this;

        [SerializeField] private List<Ground> baseTiles;

        public Ground GetClosest(ItemBase self)
        {
            Ground cl = null;
            var dist = float.MaxValue;
            
            foreach (var tile in baseTiles)
            {
                if(tile.gameObject == self.gameObject || tile.HasSet)
                    continue;

                var newDist = Vector2.Distance(self.transform.position, tile.transform.position);
                
                if (!(newDist < dist)) continue;
                
                dist = newDist;
                cl = tile;
            }

            return cl;
        }

        public Ground GetRandomEmptyCell()
        {
            Ground ground = null;

            while (ground == null)
            {
                var loc = Random.Range(0, baseTiles.Count);
                if (!baseTiles[loc].HasSet)
                    ground = baseTiles[loc];
            }

            return ground;
        }
    }
}
