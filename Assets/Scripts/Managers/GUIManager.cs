using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class GUIManager : MonoBehaviour
    {

        private TaskManager _taskManager;
        
        [Header("TaskPanel")] [SerializeField] 
        private Button okButton;

        [Header("InventoryPanel")] [SerializeField]
        private GameObject InventoryPanel;
        private bool isOpen;

        private void Awake()
        {
            Application.targetFrameRate = 60;
            _taskManager = FindObjectOfType<TaskManager>();
        }

        public void TaskCompleted(int index)
        {
            _taskManager.CompleteReward(index);
        }

        public void InventoryButton()
        {
            isOpen = !isOpen;
            InventoryPanel.SetActive(isOpen);
        }
    }
}
