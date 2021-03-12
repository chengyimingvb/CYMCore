//------------------------------------------------------------------------------
// DicList.cs
// Copyright 2019 2019/1/18 
// Created by CYM on 2019/1/18
// Owner: CYM
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace CYM
{
    public interface IListDictionary
    {
        int Add(object key,object value);
    }
    [Serializable]
    [Unobfus]
    public class ListDictionary<TKey,TVal> : Dictionary<TKey,TVal>, IListDictionary
    {
        public List<TVal> ListData { get; private set; } = new List<TVal>();

        public int Add(object key, object value)
        {
            Add((TKey)key,(TVal)value);
            return 1;
        }
        public new void Add(TKey key,TVal ent)
        {
            if (ContainsKey(key)) return;
            if (ent == null)
            {
                throw new NotImplementedException("ListDictionary.Add:ent 为 null!!!");
            }
            ListData.Add(ent);
            base.Add(key,ent);
        }
        public new void Remove(TKey key)
        {
            if (!ContainsKey(key)) return;
            ListData.Remove(this[key]);
            base.Remove(key);
        }
        public new void Clear()
        {
            base.Clear();
            ListData.Clear();
        }
    }
}