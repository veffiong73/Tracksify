using Microsoft.AspNetCore.Mvc;
using TracksifyAPI.Helpers;
using TracksifyAPI.Interfaces;
using TracksifyAPI.Mappers;
using TracksifyAPI.Models;
using TracksifyAPI.Repositories;

namespace TracksifyAPI.Controllers
{
    [Route("api/project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [HttpGet]
        // get all projects
        public async Task<IActionResult> GetProjects([FromRoute] ProjectQueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var projects = await _projectRepository.GetAllProjectsAsync(query);

            var projectDto = projects.Select(p => p.ToProjectDto());

            return Ok(projectDto);
        }

        [HttpGet("{projectId:Guid}")]
        public async Task<IActionResult> GetProjectByProjectId([FromRoute] Guid projectId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _projectRepository.ProjectExistsASync(projectId))
            {
                var project = await _projectRepository.GetProjectByProjectIdASync(projectId);

                var projectDto = project!.ToProjectDto();
                return Ok(projectDto);
            }
            return NotFound();
        }

        /*        [HttpGet("{projectId:Guid}")]
                public async Task<IActionResult> GetProjectByProjectId([FromQuery] ProjectQueryObject Guid)
                {
                    if (!ModelState.IsValid)
                        return BadRequest(ModelState);

                    if (await _projectRepository.ProjectExistsASync(Guid))
                    {
                        var project = await _projectRepository.GetProjectByProjectIdASync(Guid);

                        var projectDto = project!.ToProjectDto();
                        return Ok(projectDto);
                    }
                    return NotFound();
                }*/

        //        GetProjectByProjectId, GetProjectByUserId(Guid userId); GetProjectByStartDate(string startDate);
        // ProjectExistsASync(ProjectQueryObject query), GetProjectAssigneesASync(Guid projectId);

    }
}
