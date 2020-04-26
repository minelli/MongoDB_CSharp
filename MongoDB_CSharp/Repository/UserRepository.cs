using MongoDB.Driver;
using MongoDB_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
    }
}
