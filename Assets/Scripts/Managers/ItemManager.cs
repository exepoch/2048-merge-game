using System;
using System.Collections;
using System.Collections.Generic;
using Product;
using Scriptable;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class ItemManager : MonoBehaviour
    {
        public static ItemManager Instance;
        public static Action OnIncreaseCapacity;
        public static OnMerge OnMergedAppliance;
        public delegate void OnMerge(Appliance type);

        [SerializeField] private List<ItemData> datas;
        [SerializeField] private GameObject procuder;
        [SerializeField] private GameObject appliance;

        private Array typesArray;

        private readonly Dictionary<ApplianceType, ItemData> _dataDictionary = new Dictionary<ApplianceType, ItemData>();

        private void Awake()
        {
            Instance = this;
            
            foreach (var data in datas)
            {
                _dataDictionary.Add(data.type, data);
            }
            
            typesArray = Enum.GetValues(typeof(ApplianceType));

            StartCoroutine(CapacityIncreator());
        }

        public GameObject GetItem(ApplianceType typeRequest)
        {
            var tileItem = Instantiate(appliance).GetComponent<ItemBase>().Init(
                typeRequest,
                _dataDictionary[typeRequest].color,
                _dataDictionary[typeRequest].guiText
            );

            return tileItem;
        }
        
        public void CreateProducerRandomly()
        {
            
            var randomLoc = BaseMapManager.Instance.GetRandomEmptyCell();
            if(randomLoc == null)
                return;
            
            var producer = Instantiate(procuder).GetComponent<ItemBase>();
            producer.transform.position = randomLoc.transform.position;
        }

        public GameObject CreateAppliance(Ground ground, bool random = true, int level = -1)
        {
            var lvl = random ? Random.Range(2, typesArray.Length) : level;
            var ob = typesArray.GetValue(lvl);

            var newTile = GetItem((ApplianceType)ob);
            
            newTile.transform.position = ground.transform.position;
            newTile.GetComponent<Appliance>().SetExternals(ground, lvl);
            ground.Set(true);

            return newTile;
        }

        private IEnumerator CapacityIncreator()
        {
            while (true)
            {
                yield return new WaitForSeconds(30);
                OnIncreaseCapacity?.Invoke();
            }
        }

        public ItemData GetRandomData()
        {
            return datas[Random.Range(2, datas.Count)];
        }
    }
}
