using System.ComponentModel.DataAnnotations;

namespace ChatApi.Model
{
    public class FriendModel
    {

        [Key]
        public int Id { get; set; }
        public string username { get; set; }

        public string ownerusername { get; set; }

    }
    public class chatMamber
    {

        [Key]
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }

    }
    public class Requests
    {

        [Key]
        public int Id { get; set; }
        public string ownerusername { get; set; }
        public string username { get; set; }
        public DateTime time { get; set; }
    }
    public class friendChatModel
    {

        [Key]
        public int Id { get; set; }
        public string ownerusername { get; set; }
        public string username { get; set; }
        public string images { get; set; }

        public string sendusername { get; set; }
        public string chat { get; set; }
        public string seentext { get; set; }
        public DateTime? time { get; set; }
    }
    public class accountInformation
    {

        [Key]
        public string username { get; set; }
        public string name { get; set; }
        public string highlighttext { get; set; }
        public string aboutme { get; set; }
        public string contactnum { get; set; }
        public string currEdu { get; set; }
        public string address { get; set; }
        public string facebooklink { get; set; }
        public string instragramlink { get; set; }
        public string linkdinlink { get; set; }
        public string githublink { get; set; }
        public string youtubelink { get; set; }
        public string whatsappnum { get; set; }
        public string tiktoklink { get; set; }
        public string redditlink { get; set; }
        public string snapchartlink { get; set; }
        public string twitterlink { get; set; }
        public string pinterestlink { get; set; }
        public string websitelink { get; set; }
        public string website2link { get; set; }
        public string website3linl { get; set; }
        public string nationality { get; set; }
        public bool isactive { get; set; }
        public string base64Image { get; set; }
        public string base64ImageHigh { get; set; }
        public string connectionId { get; set; }
    }
}
