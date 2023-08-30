namespace CIS4327_Bartender.Models.Account
{
    public class EditAccountModel
    {
        public string TempUserId { get; set; }
        public string TempSecurityStamp { get; set; }
        public string OldUserName { get; set; }
        public string OldUserEmail { get; set; }
        public string NewUserName { get; set; }
        public string NewUserEmail { get; set; }


    }
}
