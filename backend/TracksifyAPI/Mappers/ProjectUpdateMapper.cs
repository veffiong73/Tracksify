using TracksifyAPI.Dtos.ProjectUpdates;
using TracksifyAPI.Models;

namespace TracksifyAPI.Mappers
{
    public static class ProjectUpdateMapper
    {
        /**
         * ToProjectUpdateDto - Converts a ProjectUpdate object to Dto
         * @projectUpdate: ProjectUpdate object to be converted
         * Return: Returns a new projectUpdateDto
         */
        public static ProjectUpdateDto ToProjectUpdateDto(this ProjectUpdate projectUpdate)
        {
            return new ProjectUpdateDto
            {
                ProjectUpdateId = projectUpdate.ProjectUpdateId,
                CheckIn = projectUpdate.CheckIn,
                DateCreated = projectUpdate.DateCreated,
                CheckOut = projectUpdate.CheckOut,
                WorkDone = projectUpdate.WorkDone,
                ProjectId = projectUpdate.ProjectId,
                UserId = projectUpdate.UserId
            };
        }

        /**
         * ToProjectUpdateFromCreateProjectUpdateDto - Creates a ProjectUpdate object from a request Dto
         * @createProjectUpdateRequestDto: Request Dto from client to be converted to a ProjectUpdate
         * @userId: User creating the ProjectUpdate
         * @projectId: project in which ProjectUpdate is being created for
         * Return: Creates a new ProjectUpdate object
         */
        public static ProjectUpdate ToProjectUpdateFromCreateProjectUpdateDto(this CreateProjectUpdateDto createProjectUpdateRequestDto,
                                                                              Guid userId, Guid projectId)
        {
            return new ProjectUpdate
            {
                CheckIn = createProjectUpdateRequestDto.CheckIn,
                CheckOut = createProjectUpdateRequestDto.CheckOut,
                WorkDone = createProjectUpdateRequestDto.WorkDone,
                ProjectId = projectId,
                UserId = userId
            };
        }
    }
}
