using System.ComponentModel.DataAnnotations;

namespace KPBrokers.Submission.Quote.Common.Models.DocuBox
{
    public class TaskModel
    {
        public string? Title { get; set; }
        public string? Description { get; set; }

        [Display(Name = "Status")]
        public int StatusId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
