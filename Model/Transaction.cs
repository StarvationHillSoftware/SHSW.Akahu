namespace SHSW.Akahu.Model
{
   
    public class TransactionResult
    {
        public bool success { get; set; }
        public List<Transaction> items { get; set; }
        public Cursor cursor { get; set; }
    }

    public class Transaction
    {
        public string _id { get; set; }
        public string _account { get; set; }
        public string _user { get; set; }
        public string _connection { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime date { get; set; }
        public string description { get; set; }
        public double amount { get; set; }
        public double balance { get; set; }
        public string type { get; set; }
        public string hash { get; set; }
        public Meta meta { get; set; }
        public Merchant merchant { get; set; }
        public Category category { get; set; }
    }

    public class Meta
    {
        public string card_suffix { get; set; }
        public string logo { get; set; }
    }

    public class Merchant
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string website { get; set; }
    }

    public class Category
    {
        public string _id { get; set; }
        public string name { get; set; }
        public object? groups { get; set; }

    }
    public class Cursor
    {
        public string next { get; set; }
    }

    public class PersonalFinance
    {
        public string _id { get; set; }
        public string name { get; set; }
    }
}
