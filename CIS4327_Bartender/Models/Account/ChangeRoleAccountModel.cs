namespace CIS4327_Bartender.Models.Account
{
    public class ChangeRoleAccountModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string CurrentRoleName { get; set; }

        public string SelectedRoleName { get; set; }
    }
}
