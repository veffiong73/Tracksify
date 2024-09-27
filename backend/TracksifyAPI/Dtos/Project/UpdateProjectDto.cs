using System.ComponentModel.DataAnnotations;
using TracksifyAPI.Models;
using static EnsureAtLeastOneElementAttribute;

namespace TracksifyAPI.Dtos.Project
{
    public class UpdateProjectDto
    {
        [Required(ErrorMessage = "ProjectName is required.")]
        [StringLength(100, ErrorMessage = "ProjectName cannot be longer than 100 characters.")]
        public string ProjectName { get; set; } = string.Empty;

        [Required(ErrorMessage = "StartDate is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [StartDateNotInPast(ErrorMessage = "StartDate cannot be in the past.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "DueDate is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DateGreaterThan(nameof(StartDate))]
        public DateTime DueDate { get; set; }

        [StringLength(500, ErrorMessage = "ProjectDescription cannot be longer than 500 characters.")]
        public string ProjectDescription { get; set; } = string.Empty;

        public ProjectStatus ProjectStatus { get; set; } = ProjectStatus.Pending;

        [EnsureAtLeastOneElement(ErrorMessage = "At least one ProjectAssignee is required.")]
        public ICollection<Guid> ProjectAssignees { get; set; } = new List<Guid>();
    }
}
