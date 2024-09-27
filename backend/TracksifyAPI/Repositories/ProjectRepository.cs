using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TracksifyAPI.Data;
using TracksifyAPI.Helpers;
using TracksifyAPI.Interfaces;
using TracksifyAPI.Models;

namespace TracksifyAPI.Repositories
{
    /* ProjectRepository implemments the methods defined in the interface repository */
    public class ProjectRepository : IProjectRepository
    {
        private readonly TracksifyDataContext _context;

        // constructor
        public ProjectRepository(TracksifyDataContext context)
        {
            _context = context;            
        }

        /*
         * GetAllProjectsAsync - Asynchronously Gets all the projects in the database.
         * It does this based on some defined query object
         * @query: query parameter specified in the ProjectQueryObject class
         * Return: Returns the result based on the query. If no query is specified it returns all
         */
        public async Task<List<Project>> GetAllProjectsAsync(ProjectQueryObject query)
        {
         /*
         * * Query all projects
         * Get all projects from db context and store in a queryable form
         * Queryable means you can ask it for data and it can return data */

            IQueryable<Project> projects = _context.Projects;

            // Search for all projects that match the project name, status or date passed in
            if (!string.IsNullOrWhiteSpace(query.ProjectName))
            {
                projects = projects.Where(p => p.ProjectName.Contains(query.ProjectName));
            }
            if (query.ProjectStatus != null)
            {
                projects = projects.Where(p => p.ProjectStatus == query.ProjectStatus);
            }

            if (query.StartDate != null)
            {
                projects = projects.Where(p => p.StartDate == query.StartDate);
            }

            if (query.DueDate != null)
            {
                projects = projects.Where(p => p.DueDate == query.DueDate);
            }

            return await projects.Include(p => p.ProjectAssignees)
                                 .Include(p => p.ProjectUpdates)
                                 .ToListAsync();
        }
        /**
          * GetProjectByProjectIdASync - Asynchronously Gets a Project by their Global Unique Identifier
          *  @projectId: projectId of the projectId to be retrieved. This would be gotten from the url
          * Return: returns a Project or NULL
          */
        public async Task<Project?> GetProjectByProjectIdASync(Guid projectId)
        {
            return await _context.Projects
                                 .Include(p => p.ProjectAssignees)
                                 .Include(p => p.ProjectUpdates)
                                 .FirstOrDefaultAsync(p => p.ProjectId == projectId);
        }
        public async Task<List<Project>> GetProjectByUserIdASync(Guid userId)
        {
            return await _context.Projects
                         .Include(p => p.ProjectAssignees)
                         .Where(p => p.ProjectAssignees
                                      .Any(a => a.UserId == userId))
                         .ToListAsync();
        }

            /**
             * ProjectExistsASync - Asynchronously checks that a project exists based on the ID
             * @projectId: projectId of the project that wants to check if it exists
             * Return: True or False
             */
            public async Task<bool> ProjectExistsASync(Guid projectId)
        {
            return await _context.Projects
                                 .AnyAsync(p => p.ProjectId == projectId);

        }

        /**
        * GetProjectAssigneesASync - Asynchronously gets the users assigned to a new project
        * @projectId: projectid of the project in which the users would be retrieved from
        * Return: returns an icollection of users 
        */
        public async Task<ICollection<User>> GetProjectAssigneesASync(Guid projectId)
        {
            // Get all projects from db context, get project matching project id and bring out the assignees in that instance
            var projectassignees = await _context.Projects
                                                 .Where(p => p.ProjectId == projectId)
                                                 .Select(o => o.ProjectAssignees)
                                                 .FirstOrDefaultAsync();

            if (projectassignees == null)
            {
                throw new Exception("Project not found");
            }

            return projectassignees;
        }

        // POST Methods

        /**
        * CreateProjectASync - Asynchronously creates a new project
        * @project: project to be created
        * Return: returns a new Project
        */
        public async Task<Project> CreateProjectASync(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return (project);
        }

        public async Task<Project?> UpdateProjectASync(Guid projectId, Project project)
        {
            var existingProject = await _context.Projects
                                                .FirstOrDefaultAsync(p => p.ProjectId == projectId);

            if (existingProject == null)
            {
                return null;
            }

            existingProject.ProjectName = project.ProjectName;
            existingProject.ProjectDescription = project.ProjectDescription;
            existingProject.StartDate = project.StartDate;
            existingProject.DueDate = project.DueDate;
            existingProject.ProjectStatus = project.ProjectStatus;
            existingProject.ProjectAssignees = project.ProjectAssignees;

            await _context.SaveChangesAsync();
            return (existingProject);
        }

        public async Task DeleteProjectAsync(Project project)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
    }


}
