//------------------------------------------------------------------------------
// HashList.cs
// Copyright 2018 2018/6/9 
// Created by CYM on 2018/6/9
// Owner: CYM
// 填写类的描述...
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CYM
{
    public interface IHashList
    {
        int Add(object value);
    }
    [Unobfus]
    [Serializable]
    public class HashList<T> : List<T>, IHashList, IDeserializationCallback
    {
        [NonSerialized]
        protected HashSet<T> Hash;

        public HashList() : base()
        {
            Hash = new HashSet<T>();
        }
        public HashList(List<T> data) : base()
        {
            Hash = new HashSet<T>();
            AddRange(data);
        }

        public T First()
        {
            if (this.Count <= 0)
                return default(T);
            return this[0];
        }
        public void AddRange(List<T> data)
        {
            if (data == null)
                return;
            foreach (var item in data)
            {
                Add(item);
            }
        }

        public new void AddRange(IEnumerable<T> data)
        {
            if (data == null)
                return;
            foreach (var item in data)
            {
                Add(item);
            }
        }
        public new void Add(T ent)
        {
            if (ent == null)
                return;
            if (Hash.Contains(ent))
                return;
            Hash.Add(ent);
            base.Add(ent);
        }
        public new void Remove(T ent)
        {
            Hash.Remove(ent);
            base.Remove(ent);
        }
        public new void RemoveAll(Predicate<T> match)
        {
            Hash.RemoveWhere(match);
            base.RemoveAll(match);
        }
        public new bool Contains(T ent)
        {
            return Hash.Contains(ent);
        }
        public bool Contains(HashList<T> ent)
        {
            foreach (var item in ent)
            {
                if (Hash.Contains(item))
                    return true;
            }
            return false;
        }
        public new void Clear()
        {
            Hash.Clear();
            base.Clear();
        }

        public int Add(object value)
        {
            Add((T)value);
            return Hash.Count;
        }

        void IDeserializationCallback.OnDeserialization(object sender)
        {
            Hash = new HashSet<T>(this);
        }
    }
}