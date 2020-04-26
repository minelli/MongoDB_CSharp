using MongoDB.Driver;

using MongoDB_CSharp.Models;
using MongoDB_CSharp.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;

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

                //Insert one register
                var user = new User
                {
                    email = "user@email.com",
                    name = "User",
                    password = "12345"
                };

                repository.InsertOne(user);
                Console.WriteLine("Insert one register into collection");

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
                Console.WriteLine("Insert a list of registers into collection");

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
