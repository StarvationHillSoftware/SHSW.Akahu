namespace SHSW.Akahu.Model
{
    public class AccountResult
    {
        public bool success { get; set; }
        public List<Account> items { get; set; }
        public Cursor? cursor { get; set; }
    }
    public class Account
    {
        public string _id { get; set; }
        public string _credentials { get; set; }
        public AccountConnection connection { get; set; }
        public string name { get; set; }
        public string formatted_account { get; set; }
        public string status { get; set; }
        public string type { get; set; }
        public string[] attributes { get; set; }
        public AccountBalance balance { get; set; }
        public AccountMeta meta { get; set; }
        public AccountRefreshData refreshed { get; set; }
    }

    public class AccountConnection
    {
        public string name { get; set; }
        public string logo { get; set; }
        public string _id { get; set; }
    }

    public class AccountBalance
    {
        public string currency { get; set; }
        public decimal current { get; set; }
        public decimal available { get; set; }
        public bool overdrawn { get; set; }
    }

    public class AccountMeta
    {
        public string holder { get; set; }
    }

    public class AccountRefreshData 
    {
        public DateTime balance { get; set; }
        public DateTime meta { get; set; }
        public DateTime transactions { get; set; }
        public DateTime party { get; set; }
    }

}
