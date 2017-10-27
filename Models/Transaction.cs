using System;

namespace bank.Models
{
    public class Transaction : BaseEntity
    {
        public int id { get; set; }
        public string type { get; set; }
        public int amount { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public int userID { get; set; }
        //foreign key

        public User User {get;set;} //this is just a placeholder. comes with the user_id foreign key
        //this doesn't get saved in the database!!
    }
}