using TracksifyAPI.Data;
using TracksifyAPI.Models;

namespace TracksifyAPI
{
    public class Seed
    {
        private readonly TracksifyDataContext dataContext;

        public Seed(TracksifyDataContext context)
        {
            this.dataContext = context;
        }

        public async Task SeedDataContext()
        {
            try
            {
                // Checking if there are any users in the database
                if (!dataContext.Users.Any())
                {
                    // Creating users
                    var users = new List<User>
                    {
                        new User
                        {
                            UserId = Guid.NewGuid(),
                            FirstName = "John",
                            LastName = "Doe",
                            Email = "john.doe@example.com",
                            Password = "password",
                            Role = "Supervisor",
                            UserType = UserType.Admin
                        },
                        new User
                        {
                            UserId = Guid.NewGuid(),
                            FirstName = "Akan",
                            LastName = "Victor",
                            Email = "Akan.Victor@example.com",
                            Password = "password",
                            Role = "Designer",
                            UserType = UserType.Employee
                        },
                        new User
                        {
                            UserId = Guid.NewGuid(),
                            FirstName = "Bukola",
                            LastName = "Oyedepo",
                            Email = "Oyedepo.Bukola@example.com",
                            Password = "password",
                            Role = "Developer",
                            UserType = UserType.Employee
                        }
                    };

                    // Add users to the database
                    dataContext.Users.AddRange(users);
   

                    // Creating projects
                    var projects = new List<Project>
                    {
                        new Project
                        {
                            ProjectId = Guid.NewGuid(),
                            ProjectName = "Design Project",
                            StartDate = DateTime.UtcNow,
                            DueDate = DateTime.UtcNow.AddDays(7),
                            ProjectDescription = "Design a new logo",
                            ProjectStatus = ProjectStatus.InProgress,
                            // Assigning the first user to the project
                            ProjectAssignees = users.Take(1).ToList()
                        },
                        new Project
                        {
                            ProjectId = Guid.NewGuid(),
                            ProjectName = "Ecommerce site",
                            StartDate = DateTime.UtcNow,
                            DueDate = DateTime.UtcNow.AddDays(7),
                            ProjectDescription = "Design and build homepage",
                            ProjectStatus = ProjectStatus.Pending,
                            // Assigning the second and third users to the project
                            ProjectAssignees = users.Skip(1).Take(2).ToList()
                        }
                    };

                    // Add projects to the database
                    dataContext.Projects.AddRange(projects);
                    await dataContext.SaveChangesAsync();


                    // Creating project updates
                    var projectUpdates = new List<ProjectUpdate>
                    {
                        new ProjectUpdate
                        {
                            ProjectUpdateId = Guid.NewGuid(),
                            CheckIn = DateTime.UtcNow,
                            DateCreated = DateTime.UtcNow,
                            CheckOut = DateTime.UtcNow.AddHours(8),
                            WorkDone = "Unable to start project because the network shut down",
                            ProjectId = projects[0].ProjectId, // Use the first project
                            UserId = users[0].UserId // Use the first user
                        },
                        new ProjectUpdate
                        {
                            ProjectUpdateId = Guid.NewGuid(),
                            CheckIn = DateTime.UtcNow.AddMinutes(30),
                            DateCreated = DateTime.UtcNow,
                            CheckOut = DateTime.UtcNow.AddHours(8),
                            WorkDone = "Created figma design for ecommerce site",
                            ProjectId = projects[1].ProjectId, // Use the second project
                            UserId = users[1].UserId // Use the second user
                        }
                    };

                    // Adding project updates to the database
                    dataContext.ProjectUpdates.AddRange(projectUpdates);
                    await dataContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding data: {ex.Message}");
            }
        }
    }


}