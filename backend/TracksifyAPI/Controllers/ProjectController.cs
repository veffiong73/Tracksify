using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TracksifyAPI.Dtos.Project;
using TracksifyAPI.Helpers;
using TracksifyAPI.Interfaces;
using TracksifyAPI.Mappers;


namespace TracksifyAPI.Controllers
{
    /**
     * ProjectController - Controller class for the Project endpoints. Serves as a gateway to the endpoints.
     * Inherits: This class inherits from the ControllerBase class. Gives access to the Attributes and Methods.
     */
    [Route("tracksify/project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        public ProjectController(IProjectRepository projectRepository, IUserRepository userRepository)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        /**
         * GetProjects - Gets all the projects in the database based on some defined query object.
         * @param query: Query parameter specified in the QueryObject class.
         * @return: Returns the result based on the query. If no query is specified, it returns all projects.
         */
        [HttpGet]
        // Get all projects
        public async Task<IActionResult> GetProjects([FromQuery] ProjectQueryObject query)
        {
            // Checks for validation errors. Returns bool.
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
       
                // Gets all the projects from the database
                var projects = await _projectRepository.GetAllProjectsAsync(query);

                // Checks if projects is empty
                if (!projects.Any())
                {
                    return BadRequest("No projects were found.");
                }

                // Maps each project to its Dto
                var projectDto = projects.Select(p => p.ToProjectDto());

                return Ok(projectDto);
        }
        /**
         * GetProjectByProjectId - Gets a Project by their Global Unique Identifier.
         * @param projectId: ProjectId of the project to be retrieved. This would be gotten from the url.
         * @return: If the project exists, it returns the project. Otherwise, it returns NotFound().
         */
        [HttpGet("{projectId:Guid}")]
        public async Task<IActionResult> GetProjectByProjectId([FromRoute] Guid projectId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Checks if project with project Id passed in exists
            // Maps project to projectDto and returns project Dto
            if (await _projectRepository.ProjectExistsASync(projectId))
            {
                var project = await _projectRepository.GetProjectByProjectIdASync(projectId);

                var projectDto = project!.ToProjectDto();
                return Ok(projectDto);
            }
            return NotFound("Project does not exist.");
        }

        [HttpGet("user-project/{userId:Guid}")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> GetProjectByUserId([FromRoute] Guid userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Checks if user with user Id passed in exists
            // Maps user to userDto and returns user Dto
            if (await _userRepository.UserExistsAsync(userId))
            {
                var projects = await _projectRepository.GetProjectByUserIdASync(userId);

                var projectDto = projects!.Select(p => p.ToProjectDto());
                return Ok(projectDto);
            }
            return NotFound();
        }

        /**
         * GetProjectByCurrentlyLoggedInUserId - Gets projects based on currently logged in user
         * Return: returns projectDto or Error
         */
        [HttpGet("loggedIn-user-project")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> GetProjectByCurrentlyLoggedInUserId()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // getting ID of the currently logged in user
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);


            // Checks if user with user Id passed in exists
            // Maps user to userDto and returns user Dto
            if (await _userRepository.UserExistsAsync(userId))
            {
                var projects = await _projectRepository.GetProjectByUserIdASync(userId);

                var projectDto = projects!.Select(p => p.ToProjectDto());
                return Ok(projectDto);
            }
            return NotFound();
        }

        /**
         * CreateProject - Creates a new Project
         * @projectCreateDto: The request Dto that will be mapped to a Project object.
         * Return: Returns a Project Dto
         */
        [HttpPost]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto projectCreateDto)
        {
            // Checks for validation errors. Returns bool.
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Converts projectDto made during creation of project to projectObject
            var project = projectCreateDto.ToProjectFromCreateProjectDto();

            // fetch the userobject for the ids in projectDTo.projectassignees
            foreach (var userId in projectCreateDto.ProjectAssignees)
            {
                var user = await _userRepository.GetUserByIdAsync(userId);
                if (user == null)
                {
                    return BadRequest($"User with ID {userId} not found.");
                }
                project.ProjectAssignees.Add(user);
            };

            // Create a new project in the repository
            await _projectRepository.CreateProjectASync(project);

            // Creates project id and returns project dto
            return CreatedAtAction(
                nameof(GetProjectByProjectId),
                new { projectId = Guid.NewGuid() },
                project.ToProjectDto());
        }

        [HttpPut]
        [Authorize(Roles = "Employer")]
        [Route("{projectId}")]
        public async Task<IActionResult> Update([FromRoute] Guid projectId, [FromBody] UpdateProjectDto projectUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _projectRepository.ProjectExistsASync(projectId))
            {
                var updatedProject = projectUpdateDto.ToProjectFromUpdateProjectDto();
                
                // fetch the project object for the ids in projectDTo.projectassignees
                foreach (var userId in projectUpdateDto.ProjectAssignees)
                {
                    var user = await _userRepository.GetUserByIdAsync(userId);
                    if (user == null)
                    {
                        return BadRequest($"User with ID {userId} not found.");
                    }
                    updatedProject.ProjectAssignees.Add(user);
                };

                // Update the existing project in the repository
                await _projectRepository.UpdateProjectASync(projectId, updatedProject);

                return Ok(updatedProject!.ToProjectDto());
            }

            return BadRequest("Project Not Found");
        }

        [HttpDelete]
        [Authorize(Roles = "Employer")]
        [Route("{projectId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid projectId)
        {
            // Checks for validation errors. Returns bool.
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // check if projectId passed in is present in the repository
            var project = await _projectRepository.GetProjectByProjectIdASync(projectId);
            if (project == null)
            {
                return NotFound("Project Not Found");
            }

            await _projectRepository.DeleteProjectAsync(project);
            return Ok(new
            {
                status = "success",
                message = "Project Deleted"
            });
        }
    }
}
