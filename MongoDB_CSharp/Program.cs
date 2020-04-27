using MongoDB_CSharp.Models;
using MongoDB_CSharp.Repository;
using System;
using System.Collections.Generic;

namespace MongoDB_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            UserRepository repository = new UserRepository();

            try
            {

                Console.WriteLine("Connect with the server!");

                #region Insert
                /*
                //Insert one register
                var user = new User
                {
                    email = "user@email.com",
                    name = "User",
                    password = "12345"
                };

                repository.InsertOne(user);
                Console.WriteLine("Insert one document in the collection");

                //Insert many registers
                var userList = new List<User>
                {
                    new User
                    {
                        email = "user2@email.com",
                        name = "User 2",
                        password = "12345"
                    },
                    new User
                    {
                        email = "user3@email.com",
                        name = "User 3",
                        password = "12345"
                    }
                };

                repository.InsertMany(userList);
                Console.WriteLine("Insert a list of documents in the collection");
                */
                #endregion

                #region Update
                //Updating many fields, adding the new field 'Active', setting to false
                var fields = new Dictionary<object, object>(){
                    { "active", false}
                };

                repository.UpdateMany(fields);
                Console.WriteLine("Updated many documents from a collection");

                var userUpdated = new User
                {
                    name = "User",
                    email = "new_email@email.com",
                    password = "password",
                    active = true
                };

                repository.UpdateOne(userUpdated);
                Console.WriteLine("Updated one document from a collection");

                #endregion
                Console.ReadLine();
            }
            catch (Exception e)
            {

                Console.WriteLine($"Error: {e.Message}");
                Console.ReadLine();
            }
        }
    }
}
