using System.ComponentModel.DataAnnotations;
using static EnsureAtLeastOneElementAttribute;

namespace TracksifyAPI.Dtos.ProjectUpdates
{
    /*
     * CreateProjectUpdateDto - Request Dto to create ProjectUpdate from client
     */
    public class CreateProjectUpdateDto
    {
        [Required(ErrorMessage = "CheckIn is required.")]
        public DateTime CheckIn { get; set; }

        [Required(ErrorMessage = "CheckOut is required.")]
        [CheckInCheckOutValidation]
        public DateTime CheckOut { get; set; }

        [Required(ErrorMessage = "Workdone is required.")]
        [StringLength(500, ErrorMessage = "WorkDone cannot be longer than 500 characters.")]
        public string WorkDone { get; set; } = string.Empty;

        [Required(ErrorMessage = "ProjectId is required.")]
        public Guid ProjectId { get; set; }
    }
}
