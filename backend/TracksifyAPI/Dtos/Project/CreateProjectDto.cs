using System.ComponentModel.DataAnnotations;
using static EnsureAtLeastOneElementAttribute;

namespace TracksifyAPI.Dtos.Project
{
    /**
     * CreateProjectDto - this is a request Dto to create a new Project
     */
    public class CreateProjectDto
    {
        /*public string ProjectName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public string ProjectDescription { get; set; } = string.Empty;
        public List<Guid> ProjectAssignees { get; set; } = new List<Guid>();*/

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

        [EnsureAtLeastOneElement(ErrorMessage = "At least one ProjectAssignee is required.")]
        public List<Guid> ProjectAssignees { get; set; } = new List<Guid>();
    }
}

