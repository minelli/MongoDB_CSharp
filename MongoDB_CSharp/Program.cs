using MongoDB_CSharp.Models;
using MongoDB_CSharp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MongoDB_CSharp
{
    class Program
    {
        static UserRepository repository = new UserRepository();
        static void Main(string[] args)
        {


            try
            {

                Console.WriteLine("Connect with the server!");

                #region Insert
                //InsertDocuments();
                #endregion

                #region Update
                //UpdateDocuments();
                #endregion

                #region Delete
                //DeleteDocuments();
                #endregion

                #region List
                ListDocuments();

                #endregion
                Console.ReadLine();
            }
            catch (Exception e)
            {

                Console.WriteLine($"Error: {e.Message}");
                Console.ReadLine();
            }
        }

        private static void InsertDocuments()
        {
            //Insert one register

            //var user = new User
            //{
            //    email = "user@email.com",
            //    name = "User",
            //    password = "12345"
            //};

            //repository.InsertOne(user);
            //Console.WriteLine("Insert one document in the collection");

            //Insert many registers
            var userList = new List<User>
                {
                    new User
                    {
                        email = "user@montreal.com",
                        name = "Montreal User",
                        password = "12345",
                        address = new Address
                        {
                            streetAddress = "rue Levy",
                            number = "505",
                            city = "Montreal",
                            province = "QC",
                            country = "Canada"
                        }
                    },
                    new User
                    {
                        email = "user@ontario.com",
                        name = "Ontario User",
                        password = "12345",
                        address = new Address
                        {
                            streetAddress = "Water Street",
                            number = "2670",
                            city = "Kitchener",
                            province = "ON",
                            country = "Canada"
                        }
                    },
                    new User
                    {
                        email = "user@quebec.com",
                        name = "Quebec User",
                        password = "12345",
                        address = new Address
                        {
                            streetAddress = "avenue Royale",
                            number = "2175",
                            city = "Quebec",
                            province = "ON",
                            country = "Canada"
                        }
                    },
                    new User
                    {
                        email = "user2@quebec.com",
                        name = "Quebec User 2",
                        password = "12345",
                        address = new Address
                        {
                            streetAddress = "Boulevard Cremazie",
                            number = "3345",
                            city = "Quebec",
                            province = "QC",
                            country = "Canada"
                        }
                    },
                    new User
                    {
                        email = "user@campogrande.com",
                        name = "Campo Grande User",
                        password = "12345",
                        address = new Address
                        {
                            streetAddress = "Rua da Batuta",
                            number = "376",
                            city = "Campo Grande",
                            province = "MS",
                            country = "Brasil"
                        }
                    },
                    new User
                    {
                        email = "user@campogrande.com",
                        name = "Campo Grande User",
                        password = "12345",
                        address = new Address
                        {
                            streetAddress = "Bolman Court",
                            number = "941",
                            city = "Chicago",
                            province = "IL",
                            country = "United States of America"
                        }
                    }
                };

            repository.InsertMany(userList);
            Console.WriteLine("Insert a list of documents in the collection");

        }

        private static void UpdateDocuments()
        {
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
        }

        private static void DeleteDocuments()
        {
            /*
                * Even if exists more than one document with the given filter,
                * will be deleted one because was selected the DeleteOne method. 
                * In this case, the first document found will be deleted
                */

            var countOne = repository.DeleteOne("active", false);
            bool successOneDeletion = countOne > 0;

            if (successOneDeletion)
                Console.WriteLine($"Delete {countOne} documents from the collection");
            else
                Console.WriteLine("No documents in the collection with the given filters");


            var countMany = repository.DeleteMany("active", false);
            bool successManyDeletion = countMany > 0;

            if (successManyDeletion)
                Console.WriteLine($"Delete {countMany} documents from the collection");
            else
                Console.WriteLine("No documents in the collection with the given filters");

        }

        private static void ListDocuments()
        {
            var filters = new List<Filter>
                {
                    new Filter("password","12345"),
                    new Filter("active", false, EFilterOperation.AND)
                };

            Console.WriteLine("Listing all\n");
            var allUsers = repository.List().ToList();
            allUsers.ForEach(u => Console.WriteLine(u.GetAllInfos()));

            Console.WriteLine("\nListing with filters");
            filters.ForEach(f => Console.WriteLine($"{f.FilterName} - {f.FilterValue} - {f.Operation.ToString()}"));
            Console.WriteLine("\n");
            var userFiltered = repository.List(filters).ToList();
            userFiltered.ForEach(u => Console.WriteLine(u.GetAllInfos()));
        }
    }
}
