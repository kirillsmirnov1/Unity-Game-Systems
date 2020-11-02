using System;
using UnityEngine;
using UnityUtils.Saves;

namespace UnityGameSystems.Persistent
{
    public abstract class BaseSaveManager<T> : MonoBehaviour where T : BaseSave
    {
        /// <summary>
        /// Should be used only where is single SaveManager in project. Otherwise, child save managers must have their own callbacks.
        /// </summary>
        public static event Action<T> OnSaveRead;

#pragma warning disable 0649
        [SerializeField] private bool logSave;
#pragma warning restore 0649
        
        protected T Save;
        private readonly object _lockable = new object();
        protected virtual string Name => "/save";

        protected abstract T DefaultSave { get; }

        protected virtual void Awake() => BindCallbacks();
        protected virtual void Start() => ReadSave();
        protected virtual void OnDestroy() => UnBindCallbacks();

        private void ReadSave()
        {
            Save = SaveIO.ReadObjectAsJsonString<T>(Name, _lockable, logSave) ?? DefaultSave;
            CheckData();
            OnSaveRead?.Invoke(Save);
        }
        
        protected abstract void CheckData();
        
        protected void WriteSave() => SaveIO.WriteObjectAsJsonString(Save, Name, _lockable, logSave);

        protected abstract void BindCallbacks();
        protected abstract void UnBindCallbacks();
    }

    public abstract class BaseSave { }
}