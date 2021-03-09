using System.Collections.Generic;
using UnityEngine;

namespace CYM
{
    #region HashManager
    [Unobfus]
    public class HashManager<K, T> where T : IBase
    {
        T defaultVal = default(T);
        public T this[K id]
        {
            get
            {
                return Data[id];
            }
        }

        Dictionary<K, T> _data = new Dictionary<K, T>();

        public Dictionary<K, T> Data
        {
            get { return _data; }
            set { _data = value; }
        }
        public virtual bool Contains(K id)
        {
            return Data.ContainsKey(id);
        }

        public virtual void Add(K id, T ent)
        {
            if (Data.ContainsKey(id))
            {
                Debug.LogError("重复的id:" + id);
                return;
            }
            Data.Add(id, ent);
        }

        public virtual void Remove(K id)
        {
            Data.Remove(id);
        }
        public virtual T Get(K id)
        {
            T temp = defaultVal;
            if (id == null)
                return temp;
            Data.TryGetValue(id, out temp);
            return temp;
        }

        public int Count()
        {
            return Data.Count;
        }

        public bool IsEmpty()
        {
            return Data.Count == 0;
        }

        public void Clear()
        {
            Data.Clear();
        }
    }
    #endregion

}
