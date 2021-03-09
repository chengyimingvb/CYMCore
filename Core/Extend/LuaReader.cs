using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using MoonSharp.Interpreter;
using UnityEngine;

namespace CYM
{
    public interface ILuaObj
    {
        void OnBeCreated();
    }

	public class LuaReader:MonoBehaviour
	{
        [RuntimeInitializeOnLoadMethod]
        static void Init()
        {
            var go = new GameObject("LuaReader");
            go.AddComponent<LuaReader>();
            go.hideFlags = HideFlags.HideInHierarchy;
        }

        #region custom Readers
        private static readonly Dictionary<Type, Func<DynValue, object>> customReaders = new Dictionary<Type, Func<DynValue, object>>();
        //Adds a custom Lua reader or overrides the default reader for a specific CLR type.
        public static void AddCustomReader(Type type, Func<DynValue, object> reader)
        {
            if (type == null || reader == null) return;
            customReaders[type] = reader;
        }
        // Removes a custom Lua reader set for a specific CLR type.
        public static void RemoveCustomReader(Type type)
        {
            if (type == null) return;
            customReaders.Remove(type);
        }
        #endregion

        #region public
        public static object Convert(DynValue luaValue, Type type)
        {
            // Custom Converters
            if (customReaders.ContainsKey(type)) return customReaders[type](luaValue);
            // Read basic types
            if (type == typeof(bool)) return luaValue.Boolean;
            if (type == typeof(int)) return (int)luaValue.Number;
            if (type == typeof(float)) return (float)luaValue.Number;
            if (type == typeof(double)) return luaValue.Number;
            if (type == typeof(string) && luaValue.String != null) return luaValue.String;
            if (type == typeof(byte)) return (byte)luaValue.Number;
            if (type == typeof(decimal)) return (decimal)luaValue.Number;
            // Read Lua closure
            if (luaValue.Type == DataType.Function) return luaValue.Function;
            // Read enums
            if (type.IsEnum && luaValue.String != null) return System.Convert.ChangeType(Enum.Parse(type, luaValue.String), type);
            if (luaValue.Table == null) return null;
            // Read Unity types
            if (type == typeof(DateTime)) return ReadDateTime(luaValue.Table);
            if (type == typeof(Color)) return ReadColor(luaValue.Table);
            if (type == typeof(Color32)) return ReadColor32(luaValue.Table);
            if (type == typeof(Rect)) return ReadRect(luaValue.Table);
            if (type == typeof(Vector2)) return ReadVector2(luaValue.Table);
            if (type == typeof(Vector3)) return ReadVector3(luaValue.Table);
            if (type == typeof(Vector4)) return ReadVector4(luaValue.Table);
            // Read Ex types
            if (type == typeof(Range)) return ReadRange(luaValue.Table);
            // Read arrays
            if (type.IsArray) return ReadArray(luaValue.Table, type);
            // Read generic lists
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>)) return ReadList(luaValue.Table, type);
            // read hash set
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(HashSet<>)) return ReadHashSet(luaValue.Table, type);
            // read hash list
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(HashList<>)) return ReadHashList(luaValue.Table, type);
            // Read generic dictionaries
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>)) return ReadDictionary(luaValue.Table, type);
            // Read generic multi
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(MultiDic<,>)) return ReadMultiDic(luaValue.Table, type);
            // Read classes
            if (type.IsClass) return ReadClass(luaValue.Table, type);
            return null;
        }
        //��ȡ��������
        public static void SetValue<T>(T obj, string propertyName, DynValue propertyValue)
        {
            try
            {
                if (propertyName == Const.STR_LuaTemplate)
                    return;
                PropertyInfo property = obj.GetType().GetProperty(propertyName);
                FieldInfo field = obj.GetType().GetField(propertyName);

                if (propertyValue == null)
                {
                    CLog.Error($"{propertyName}:��ȡ��ֵΪ��");
                    return;
                }

                if (property == null && field == null)
                {
                    CLog.Error($"{propertyName}:û���������!");
                    return;
                }

                if (property != null)
                {
                    if (!property.CanWrite)
                    {
                        CLog.Error($"{propertyName}:��ȡ����!");
                        return;
                    }
                }

                Type tempType = null;
                if (property != null)
                    tempType = property.PropertyType;
                else if (field != null)
                    tempType = field.FieldType;

                property.SetValue(obj, Convert(propertyValue, tempType), null);
            }
            catch
            {
                Debug.LogError("LuaReader: could not define property:" + propertyName + ",valueName:" + propertyValue);
                throw new Exception();
            }
        }
        #endregion

        #region private
        private static DateTime ReadDateTime(Table luaTable)
        {
            int year = luaTable[1] == null ? 0 : (int)(double)luaTable[1];
            int month = luaTable[2] == null ? 0 : (int)(double)luaTable[2];
            int day = luaTable[3] == null ? 0 : (int)(double)luaTable[3];
            int hour = luaTable[4] == null ? 0 : (int)(double)luaTable[4];
            int minute = luaTable[5] == null ? 0 : (int)(double)luaTable[5];
            int second = luaTable[6] == null ? 0 : (int)(double)luaTable[6];
            return new DateTime(year, month, day, hour, minute, second);
        }
        private static Color ReadColor(Table luaTable)
		{
			float r = luaTable[1] == null ? 0f : (float) (double) luaTable[1];
			float g = luaTable[2] == null ? 0f : (float) (double) luaTable[2];
			float b = luaTable[3] == null ? 0f : (float) (double) luaTable[3];
			float a = luaTable[4] == null ? 1f : (float) (double) luaTable[4];
			return new Color(r, g, b, a);
		}
		private static Color32 ReadColor32(Table luaTable)
		{
			byte r = luaTable[1] == null ? (byte) 0 : (byte) (double) luaTable[1];
			byte g = luaTable[2] == null ? (byte) 0 : (byte) (double) luaTable[2];
			byte b = luaTable[3] == null ? (byte) 0 : (byte) (double) luaTable[3];
			byte a = luaTable[4] == null ? (byte) 255 : (byte) (double) luaTable[4];
			return new Color32(r, g, b, a);
		}
		private static Rect ReadRect(Table luaTable)
		{
			float x = luaTable[1] == null ? 0 : (float) (double) luaTable[1];
			float y = luaTable[2] == null ? 0 : (float) (double) luaTable[2];
			float width = luaTable[3] == null ? 0 : (float) (double) luaTable[3];
			float height = luaTable[4] == null ? 0 : (float) (double) luaTable[4];
			return new Rect(x, y, width, height);
		}
        private static Range ReadRange(Table luaTable)
        {
            float x = luaTable[1] == null ? 0 : (float)(double)luaTable[1];
            float y = luaTable[2] == null ? 0 : (float)(double)luaTable[2];
            return new Range(x, y);
        }
        private static Vector2 ReadVector2(Table luaTable)
		{
			float x = luaTable[1] == null ? 0 : (float) (double) luaTable[1];
			float y = luaTable[2] == null ? 0 : (float) (double) luaTable[2];
			return new Vector2(x, y);
		}
		private static Vector3 ReadVector3(Table luaTable)
		{
			float x = luaTable[1] == null ? 0 : (float) (double) luaTable[1];
			float y = luaTable[2] == null ? 0 : (float) (double) luaTable[2];
			float z = luaTable[3] == null ? 0 : (float) (double) luaTable[3];
			return new Vector3(x, y, z);
		}
		private static Vector4 ReadVector4(Table luaTable)
		{
			float x = luaTable[1] == null ? 0 : (float) (double) luaTable[1];
			float y = luaTable[2] == null ? 0 : (float) (double) luaTable[2];
			float z = luaTable[3] == null ? 0 : (float) (double) luaTable[3];
			float w = luaTable[4] == null ? 0 : (float) (double) luaTable[4];
			return new Vector4(x, y, z, w);
		}
		private static Array ReadArray(Table luaTable, Type type)
		{
			Type elementType = type.GetElementType();
			if (elementType == null) return null;
			if (type.GetArrayRank() == 1)
			{
				Array array = Array.CreateInstance(elementType, luaTable.Values.Count());
				int i = 0;
				foreach (var dynValue in luaTable.Values)
				{
                    array.SetValue(System.Convert.ChangeType(Convert(dynValue, elementType), elementType), i);
					i++;
				}
				return array;
			}
			if (type.GetArrayRank() == 2)
			{
				var maxLength = (from dynValue in luaTable.Values where dynValue.Table != null select dynValue.Table.Values.Count()).Concat(new[] {0}).Max();
				Array array = Array.CreateInstance(elementType, luaTable.Values.Count(), maxLength);
				int i = 0;
				foreach (var dynValue in luaTable.Values)
				{
					int j = 0;
					foreach (var subElement in dynValue.Table.Values)
                    {
                        array.SetValue(System.Convert.ChangeType(Convert(subElement, elementType), elementType), i, j);
						j++;
					}
					i++;
				}
				return array;
			}
			return null;
		}
		private static IList ReadList(Table luaTable, Type type)
		{
			Type elementType = type.GetGenericArguments()[0];
			var list = (IList) Activator.CreateInstance(typeof (List<>).MakeGenericType(elementType));
            foreach (DynValue value in luaTable.Values)
            {
                object obj = Convert(value, elementType);
                list.Add(System.Convert.ChangeType(obj, obj.GetType()));
            }
			return list;
		}
        private static IList ReadHashSet(Table luaTable, Type type)
        {
            throw new Exception("Lua����ϵͳ��֧��������� HashSet ");
        }
        private static IHashList ReadHashList(Table luaTable, Type type)
        {
            Type elementType = type.GetGenericArguments()[0];
            var list = (IHashList)Activator.CreateInstance(typeof(HashList<>).MakeGenericType(elementType));
            foreach (DynValue value in luaTable.Values)
            {
                object obj = Convert(value, elementType);
                list.Add(System.Convert.ChangeType(obj, obj.GetType()));
            }
            return list;
        }
        private static IDictionary ReadDictionary(Table luaTable, Type type)
		{
			Type keyType = type.GetGenericArguments()[0];
			Type valueType = type.GetGenericArguments()[1];
			var dictionary = (IDictionary) Activator.CreateInstance(typeof (Dictionary<,>).MakeGenericType(keyType, valueType));
			foreach (TablePair pair in luaTable.Pairs)
			{
                object keyObj = Convert(pair.Key, keyType);
                object keyValue = System.Convert.ChangeType(keyObj, keyObj.GetType());
                if (keyValue == null) continue;

                object valObj = Convert(pair.Value, valueType);
                dictionary[keyValue] = System.Convert.ChangeType(valObj, valObj.GetType());
			}
			return dictionary;
		}
        private static IMultiDic ReadMultiDic(Table luaTable, Type type)
        {
            Type keyType = type.GetGenericArguments()[0];
            Type valueType = type.GetGenericArguments()[1];
            var dictionary = (IMultiDic)Activator.CreateInstance(typeof(MultiDic<,>).MakeGenericType(keyType, valueType));

            foreach (DynValue value in luaTable.Values)
            {
                object v1 = Convert(value.Table.Get(1), keyType);
                object v2 = Convert(value.Table.Get(2), keyType);
                object v3 = Convert(value.Table.Get(3), valueType);
                dictionary.Add(
                    System.Convert.ChangeType(v1, v1.GetType()),
                    System.Convert.ChangeType(v2, v2.GetType()),
                    System.Convert.ChangeType(v3, v3.GetType())
                    );
            }
            return dictionary;
        }
        private static object ReadClass(Table luaTable, Type type)
        {
            object cObject = CreateObj(luaTable, type);

            if (!typeof(object).IsValueType && cObject == null)
            {
                Debug.LogWarning("LuaReader: method ReadObjectData called with null object.");
                return null;
            }
            if (luaTable == null)
            {
                Debug.LogWarning("LuaReader: method ReadObjectData called with null Lua table.");
                return null;
            }
            foreach (TablePair propertyPair in luaTable.Pairs)
                SetValue(cObject, propertyPair.Key.String, propertyPair.Value);

            return cObject;
        }
        //����Table����һ����
        private static object CreateObj(Table luaTable, Type type)
        {
            Type newType = type;
            object cObject = null;
            string nameSpace = type.Namespace.ToString();
            DynValue temp = luaTable.RawGet(Const.STR_LuaTemplate);
            if (temp != null)
            {
                newType = Type.GetType(nameSpace + "." + temp.String, false, false);
                if (newType == null)
                {
                    newType = Type.GetType(Const.NameSpace + "." + temp.String, true, false);
                    if (newType == null)
                        CLog.Error("�޷��ҵ�������:" + temp);
                }
                cObject = newType.Assembly.CreateInstance(newType.FullName);
            }
            else
            {
                cObject = Activator.CreateInstance(type);
            }

            if (cObject is ILuaObj luaObj)
                luaObj.OnBeCreated();

            return cObject;
        }
        #endregion
    }
}