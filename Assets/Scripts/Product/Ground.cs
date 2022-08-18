using System;
using UnityEngine;

namespace Product
{
    public class Ground : ItemBase
    {
        public bool HasSet => _set;

        [SerializeField]private bool _set;

        public void Set(bool val)
        {
            _set = val;
            GetComponent<Collider2D>().enabled = !_set;
        }
    }
}
