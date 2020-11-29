using System;
using Luna.Unit;
using UnityEngine;

namespace Luna.WeaponEffects
{
    [RequireComponent(typeof(Collider2D))]
    public class OnTriggerEnterApplyEffects : MonoBehaviour
    {
        [SerializeField] private WeaponEffect[] effects;

        private UnitTurnController _turnController;

        private void Awake()
        {
            _turnController = FindObjectOfType<UnitTurnController>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_turnController)
            {
                Debug.LogError($"{nameof(OnTriggerEnterApplyEffects)} on {name} couldn't find UnitTurnController");
                return;
            }

            foreach (var effect in effects)
            {
                var actions = effect.Apply(other.gameObject, gameObject);

                _turnController.AddActionsToCurrentUnit(actions);
            }
        }
    }
}