using UnityEngine;

namespace Scriptable
{
    public enum ApplianceType
    {
        Ground,
        Producer,
        T2,
        T4,
        T8,
        T16,
        T32,
        T64,
        T128,
        T256,
        T512,
        T1024,
        T2048
    }
    
    [CreateAssetMenu(fileName = "Type", menuName = "ScriptableObject/CreateItemType")]
    public class ItemData : ScriptableObject
    {
        public ApplianceType type;
        public Color color;
        public string guiText;
    }
}
