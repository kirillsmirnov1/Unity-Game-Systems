using System;
using UnityEngine;

namespace UnityGameSystems.Persistent
{
    public interface IBalanceManager
    {
        int Balance { get; }
        void ChangeBalance(int value);
    }

    public abstract class BaseBalanceManager : MonoBehaviour, IBalanceManager
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