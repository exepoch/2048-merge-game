using Managers;
using Scriptable;
using UnityEngine;
using Object = System.Object;

namespace Product
{
    public class Appliance : ItemBase
    {
        private Transform _transform;
        private Vector3 oldPos;
        private Camera _cam;
        private Object collided;
        private ItemBase _tile;
        private Ground _holderGround;
        private bool isDragging;
        private int level;
        
            
        private void Awake()
        {
            base.Awake();
            _cam = Camera.main;
            _transform = transform;
            oldPos = _transform.position;
        }

        private void OnMouseDown()
        {
            if (type == ApplianceType.T2048)
            {   
                _holderGround.Set(false);
                Destroy(gameObject);
                return;
            }

            oldPos = _transform.position;
            Renderer.sortingOrder += 1;
            isDragging = true;
        }

        private void OnMouseDrag()
        {
            var mPos = _cam.ScreenToWorldPoint(Input.mousePosition);
            mPos.z = 0;
            _transform.position = mPos;
        }

        private void OnMouseUp()
        {
            isDragging = false;
            Renderer.sortingOrder -= 1;

            if (_tile == null)
            {
                TurnBackOldPosition();
                return;
            }

            var checkType = _tile.GetApplianceType();

            if (checkType == ApplianceType.Ground)
            {
                _holderGround.Set(false); //Make old ground fillable again.

                _holderGround = (Ground)collided;
                transform.position = _holderGround.gameObject.transform.position;
                _holderGround.Set(true);
            }
            else if(checkType == type)
            {
                var applcn = (Appliance)collided;
                
                //Release just dragged holder
                _holderGround.Set(false);
                
                var newTile = ItemManager.Instance.CreateAppliance(applcn._holderGround, false, level+1);
                
                ItemManager.OnMergedAppliance?.Invoke(newTile.GetComponent<Appliance>());
                
                Destroy(applcn.gameObject);
                Destroy(gameObject);
            }
            else
            {
                TurnBackOldPosition();
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(_tile!= null)
                _tile.ReturnDefaultColor(); //Stop double collide visual

            collided = col.gameObject.GetComponent<ItemBase>();
            _tile = (ItemBase)collided;
            _tile.ChangeAppearence(Color.red);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _tile = null;
            other.GetComponent<ItemBase>().ReturnDefaultColor();
        }

        private void TurnBackOldPosition()
        {
            transform.position = oldPos;
        }

        public void SetExternals(Ground grnd, int lvl)
        {
            _holderGround = grnd;
            level = lvl;
        }
    }
}