using System;
using System.Collections.Generic;

namespace DefectLog.Web.ViewModels
{
    public class DefectListItem
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public int TesterId { get; set; }
        public int? DeveloperId { get; set; }
        public string Summary { get; set; }
        public DateTime DateLogged { get; set; }
        public string TesterName { get; set; }
        public string DeveloperName { get; set; }
        public string CssClass { get; set; }
        public int AppVersionId { get; set; }
        public string Build { get; set; }
        public string Screen { get; set; }
        public string LineOfBusiness { get; set; }
        public string Priority { get; set; }
        public int PriorityLevelId { get; set; }
        public int? CategoryId { get; set; }

        public IEnumerable<CommentListItem> Comments { get; set; }
    }
}