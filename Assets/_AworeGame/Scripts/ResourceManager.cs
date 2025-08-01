using System.Collections.Generic;
using _AworeGame.Scripts.Data;
using UnityEngine;

namespace _AworeGame.Scripts
{
    public class ResourceManager : MonoBehaviour
    {
        private static ResourceManager _instance;

        [Header("Start resources settings")]
        [SerializeField] private float _startMoney;
        [SerializeField] private float _startFood;
        [SerializeField] private float _startWeapons;
        [SerializeField] private float _startWool;
        [SerializeField] private float _startPopulation;
        
        private List<Resource> _resources;
        private List<PotentialResource> _potentialResources;

        private void Awake() => _instance = this;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _resources = new List<Resource>()
            {
                new Resource(_startMoney, ResourceType.Money),
                new Resource(_startFood, ResourceType.Food),
                new Resource(_startWeapons, ResourceType.Weapons),
                new Resource(_startWool, ResourceType.Wool),
                new Resource(_startPopulation, ResourceType.Population),
            };
            
            _potentialResources = new List<PotentialResource>()
            {
                new PotentialResource(ResourceType.Money),
                new PotentialResource(ResourceType.Food),
                new PotentialResource(ResourceType.Weapons),
                new PotentialResource(ResourceType.Wool),
                new PotentialResource(ResourceType.Population)
            };
        }

        public static void ChangeResource(ResourceType type, float amount)
        {
            var resource = _instance._resources.Find(x => x.Type == type);

            resource.ChangeAmount(amount);
        }

        public static void ChangePotentialResource(PotentialResourceSource source, ResourceType type, float term, float modifier)
        {
            var potentialResource = _instance._potentialResources.Find(x => x.Type == type);
            
            potentialResource.ChangeTerm(source, term);
            potentialResource.ChangeModifier(source, modifier);
            potentialResource.InvokePotentialResourceChangedAction();
        }

        public static void ApplyPotentialResources()
        {
            for (var i = 0; i > _instance._resources.Count; i++)
            {
                _instance._resources[i].ChangeAmount(_instance._potentialResources[i].GetIncome());
                
                _instance._potentialResources[i].Reset();
                _instance._potentialResources[i].InvokePotentialResourceChangedAction();
            }
        }
        
        public static Resource GetResource(ResourceType type) =>
            _instance._resources.Find(x => x.Type == type);

        public static PotentialResource GetPotentialResource(ResourceType type) =>
            _instance._potentialResources.Find(x => x.Type == type);
    }
}