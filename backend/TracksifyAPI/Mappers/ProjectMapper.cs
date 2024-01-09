using TracksifyAPI.Dtos.Project;
using TracksifyAPI.Models;

namespace TracksifyAPI.Mappers
{
    public static class ProjectMapper
    {
        public static ProjectDto ToProjectDto(this Project projectModel)
        {
            return new ProjectDto
            {
                ProjectId = projectModel.ProjectId,
                ProjectName = projectModel.ProjectName,
                StartDate = projectModel.StartDate,
                DueDate = projectModel.DueDate,
                ProjectDescription = projectModel.ProjectDescription,
                ProjectStatus = projectModel.ProjectStatus,
            };
        }
    }
}
