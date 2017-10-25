using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileManager.Mvc.Models
{
    public class EmailSendModel
    {
        public int FileId { get; set; }
        public string Subject { get; set; }
        public string RecipientsEmail { get; set; }
    }
}