using System;
using System.Collections.Generic;

namespace bank.Models
{
    public class User : BaseEntity
    {
        public int userID { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int balance { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }


        public List<Transaction> Trans {get;set;}
        //here we created a list name Trans of type Transaction
        // this list will get filled as we create users
        public User(){
            Trans = new List<Transaction>();
            balance = 100;
            //instanciation of new object Trans
            //this is a constructor method
        }
    }
}