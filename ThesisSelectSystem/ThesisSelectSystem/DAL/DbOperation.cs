using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Xml;

namespace ThesisSelectSystem.DAL
{
    public class DbOperation
    {
        private static XmlDocument xmlDocument=null;
        private static string xmlPath ;


        public static void SetXmlPath(string XmlPath)
        {
            xmlPath = XmlPath;
        }


        public static string GetXmlPath()
        {
            if (xmlPath!=null)
            {
                return xmlPath;
            }
            else
            {
                throw new Exception("配置文件路径为空！");
            }
            
        }


        /// <summary>
        /// 加载xml配置文件
        /// </summary>
        /// <returns></returns>
        private static XmlDocument GetXmlDocument()
        {
            if (xmlDocument == null)
            {
                xmlDocument = new XmlDocument();
                if (xmlPath != null)
                {
                    if (File.Exists(xmlPath))
                    {
                        xmlDocument.Load(xmlPath);
                        return xmlDocument;
                    }
                    else
                    {
                        throw new Exception("未找到" + xmlPath);
                    }
                }
                else
                {
                    throw new Exception("配置文件TableMappingObj.xml未找到");
                }
            }
            else
            {
                return xmlDocument;
            }
        }


        /// <summary>
        /// 获取xml文件里指定name属性的table元素
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>XmlElement元素</returns>
        private static XmlElement GetTableElement(string tableName)
        {
            XmlDocument xmlDoc = GetXmlDocument();
            XmlElement root = xmlDoc.DocumentElement;
            XmlNodeList tableNodeList = root.ChildNodes;
            XmlElement table = null;
            foreach (var tableNode in tableNodeList)
            {
                table = (XmlElement)tableNode;
                if (table.GetAttribute("name").ToLower().Equals(tableName.ToLower()))
                {
                    break;
                }
                else
                {
                    table = null;
                }
            }
            return table;
        }


        /// <summary>
        /// 获取TableMappingObj.xml配置文件下指定表column元素的"use"属性为true的表字段与属性映射信息
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <returns>表字段与对象属性映射的字典集</returns>
        public static Dictionary<string, string> GetFieldsMappingObjectProperties(string tableName)
        {
            XmlElement tableElement = GetTableElement(tableName);
            Dictionary<string, string> mapInfos = new Dictionary<string, string>();
            try
            {
                XmlNodeList columnNames = tableElement.ChildNodes;
                foreach (var columnName in columnNames)
                {
                    XmlElement field = (XmlElement)columnName;
                    if (field.GetAttribute("use").ToLower().Equals("true"))
                    {
                        string fieldName = field.GetAttribute("name");
                        if (fieldName != null)
                        {
                            string propertyName = field.InnerText.Trim();
                            if (propertyName.Equals("") || propertyName == null)
                            {
                                throw new Exception("类属性值为空!");
                            }
                            else
                            {
                                mapInfos[fieldName] = propertyName;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return mapInfos;
        }


        /// <summary>
        /// 获取TableMappingObj.xml配置文件下指定表的所有字段与属性的映射信息
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <returns>配置文件中所有表字段与对象属性映射的字典集</returns>
        public static Dictionary<string, string> GetAllFieldsMappingObjectProperties(string tableName)
        {
            Dictionary<string, string> mapInfos = new Dictionary<string, string>();
            XmlElement tableElement = GetTableElement(tableName);
            try
            {
                XmlNodeList columnNames = tableElement.ChildNodes;
                foreach (var columnName in columnNames)
                {
                    XmlElement field = (XmlElement) columnName;
                    string fieldName = field.GetAttribute("name");
                    if (fieldName != null)
                    {
                        string propertyName = field.InnerText.Trim();
                        if (propertyName.Equals("") || propertyName == null)
                        {
                            throw new Exception("类属性值为空!");
                        }
                        else
                        {
                            mapInfos[fieldName] = propertyName;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return mapInfos;
        }


        /// <summary>
        /// 将实体类对象保存到数据库对应的表里，该方法依赖于配置文件TableMappingObj.xml,
        /// 对象属性与表字段的映射需要在映射文件的column元素里声明，并把"use"属性设置为"true"
        /// 例：<column name="表字段名" use="true">实体属性名</column>
        /// </summary>
        /// <param name="entity">要保存的实体类对象</param>
        /// <param name="tableName">与对象映射的数据库表名</param>
        /// <returns>SQL语句的执行结果</returns>
        public static bool Save(Object entity, string tableName)
        {
            Dictionary<string, string> tableMapObject = GetFieldsMappingObjectProperties(tableName);
            bool result = false;//用于判断是否成功执行SQL语句的标志

            #region 通过反射获取实体对象的信息
            Type entityType = entity.GetType();
            string[] fieldsString = tableMapObject.Keys.ToArray();
            string[] propertiesString = tableMapObject.Values.ToArray();
            MethodInfo[] usedGetMethods =
                entityType.GetMethods().Where(method => (method.Name.Contains("get_")))
                .Where(method => (propertiesString.Contains(method.ToString()
                .Substring(method.ToString().IndexOf("_") + 1, method.ToString().IndexOf("(") - method.ToString().IndexOf("_") - 1))))
                .ToArray();
            //foreach (var usedGetMethod in usedGetMethods)
            //{
            //    Console.WriteLine(usedGetMethod);
            //}
            #endregion
            #region 拼接数据库插入数据语句
            StringBuilder sqlbuilder = new StringBuilder();
            sqlbuilder.Append("insert into  " + tableName + "(");
            for (int i = 0; i < fieldsString.Length; i++)
            {
                sqlbuilder.Append(fieldsString[i]);
                if (i < fieldsString.Length - 1)
                {
                    sqlbuilder.Append(",");
                }
                else
                {
                    sqlbuilder.Append(")").Append("  values(");
                }
            }
            for (int i = 0; i < propertiesString.Length; i++)
            {
                sqlbuilder.Append("@parameter").Append(i);
                if (i < propertiesString.Length - 1)
                {
                    sqlbuilder.Append(",");
                }
                else
                {
                    sqlbuilder.Append(")");
                }
            }
            string sqltext = sqlbuilder.ToString();
            #endregion
            #region 执行SQL语句
            using (SqlConnection coon = new SqlConnection(SqlHelper.sqlConnectionString))
            {
                try
                {
                    coon.Open();
                    using (SqlCommand cmd = new SqlCommand(sqltext, coon))
                    {

                        for (int i = 0; i < propertiesString.Length; i++)
                        {
                            SqlParameter sqlParameter = new SqlParameter
                                ("@parameter" + i.ToString(), usedGetMethods[i].Invoke(entity, null));
                            cmd.Parameters.Add(sqlParameter);
                        }
                        result = cmd.ExecuteNonQuery() > 0 ? true : false;
                    }
                }
                catch (Exception)
                {

                    throw new Exception("SQL语句执行异常");
                }
            }
            #endregion

            return result;
        }


        /// <summary>
        /// 将实体类对象保存到指定表名的数据库里,该方法依赖于配置文件TableMappingObj.xml
        /// </summary>
        /// <param name="entity">要保存的实体类对象</param>
        /// <param name="tableName">与对象映射的数据库表名</param>
        /// <param name="result">SQL语句的执行结果</param>
        public static void Save(Object entity, string tableName, out bool result)
        {
            Type entityType = entity.GetType();
            #region  反射获取对象类型、方法等信息
            MethodInfo[] getMethodInfos =
                entityType.GetMethods().Where(method => (method.ToString().Contains("get_")))
                .Where(getMethod => (getMethod.Invoke(entity, null) != null))
                .ToArray();
            List<MethodInfo> usedGetMethodsList = new List<MethodInfo>();
            for (int i = 0; i < getMethodInfos.Length; i++)
            {
                if (getMethodInfos[i].ReturnType.IsValueType)
                {
                    if (!getMethodInfos[i].Invoke(entity, null).ToString().Equals("0"))
                    {
                        usedGetMethodsList.Add(getMethodInfos[i]);
                    }
                }
                else
                {
                    usedGetMethodsList.Add(getMethodInfos[i]);
                }
            }
            #endregion

            #region 获取sql语句需要使用的表字段
            Dictionary<string, string> mappingInfo = GetAllFieldsMappingObjectProperties(tableName);
            string[] usedFields = mappingInfo.Keys.Where(
                temp => (usedGetMethodsList.Select(useGetMethod => (useGetMethod.ToString()))
                .Select(getMethodName => (getMethodName.
                Substring(getMethodName.IndexOf("_") + 1,
                          getMethodName.IndexOf("(") - getMethodName.IndexOf("_") - 1))).ToArray()
                .Contains(mappingInfo[temp]))
                ).ToArray();
            var usedGetMethods = usedGetMethodsList.ToArray();
            for (int i = 0; i < usedFields.Length; i++)
            {
                Console.WriteLine("字段：{0}\nget方法：{1}\n", usedFields[i], usedGetMethods[i].ToString());
            }
            #endregion

            #region 拼接sql语句
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into ").Append(tableName).Append("(");
            int length = usedFields.Length;
            for (int i = 0; i < length; i++)
            {
                builder.Append(usedFields[i]);
                if (i < usedFields.Length - 1)
                {
                    builder.Append(",");
                }
                else
                {
                    builder.Append(") values(");
                    for (int j = 0; j < length; j++)
                    {
                        builder.Append("@parameter").Append(j);
                        if (j < length - 1)
                        {
                            builder.Append(",");
                        }
                        else
                        {
                            builder.Append(")");
                        }
                    }
                }
            }
            string sqltext = builder.ToString();
            #endregion

            #region 执行sql语句
            using (SqlConnection coon = new SqlConnection(SqlHelper.sqlConnectionString))
            {
                try
                {
                    coon.Open();
                    using (SqlCommand cmd = new SqlCommand(sqltext, coon))
                    {
                        for (int i = 0; i < length; i++)
                        {
                            SqlParameter sqlParameter = new SqlParameter
                                ("@parameter" + i.ToString(), usedGetMethods[i].Invoke(entity, null));
                            cmd.Parameters.Add(sqlParameter);
                        }
                        result = cmd.ExecuteNonQuery() > 0 ? true : false;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            #endregion
        }


        /// <summary>
        /// 修改实体类对应的表记录的值，
        /// 该方法依赖于TableMappingObj.xml的table元素里配置的primary-key属性，
        /// primary-key属性的值对应的对象属性值不允许为空
        /// </summary>
        /// <param name="entity">要保存的实体类对象</param>
        /// <param name="tableName">实体类对应的表名</param>
        /// <returns>SQL语句执行的结果</returns>
        public static bool Update(Object entity, string tableName)
        {
            bool result = false;
            #region 获取表主键
            var tableElement = GetTableElement(tableName);
            string primaryKey = null;
            string primaryKeyValue = null;
            
            if (tableElement != null)
            {
                primaryKey = tableElement.GetAttribute("primary-key");
            }
            else
            {
                throw new Exception("主键primaryKey的值为null");
            }
            Dictionary<string, string> configurationInfo = GetAllFieldsMappingObjectProperties(tableName);
            if (!configurationInfo.Keys.Contains(primaryKey))
            {
                throw new Exception("配置文件未在table元素配置primary-key属性或未配置主键字段");
            }
            #endregion

            #region 获取对象属性值非空或非0的get方法
            Type entityType = entity.GetType();
            var entityGetMethodsInfo = entityType.GetMethods().Where(method => (method.ToString().Contains("get_")))
                .Where(method => (method.Invoke(entity, null) != null)).ToList();
            List<MethodInfo> getMethodList = new List<MethodInfo>();
            for (int i = 0; i < entityGetMethodsInfo.Count; i++)
            {
                if (entityGetMethodsInfo[i].ReturnType.IsValueType)
                {
                    if (!entityGetMethodsInfo[i].Invoke(entity, null).ToString().Equals("0"))
                    {
                        getMethodList.Add(entityGetMethodsInfo[i]);
                    }
                }
                else
                {
                    getMethodList.Add(entityGetMethodsInfo[i]);
                }
                string methodName = entityGetMethodsInfo[i].ToString();
                if (methodName.Substring(methodName.IndexOf("_") + 1, methodName.IndexOf("(") - methodName.IndexOf("_") - 1).Equals(configurationInfo[primaryKey]))
                {
                    primaryKeyValue = entityGetMethodsInfo[i].Invoke(entity, null).ToString();
                }
            }
            getMethodList.Remove(
                getMethodList.Where(method => (method.ToString().Contains(configurationInfo[primaryKey]))).FirstOrDefault());

            MethodInfo[] getMethods = getMethodList.ToArray();
            string[] usedFields = null;
            if (primaryKeyValue != null)
            {
                usedFields = configurationInfo.Keys.Where(key => (getMethodList.Select(getMethod => getMethod.ToString())
                    .Select(temp => (temp.Substring(temp.IndexOf("_") + 1, temp.IndexOf("(") - temp.IndexOf("_") - 1)))
                    .Contains(configurationInfo[key]))).ToArray();
                for (int i = 0; i < usedFields.Length; i++)
                {
                    Console.WriteLine(usedFields[i]);
                }
            }
            #endregion

            #region 拼接更新表的SQL语句
            StringBuilder builder = new StringBuilder();
            builder.Append("update  ").Append(tableName).Append("  set ");
            for (int i = 0; i < usedFields.Length; i++)
            {
                builder.Append(usedFields[i]).Append(" =").Append("@parameter").Append(i);
                if (i < usedFields.Length - 1)
                {
                    builder.Append(",");
                }
                else
                {
                    builder.Append("  where  ").Append(primaryKey).Append(" = @parameter").Append(i + 1);
                }
            }
            string sqlText = builder.ToString();
            #endregion

            #region 执行sql语句
            using (SqlConnection coon = new SqlConnection(SqlHelper.sqlConnectionString))
            {
                try
                {
                    coon.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlText, coon))
                    {
                        int i = 0;
                        for (i = 0; i < usedFields.Length; i++)
                        {
                            SqlParameter sqlParameter = new SqlParameter
                                ("@parameter" + i.ToString(), getMethods[i].Invoke(entity, null));
                            cmd.Parameters.Add(sqlParameter);
                        }
                        SqlParameter mainKey = new SqlParameter("@parameter" + i.ToString(), primaryKeyValue);
                        cmd.Parameters.Add(mainKey);

                        result = cmd.ExecuteNonQuery() > 0 ? true : false;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            #endregion
            return result;
        }


        /// <summary>
        /// 删除指定id对象对应的表记录，依赖于TableMappingObj.xml配置文件下的table元素里的primary-key属性，该属性不允许为空
        /// </summary>
        /// <param name="entity">要删除的对象</param>
        /// <param name="tableName">与对象映射的表名</param>
        /// <returns>SQL语句执行结果</returns>
        public static bool Delete(Object entity, string tableName)
        {
            bool result = false;
            #region 获取表主键及对应的对象字段的get方法
            var tableElement = GetTableElement(tableName);
            string primaryKeyField = tableElement.GetAttribute("primary-key");
            var mappinfInfo = GetAllFieldsMappingObjectProperties(tableName);
            string primaryKeyProperty = mappinfInfo[primaryKeyField];
            var primaryKeyGetMethod =
            entity.GetType().GetMethods().Where(method => (method.ToString().Contains(primaryKeyProperty)))
            .FirstOrDefault();
            #endregion
            
            #region 拼接删除记录的sql语句
            StringBuilder builder = new StringBuilder();
            builder.Append("delete ").Append(tableName).Append(" where ").Append(primaryKeyField)
                .Append("=").Append("@parameter");
            string sqlText = builder.ToString();
            #endregion

            #region 执行sql语句
            using (SqlConnection coon = new SqlConnection(SqlHelper.sqlConnectionString))
            {
                try
                {
                    coon.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlText, coon))
                    {
                        var primaryKeyValue = primaryKeyGetMethod.Invoke(entity, null);
                        SqlParameter mainKey = new SqlParameter("@parameter", primaryKeyValue);
                        cmd.Parameters.Add(mainKey);

                        result = cmd.ExecuteNonQuery() > 0 ? true : false;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            #endregion
            return result;
        }

    }
}