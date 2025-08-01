using System;
using UnityEngine;

namespace _AworeGame.Scripts.Data
{
    public class Resource
    {
        public Action ResourceChanged;
        public Action ResourceNegative;
        
        public float Amount { get; private set; }
        
        public ResourceType Type { get;}

        public Resource(float amount, ResourceType type)
        {
            Amount = amount;
            Type = type;
        }

        public void ChangeAmount(float value)
        {
            Amount = Mathf.Clamp(Amount + value, 0, Amount + value);
            InvokeResourceChanged();
        }

        public void InvokeResourceChanged() => ResourceChanged?.Invoke();
    }
}