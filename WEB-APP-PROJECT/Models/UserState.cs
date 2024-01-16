using System.ComponentModel.DataAnnotations;


namespace WEB_APP_PROJECT.Models
{
    public class UserState
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string State { get; set; }
    }
}