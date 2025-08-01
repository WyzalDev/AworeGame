using _AworeGame.Scripts.Data.ResourceSystem;
using DG.Tweening;
using UnityEngine;

namespace _AworeGame.Scripts
{
    public class DayManager : MonoBehaviour
    {
        private static DayManager _instance;

        [SerializeField] private float _dayLengthInSeconds;
        [SerializeField] private float _dayHMAmount = 1440;

        public int DayCounter { get; private set; } = 0;
        
        private float _dayTimer = 0;

        private void Awake() => _instance = this;

        public static void SkipDay()
        {
            var hourLengthInSeconds = _instance._dayLengthInSeconds / 24;
            var hourAmount = hourLengthInSeconds / 24;
            var sequence = DOTween.Sequence();
            
            //TODO Close access for all buildings

            sequence.Append(DOVirtual.Float(0, hourAmount * 4, hourLengthInSeconds * 4,
                value => _instance._dayTimer = value))
                .OnComplete(() =>
                {
                    ResourceManager.ApplyPotentialResources(ResourceType.Food);
                });

            sequence.Append(DOVirtual.Float(
                    hourAmount * 4, hourAmount * 6, hourLengthInSeconds * 2,
                value => _instance._dayTimer = value))
                .OnComplete(() =>
                {
                    ResourceManager.ApplyPotentialResources(ResourceType.Wool);
                });

            sequence.Append(DOVirtual.Float(
                    hourAmount * 6, hourAmount * 8, hourLengthInSeconds * 4,
                    value => _instance._dayTimer = value))
                .OnComplete(() =>
                {
                    ResourceManager.ApplyPotentialResources(ResourceType.Money);
                });
            
            sequence.Append(DOVirtual.Float(
                    hourAmount * 8, hourAmount * 10, hourLengthInSeconds * 4,
                    value => _instance._dayTimer = value))
                .OnComplete(() =>
                {
                    //TODO -1 for trade day counter
                });

            sequence.Append(DOVirtual.Float(
                    hourAmount * 10, hourAmount * 12, hourLengthInSeconds * 2,
                    value => _instance._dayTimer = value))
                .OnComplete(() =>
                {
                    ResourceManager.ApplyPotentialResources(ResourceType.Weapons);
                });

            sequence.Append(DOVirtual.Float(
                hourAmount * 12, hourAmount * 14, hourLengthInSeconds * 2,
                value => _instance._dayTimer = value)).OnComplete(() =>
            {
                ResourceManager.ApplyPotentialResources(ResourceType.Population);
            });

            sequence.Append(DOVirtual.Float(
                    hourAmount * 14, hourAmount * 16, hourLengthInSeconds * 2,
                value => _instance._dayTimer = value))
                .OnComplete(() =>
                {
                    _instance.DayCounter++;
                    //TODO raid counter -1
                });

            sequence.Append(DOVirtual.Float(
                    hourAmount * 16, hourAmount * 17, hourLengthInSeconds,
                value => _instance._dayTimer = value))
                .OnComplete(() =>
                {
                    //TODO if raid begins change UI and music for raid, if raid ends change UI and music for base mode
                });

            sequence.Append(DOVirtual.Float(
                    hourAmount * 17, hourAmount * 22, hourLengthInSeconds * 5,
                value => _instance._dayTimer = value))
                .OnComplete(() =>
                {
                    //TODO add some percent for event dropping
                });

            sequence.Append(DOVirtual.Float(
                    hourAmount * 22, _instance._dayHMAmount, hourLengthInSeconds * 2,
                value => _instance._dayTimer = value))
                .OnComplete(() =>
                {
                    //TODO sound for new day
                    DaySkipEnds();
                });
        }

        private static void DaySkipEnds()
        {
            _instance._dayTimer = 0;
            //TODO Check for win/loose
            //TODO Raid Def check
            //TODO Check and disable trade, apply trade resources changes
            //TODO Check for GameEvent drop, if true drop GameEvent
            //TODO If raid is continue then add debuffs for day
            //TODO Open access for all buildings
        }
    }
}