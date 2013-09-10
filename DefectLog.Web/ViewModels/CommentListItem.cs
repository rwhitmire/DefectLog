using System;

namespace DefectLog.Web.ViewModels
{
    public class CommentListItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }
        public UserListItem User { get; set; }
    }
}