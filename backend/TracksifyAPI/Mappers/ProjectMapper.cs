using System.Collections.Generic;
using TracksifyAPI.Data;
using TracksifyAPI.Dtos.Project;
using TracksifyAPI.Interfaces;
using TracksifyAPI.Models;
using TracksifyAPI.Repositories;

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
                //ProjectAssignees = new List<User>(projectModel.ProjectAssignees), 
                //ProjectAssignees = projectModel.ProjectAssignees.Select(u => new AddUserToProjectDto { UserId = u.UserId }).ToList()
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
                 ProjectDescription = projectDto.ProjectDescription,
                 // we should be able to get the user by user Id method in user repo
                // ProjectAssignees = projectDto.ProjectAssignees.g,
            };
        }
    }
}
