using System;
using System.Collections.Generic;
using Product;
using Scriptable;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class TaskManager : MonoBehaviour
    {
        [SerializeField] private List<Task> tasks;

        private ApplianceType lastAddedTask;
        
        private void Start()
        {
            GetNewTask();

            ItemManager.OnMergedAppliance += MergedListener;
        }

        private void GetNewTask()
        {
            foreach (Task task in tasks)
            {
                if(!task.isCompleted) 
                    continue;
                
                var data = ItemManager.Instance.GetRandomData();
                while (data.type == lastAddedTask)
                {
                    data = ItemManager.Instance.GetRandomData();
                }
                
                task.targetType = data.type;
                task.image.color = data.color;
                task.text.text = data.guiText;
                task.button.color = Color.white;
                task.canGetReward = false;
                task.isCompleted = false;
                lastAddedTask = data.type;
            }
        }

        private void MergedListener(Appliance appliance)
        {
            foreach (Task task in tasks)
            {
                if (task.targetType == appliance.GetApplianceType())
                {
                    task.button.color = Color.green;
                    appliance.ChangeAppearence(Color.green);
                    task.mergedFrom = appliance;
                    task.canGetReward = true;
                }
            }
        }

        public void CompleteReward(int index)
        {
            print(tasks[index].image.name);
            if (tasks[index].canGetReward)
            {
                tasks[index].button.color = Color.white;
                tasks[index].isCompleted = true;
                
                if(tasks[index].mergedFrom != null)
                    tasks[index].mergedFrom.ReturnDefaultColor();
                
                GetNewTask();
            }
        }
        
        [Serializable]
        public class Task
        {
            public ApplianceType targetType;
            public Image image;
            public TextMeshProUGUI text;
            public Image button;
            public bool isCompleted;
            public bool canGetReward;
            public Appliance mergedFrom;
        }
    }
}
