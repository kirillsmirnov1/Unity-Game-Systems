using System;
using UnityEngine;

namespace UnityGameSystems.Persistent
{
    public abstract class BaseBalanceManager : MonoBehaviour
    {
        public static event Action<int> OnBalanceChange;

        private int _balance;

        public int Balance
        {
            get => _balance;
            protected set
            {
                if(_balance == value) return;
                _balance = value;
                OnBalanceChange?.Invoke(_balance);
            }
        }

        protected virtual void Awake() => BindCallbacks();
        protected virtual void OnDestroy() => UnBindCallbacks();

        protected abstract void BindCallbacks();
        protected abstract void UnBindCallbacks();

        public void ChangeBalance(int value) => Balance += value;
    }
}