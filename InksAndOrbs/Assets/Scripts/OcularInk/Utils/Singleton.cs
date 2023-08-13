/**
 *
 * Boilerplate for making a Unity GameObject a singleton
 * 2019 - Bart van de Sande / Nonline
 *
 */

using UnityEngine;

namespace OcularInk.Utils
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T _instance;

        /// <summary>
        /// Returns if there is an instance of this
        /// </summary>
        public static bool HasInstance => _instance != null;

        /// <summary>
        /// Access the singleton instance, will create one if it doesn't exist yet
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                    new GameObject(typeof(T).Name).AddComponent<T>();
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance != null)
            {
                Destroy(this.gameObject);
                return;
            }

            _instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
                _instance = null;
        }
    }
}