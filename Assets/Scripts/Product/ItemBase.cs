using System;
using Scriptable;
using TMPro;
using UnityEngine;

namespace Product
{
    public class ItemBase : MonoBehaviour
    {
        [SerializeField] protected ApplianceType type;
        protected SpriteRenderer Renderer;
        protected TextMeshPro Gui;
        private Color _defColor;

        protected void Awake()
        {
            Renderer = GetComponent<SpriteRenderer>();
            Gui = GetComponentInChildren<TextMeshPro>();
            _defColor = Renderer.color;
        }

        public GameObject Init(ApplianceType appType, Color appearence, string gui)
        {
            type = appType;
            _defColor = appearence;

            Renderer = GetComponent<SpriteRenderer>();
            Gui = GetComponentInChildren<TextMeshPro>();

            Gui.text = gui;
            Renderer.color = appearence;
            return gameObject;
        }

        public ApplianceType GetApplianceType()
        {
            return type;
        }

        public void ChangeAppearence(Color newColor)
        {
            Renderer.color = newColor;
        }

        public void ReturnDefaultColor()
        {
            Renderer.color = _defColor;
        }
    }
}
