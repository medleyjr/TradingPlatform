using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;

namespace Medley.Common.ADODatabase
{
    /// <summary>
    /// Helper Class to Copy Data To/From DataTable to Class
    /// </summary>
    public class ADODatabaseHelper
    {
        //  Internal Class
        class PropertyMapper
        {
            public string ColumnName { get; set; }
            public PropertyInfo Property { get; set; }
        }

        private static Dictionary<Type, List<PropertyMapper>> _TableMapper = new Dictionary<Type, List<PropertyMapper>>();
        private static object lockObject = new object();

        public static DbType GetDbType(object obj)
        {
            if (obj is int)
                return DbType.Int32;
            else if (obj is long)
                return DbType.Int64;
            else if (obj is string)
                return DbType.String;
            else if (obj is DateTime)
                return DbType.DateTime;
            else if (obj is double)
                return DbType.Double;
            else if (obj is float)
                return DbType.Double;


            return DbType.String;

        }

        public static List<T> ConvertDataTableToList<T>(DataTable table) where T : class, new()
        {
            List<T> returnList = new List<T>();
            List<PropertyMapper> propList = FindPropertyMapper(typeof(T), table);
            foreach (DataRow row in table.Rows)
            {
                returnList.Add(CopyFromRowToClass<T>(table, row, propList));
            }
            return returnList;
        }

        public static T CopyFromRowToClass<T>(DataTable table, DataRow row) where T : class, new()
        {
            List<PropertyMapper> propList = FindPropertyMapper(typeof(T), table);
            return CopyFromRowToClass<T>(table, row, propList);
        }

        public static DataRow CopyFromClassToRow<T>(DataTable table, T entity, DataRow row) where T : class//, new()
        {
            List<PropertyMapper> propList = FindPropertyMapper(typeof(T), table);
            return CopyFromClassToRow<T>(table, entity, row ,propList);
        }

        //  Internal Helper
        private static T CopyFromRowToClass<T>(DataTable table, DataRow row, List<PropertyMapper> propList) where T : class, new()
        {
            T item = new T();
            foreach (var propMapper in propList)
            {
                //  Call Set Property
              //  try
                {
                    propMapper.Property.SetValue(item, row[propMapper.ColumnName], null);
                }
              //  catch (Exception ex)
                { 
                }
            }
            return item;
        }

        //  Internal Helper
        private static DataRow CopyFromClassToRow<T>(DataTable table, T item, DataRow row, List<PropertyMapper> propList) where T : class//, new()
        {
            foreach (var propMapper in propList)
            {
                //  Call Set Property
             //   try
                {
                    if(!table.Columns.Contains(propMapper.ColumnName))
                        table.Columns.Add(propMapper.ColumnName);

                    //if(propMapper.ColumnName.ToUpper() != "ID")
                        row[propMapper.ColumnName] = propMapper.Property.GetValue(item, null);
                }
             //   catch (Exception ex)
                {
                }
            }
            return row;
        }

        private static List<PropertyMapper> FindPropertyMapper(Type objectType, DataTable table)
        {
            //  Multi Threading
            lock (lockObject)
            {
                if (!_TableMapper.ContainsKey(objectType))
                {
                    _TableMapper.Add(objectType, BuildPropertyMapper(objectType, table));
                }
            }
            return _TableMapper[objectType];
        }

        private static List<PropertyMapper> BuildPropertyMapper(Type objectType, DataTable table)
        {
            string columnName = "";
            List<PropertyMapper> propMapper = new List<PropertyMapper>();
            //  Pass each propery
            foreach (var propitem in objectType.GetProperties((BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy)))
            {
                columnName = propitem.Name;
                //  Check for attributes
               /* object[] _Attribs = propitem.GetCustomAttributes(typeof(ColumnNameAttribute), true);
                if (_Attribs.Length > 0)
                {
                    columnName = ((ColumnNameAttribute)_Attribs[0]).ColumnName;
                }*/
                //  Check in Columns
                //if (table.Columns.Contains(columnName))
                {
                    //  Assume we can use the Set Method
                    //  Add to list
                    propMapper.Add(new PropertyMapper() { ColumnName = columnName, Property = propitem });
                }
            }
            return propMapper;
        }
    }
}
