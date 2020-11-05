using TMPro;
using UnityEngine;
using UnityGameSystems.Persistent;
using Zenject;

namespace UnityGameSystems.Display
{
    public class BalanceDisplay : MonoBehaviour
    {
#pragma warning disable 0649
        [Inject] protected IBalanceManager BalanceManager;
#pragma warning restore 0649
        
        protected TextMeshProUGUI BalanceText;

        protected virtual void Awake()
        {
            BalanceText = GetComponentInChildren<TextMeshProUGUI>();
            BaseBalanceManager.OnBalanceChange += SetText;
        }

        protected virtual void OnDestroy()
        {
            BaseBalanceManager.OnBalanceChange -= SetText;
        }

        protected virtual void Start() => SetText(BalanceManager.Balance);

        protected virtual void SetText(int balance) => BalanceText.text = balance.ToString();
    }
}