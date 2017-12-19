using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileManager.Data;

namespace FileManager.Mvc.Models
{
    public class FileCommentModel
    {
        public int Id { get; set; }
        public string CommentText { get; set; }
        public int FileId { get; set; }

        public FileCommentModel(FileComment domainComment) {
            this.Id = domainComment.Id;
            this.CommentText = domainComment.CommentText;
            this.FileId = domainComment.FileId;
        }
    }
}