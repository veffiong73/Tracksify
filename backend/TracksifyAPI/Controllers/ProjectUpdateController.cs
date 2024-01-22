using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Security.Claims;
using TracksifyAPI.Dtos.ProjectUpdates;
using TracksifyAPI.Interfaces;
using TracksifyAPI.Mappers;

namespace TracksifyAPI.Controllers
{
    /**
     * ProjectUpdateController - Controller class for the ProjectUpdate endpoints. Serves as a gateway to the endpoints
     *  - Inherits: This class inherits from the ControllerBase class. Gives access to the Attributes
     */
    [Route("tracksify/projectUpdate")]
    [ApiController]
    public class ProjectUpdateController : ControllerBase
    {
        private readonly IProjectUpdateRepository _projectUpdateRepo;
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        public ProjectUpdateController(IProjectUpdateRepository projectUpdateRepo,
                                       IProjectRepository projectRepository,
                                       IUserRepository userRepository)
        {
            _projectUpdateRepo = projectUpdateRepo;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
        }
        /**
         * GetAll - Gets All the ProjectUpdates
         * Return: Returns a projectUpdateDto Response to the client
         */
        [HttpGet]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var projectUpdate = await _projectUpdateRepo.GetAllProjectUpdateAsync();
            var projectUpdateDto = projectUpdate.Select(p => p.ToProjectUpdateDto());

            return Ok(projectUpdateDto);
        }

        /**
         * GetProjectUpdatesByProjectId - Get's all project updates for all assignees to the project
         * @projectId: ProjectId of the project for which the project updates are being fetched
         * Return: Returns a list of projectUpdates OR error
         */
        [HttpGet]
        [Authorize(Roles = "Employer")]
        [Route("employer-getall-projectupdate/{projectId:Guid}")]
        public async Task<IActionResult> GetProjectUpdatesByProjectId([FromRoute] Guid projectId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _projectUpdateRepo.ProjectUpdateExistsAsync(projectId))
            {
                var projectUpdates = await _projectUpdateRepo.GetProjectUpdateByProjectIdAsync(projectId);
                return Ok(projectUpdates);
            }

            return NotFound();
        }

        /**
         * GetProjectUpdatesByUserId - Get's all project updates for all assignees to the project
         * @userId: UserId of the User for which his/her project updates are being fetched
         * Return: Returns a list of projectUpdates OR error
         */
        [HttpGet]
        [Authorize(Roles = "Employer")]
        [Route("employer/{userId:Guid}")]
        public async Task<IActionResult> GetProjectUpdatesByUserId([FromRoute] Guid userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _userRepository.UserExistsAsync(userId))
            {
                var projectUpdates = await _projectUpdateRepo.GetProjectUpdateByUserIdAsync(userId);
                return Ok(projectUpdates);
            }

            return NotFound();
        }

        /**
         * ------------------
         * GetProjectUpdatesByUserId - Get's all project updates for "a project" assigned to a user
         * @userId: UserId of the User for which his/her project updates are being fetched
         * @projectId: ProjectId to get updates for, for a particular user
         * Return: Returns a list of projectUpdates OR error
         */
        [HttpGet]
        [Authorize(Roles = "Employee")]
        [Route("employee-projectUpdate/project/{projectId:Guid}")]
        public async Task<IActionResult> GetUserProjectUpdatesForAProjectByProjectId([FromRoute] Guid projectId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);

            // Check if project Exists
            if (!await _projectRepository.ProjectExistsASync(projectId))
                return NotFound($"Project with id : {projectId} Not Found");

            if (await _userRepository.UserExistsAsync(userId))
            {
                var projectUpdates = await _projectUpdateRepo.GetUserProjectUpdateForSingleProjectByProjectIdAsync(userId, projectId);
                return Ok(projectUpdates);
            }

            return NotFound();
        }

        /**
         * GetProjectUpdateById - Gets a projectUpdate by it's projectUpdateId
         * @projectUpdateId: ProjectUpdateId of projectUpdate to be retrieved
         * Return: Returns a ToProjectUpdateDto response to the client
         */
        [HttpGet]
        [Route("{projectUpdateId:Guid}")]
        public async Task<IActionResult> GetProjectUpdateById([FromRoute] Guid projectUpdateId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _projectUpdateRepo.ProjectUpdateExistsAsync(projectUpdateId))
            {
                var projectUpdate = await _projectUpdateRepo.GetProjectUpdateByIdAsync(projectUpdateId);
                return Ok(projectUpdate!.ToProjectUpdateDto());
            }

            return NotFound();
        }

        /**
         * CreateProjectUpdate - Creates a new ProjectUpdate
         * @ProjectId: ProjectId to create a projectUpdate for
         * @createProjectUpdateRequestDto - RequestDto to create a projectUpdate
         * Return: returns a ToProjectUpdateDto response to the client
         */
        [HttpPost]
        [Authorize(Roles = "Employee")]
        [Route("employee/{projectId:Guid}")]
        public async Task<IActionResult> CreateProjectUpdate([FromRoute] Guid projectId,
                                                             [FromBody] CreateProjectUpdateDto createProjectUpdateRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            // getting ID of the currently logged in user
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return NotFound("User Claim returned null");

            var userId = Guid.Parse(userIdClaim.Value);

            // Check if project Exists
            if (!await _projectRepository.ProjectExistsASync(projectId))
                return NotFound($"Project with id : {projectId} Not Found");

            // checking that user exists
            if (!await _userRepository.UserExistsAsync(userId))
                return NotFound("User doesn't exists");


            var projectUpdate = createProjectUpdateRequestDto.ToProjectUpdateFromCreateProjectUpdateDto(userId, projectId);

            if (projectUpdate == null)
            {
                return BadRequest("Project Update is NULL");
            }

            // Checking that a ProjectUpdate hasn't be created on the same day by the same user
            if (await _projectUpdateRepo.ProjectUpdateExistsForThatDay(userId, projectId, projectUpdate.DateCreated))
                return Conflict("ProjectUpdate has been done for today");

            await _projectUpdateRepo.CreateProjectUpdateAsync(projectUpdate);

            return CreatedAtAction(nameof(GetProjectUpdateById),
                                   new { projectUpdateId = Guid.NewGuid() },
                                   projectUpdate.ToProjectUpdateDto());
        }
    }
}
