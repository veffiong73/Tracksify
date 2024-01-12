namespace TracksifyAPI.Dtos.ProjectUpdates
{
    /*
     * CreateProjectUpdateDto - Request Dto to create ProjectUpdate from client
     */
    public class CreateProjectUpdateDto
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string WorkDone { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }
    }
}
