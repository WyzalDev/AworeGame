using System;
using System.Collections.Generic;
using System.Linq;

namespace _AworeGame.Scripts.Data.ResourceSystem
{
    public class PotentialResource
    {
        public Action PotentialResourceChanged;
        
        public ResourceType Type { get; set; }

        private readonly List<PotentialResourceExpressionMember> _terms;
        private readonly List<PotentialResourceExpressionMember> _modifiers;

        public PotentialResource(ResourceType type)
        {
            Type = type;
            _terms = new List<PotentialResourceExpressionMember>();
            _modifiers = new List<PotentialResourceExpressionMember>();
        }
        
        public void InvokePotentialResourceChangedAction() => PotentialResourceChanged?.Invoke();

        public void ChangeTerm(PotentialResourceSource source, float value)
        {
            var term = _terms.Find(x => x.Source == source);

            if (term == null)
            {
                term = new PotentialResourceExpressionMember(source, value);
                _terms.Add(term);
            }
            else
                term.ExpressionMember = value;
        }

        public void ChangeModifier(PotentialResourceSource source, float value)
        {
            var modifier = _modifiers.Find(x => x.Source == source);
            
            if (modifier == null)
            {
                modifier = new PotentialResourceExpressionMember(source, value);
                _modifiers.Add(modifier);
            }
            else
                modifier.ExpressionMember = value;
        }

        public float GetIncome()
        {
            var amount = _terms.Sum(x => x.ExpressionMember);
            var modifier = _modifiers.Sum(x => x.ExpressionMember);
            
            return amount * modifier;
        }

        public void Reset()
        {
            _terms.Clear();
            _modifiers.Clear();
        }
    }
}