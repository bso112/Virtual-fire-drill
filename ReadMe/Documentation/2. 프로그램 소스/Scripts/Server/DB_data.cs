using System.Collections;
using System.Collections.Generic;

public class DB_data
{
    public string Users { get; set; }
    public string Timeleft { get; set; }
    public string Score { get; set; }
    public string today { get; set; }
    public string mission { get; set; }

    public DB_data() { }

    public DB_data(string userName, string timeLeft, string score, string today)
    {
        this.Users = userName;
        this.Timeleft = timeLeft;
        this.Score = score;
        this.today = today;
       
    }

}
