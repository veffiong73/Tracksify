using TracksifyAPI.Dtos.Project;
using TracksifyAPI.Models;

namespace TracksifyAPI.Mappers
{
    /**
     * ProjectMapper - Helps in mapping objects to Dtos' and Dtos' back to Project Objects
     */
    public static class ProjectMapper
    {
        /**
         * ToProjectDto - Converts a Project Object to a Dto
         * @projectModel: Project object to be converted
         */

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
                ProjectAssignees = projectModel.ProjectAssignees.Select(u => u.UserId).ToList(),
            };
        }

        /**
         * ToProjectFromCreateProjectDto - Converts a create project request Dto to a Project object
         * 
         */
        public static Project ToProjectFromCreateProjectDto(this CreateProjectDto projectDto) 
        {
            return new Project
            {
                 ProjectName = projectDto.ProjectName,
                 StartDate = projectDto.StartDate,
                 DueDate = projectDto.DueDate,
                 ProjectDescription = projectDto.ProjectDescription
            };
        }

        public static Project ToProjectFromUpdateProjectDto(this UpdateProjectDto projectDto)
        {
            return new Project
            {
                ProjectName = projectDto.ProjectName,
                StartDate = projectDto.StartDate,
                DueDate = projectDto.DueDate,
                ProjectStatus = projectDto.ProjectStatus,
                ProjectDescription = projectDto.ProjectDescription
            };
        }
    }
}
