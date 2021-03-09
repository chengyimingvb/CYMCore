using System.Collections.Generic;
/// <summary>
/// 访问更加安全的Dictionary
/// </summary>
namespace CYM
{
    public class SafeDic<T,V> : Dictionary<T,V>
    {
        public new V this[T key]
        {
            get
            {
                if (ContainsKey(key))
                {
                    return base[key];
                }
                return default;
            }
            set
            {
                if (ContainsKey(key))
                {
                    base[key] = value;
                }
                else
                {
                    Add(key, value);
                }
            }
        }
    }
}
