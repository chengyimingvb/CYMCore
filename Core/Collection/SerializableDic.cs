//------------------------------------------------------------------------------
// SDic.cs
// Copyright 2019 2019/12/5 
// Created by CYM on 2019/12/5
// Owner: CYM
// 填写类的描述...
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CYM
{
    [Unobfus]
    [Serializable]
    public class SerializableDic<TKey, TValue> : Dictionary<TKey, TValue>, IDeserializationCallback, ISerializable
    {
        #region Data
        List<TKey> Key1 = new List<TKey>();
        List<TValue> Value = new List<TValue>();

        public SerializableDic(SerializationInfo info, StreamingContext context)
        {
            Key1 = (List<TKey>)info.GetValue("key1", typeof(List<TKey>));
            Value = (List<TValue>)info.GetValue("Value", typeof(List<TValue>));
            for (int i = 0; i < Key1.Count; ++i)
            {
                Add(Key1[i], Value[i]);
            }
        }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Key1.Clear();
            Value.Clear();
            foreach (var item in this)
            {
                Key1.Add(item.Key);
                Value.Add(item.Value);
            }
            info.AddValue("key1", Key1);
            info.AddValue("Value", Value);
        }
        public SerializableDic() : base() { }
        void IDeserializationCallback.OnDeserialization(object sender) { }
        #endregion
    }
}