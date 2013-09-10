using System;

namespace DefectLog.Web.Forms
{
    public class DefectForm
    {
        public int Id { get; set; }
        public int? DeveloperId { get; set; }
        public int? TesterId { get; set; }
        public int? StatusId { get; set; }
        public int? CategoryId { get; set; }
        public int PriorityLevelId { get; set; }
        public int AppVersionId { get; set; }
        public string Summary { get; set; }
        public string Build { get; set; }
        public string Screen { get; set; }
        public string LineOfBusiness { get; set; }
        public DateTime DateLogged { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
    }
}