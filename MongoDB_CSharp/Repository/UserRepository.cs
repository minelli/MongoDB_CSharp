using MongoDB.Driver;
using MongoDB_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB_CSharp.Repository
{
    public class UserRepository
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString;
        private MongoClient client;
        private IMongoDatabase database;
        private IMongoCollection<User> collection;

        public UserRepository()
        {
            client = new MongoClient(connectionString);
            database = client.GetDatabase("store");
            collection = database.GetCollection<User>("user");
        }

        /// <summary>
        /// Method  insert one documento in the collection
        /// </summary>
        /// <param name="user">Object to insert</param>
        public void InsertOne(User user)
        {
            try
            {
                collection.InsertOne(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to insert many documents in the collection
        /// </summary>
        /// <param name="users">List of objects to insert</param>
        public void InsertMany(List<User> users)
        {
            try
            {
                collection.InsertMany(users);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to update one document from a collection
        /// </summary>
        /// <param name="instance">The object to update</param>
        public void UpdateOne(User instance)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq("name", instance.name);

                var properties = GetProperties(instance);
                var updateList = SetFieldsForUpdate(properties);
                var update = Builders<User>.Update.Combine(updateList);

                collection.UpdateOne(filter, update);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to update many documents from a collection
        /// </summary>
        /// <param name="properties">Dicitonary with property and value to update</param>
        public void UpdateMany(Dictionary<object, object> properties)
        {
            /*
             * In case of an adiction of a new field, the olders documents w'ont get this new field.
             * To solve this, we can make an UpdateMany operation, setting the new field.
             * For this, we create an empty Filter, then we set the neew field with the needed value
             */

            var filter = Builders<User>.Filter.Empty;
            var updateList = SetFieldsForUpdate(properties);
            var update = Builders<User>.Update.Combine(updateList);

            collection.UpdateMany(filter, update);
        }

        /// <summary>
        /// Method to delete one document from a collection
        /// </summary>
        /// <param name="filterName">The name of the filter</param>
        /// <param name="filterValue">The value of the filter</param>
        /// <returns>Return the quantity of deleted documents</returns>
        public long DeleteOne(string filterName, object filterValue)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq(filterName, filterValue);
                var result = collection.DeleteOne(filter);
                return result.DeletedCount;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to delete multiple documents from a collection
        /// </summary>
        /// <param name="filterName">The name of the filter</param>
        /// <param name="filterValue">The value of the filter</param>
        /// <returns>Return the quantity of deleted documents</returns>
        public long DeleteMany(string filterName, object filterValue)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq(filterName, filterValue);
                var result = collection.DeleteMany(filter);
                return result.DeletedCount;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Method to update one document from a collection
        /// </summary>
        /// <param name="instance">The object to update</param>
        public IEnumerable<User> List(List<Filter> filters = null)
        {
            try
            {
                var filter = Builders<User>.Filter.Empty;

                if (filters != null)
                {
                    foreach (var item in filters)
                    {
                        if(item.Operation == EFilterOperation.AND)
                            filter = filter & (Builders<User>.Filter.Eq(item.FilterName, item.FilterValue));
                        else
                            filter = filter | (Builders<User>.Filter.Eq(item.FilterName, item.FilterValue));
                    }
                }


                return collection.Find(filter).ToEnumerable();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Functions
        /// <summary>
        /// Method to get by reflection the properties name and value from object
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private Dictionary<object, object> GetProperties(User user)
        {
            var dictionary = new Dictionary<object, object>();
            Type t = typeof(User);
            PropertyInfo[] propInfos = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in propInfos)
            {
                dictionary[prop.Name] = GetValueByProperty(user, prop.Name);
            }
            return dictionary;
        }

        /// <summary>
        /// Get the instance property value from the given name
        /// </summary>
        /// <param name="instance">Instance to get the value</param>
        /// <param name="property">Desired property's name</param>
        /// <returns></returns>
        private object GetValueByProperty(User instance, string property)
        {
            return instance.GetType().GetProperty(property).GetValue(instance, null);
        }

        /// <summary>
        /// Create a list with the UpdateDefinition including the fields and values for update
        /// </summary>
        /// <param name="properties">Dictionary with properties and values</param>
        /// <returns></returns>
        private List<UpdateDefinition<User>> SetFieldsForUpdate(Dictionary<object, object> properties)
        {
            var updateList = new List<UpdateDefinition<User>>();
            properties.ToList().ForEach(p => updateList.Add(Builders<User>.Update.Set(p.Key.ToString(), p.Value)));

            return updateList;
        }

        #endregion
    }
}