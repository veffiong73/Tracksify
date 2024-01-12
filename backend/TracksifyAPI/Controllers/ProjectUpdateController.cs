using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var projectUpdate = await _projectUpdateRepo.GetAllProjectUpdateAsync();
            var projectUpdateDto = projectUpdate.Select(p => p.ToProjectUpdateDto());

            return Ok(projectUpdateDto);
        }

        /*[HttpGet]
        [Route("{projectId : Guid}")]
        public async Task<IActionResult> GetProjectUpdatesByProjectId([FromRoute] Guid projectId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _projectUpdateRepo.ProjectUpdateExistsAsync(projectId))
            {
                var projectUpdates = await _projectUpdateRepo.GetProjectUpdatesByProjectUpdateIdAsync(projectId);
                return Ok(projectUpdates);
            }

            return NotFound();
        }*/

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
         * @userId: UserId of user creating the projectUpdate
         * @createProjectUpdateRequestDto - RequestDto to create a projectUpdate
         * Return: returns a ToProjectUpdateDto response to the client
         */
        [HttpPost]
        [Route("{projectId:Guid}/{userId:Guid}")]
        public async Task<IActionResult> CreateProjectUpdate([FromRoute] Guid projectId,
                                                             [FromRoute] Guid userId,
                                                             [FromBody] CreateProjectUpdateDto createProjectUpdateRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _userRepository.UserExistsAsync(userId))
                return BadRequest($"User with id : {userId} Not Found");
            if (!await _projectRepository.ProjectExistsASync(projectId))
                return BadRequest($"Project with id : {projectId} Not Found");

            var projectUpdate = createProjectUpdateRequestDto.ToProjectUpdateFromCreateProjectUpdateDto(userId, projectId);

            if (projectUpdate == null)
            {
                return BadRequest("Project Update is NULL");
            }

            await _projectUpdateRepo.CreateProjectUpdateAsync(projectUpdate);

            return CreatedAtAction(nameof(GetProjectUpdateById),
                                   new { projectUpdateId = Guid.NewGuid() },
                                   projectUpdate.ToProjectUpdateDto());
        }
    }
}
