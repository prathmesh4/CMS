using System.ComponentModel.DataAnnotations;

namespace CustMgmSys.Models
{

    public class EmailViewModel
    {
        [Required(ErrorMessage = "The Sender field is required.")]
        [EmailAddress(ErrorMessage = "Invalid sender email address.")]
        public string From { get; set; }

        [Required(ErrorMessage = "The Recipient field is required.")]
        [EmailAddress(ErrorMessage = "Invalid recipient email address.")]
        public string To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public IFormFile Attachment { get; set; }
    }

    public class EmailAttachment
    {
        public byte[] Data { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }

    public class EmailSettings
    {
        public string EmailFrom { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
    }


}
