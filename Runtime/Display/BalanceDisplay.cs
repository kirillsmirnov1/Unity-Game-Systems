using TMPro;
using UnityEngine;
using UnityGameSystems.Persistent;
using Zenject;

namespace UnityGameSystems.Display
{
    public class BalanceDisplay : MonoBehaviour
    {
#pragma warning disable 0649
        [Inject] private BaseBalanceManager _balanceManager;
#pragma warning restore 0649
        
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
            BaseBalanceManager.OnBalanceChange += SetText;
        }

        private void OnDestroy()
        {
            BaseBalanceManager.OnBalanceChange -= SetText;
        }

        private void Start() => SetText(_balanceManager.Balance);

        private void SetText(int balance) => _text.text = balance.ToString();
    }
}