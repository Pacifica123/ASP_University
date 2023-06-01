using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ASP.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        [DisplayName("Имя")]
        public string Name { get; set; }
        public int? ContextId { get; set; } // определяет ID соответствующей таблицы Студентов или Преподавателей 
        public bool IsTeacher { get; set; } 
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            //сюда код с созданием БД 
            Database.CreateIfNotExists();
            // прописывание ролей
            if (!this.Roles.Any())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(this));
                /*АДМИН:
                 * - разрешается весь CRUD
                 * (за исключением создания пользователей...???)
                 * 
                 * СТУДЕНТ:
                 * - может смотреть все
                 * - запрещено создание и редактирование
                 * 
                 * ПРЕПОДАВАТЕЛЬ:
                 * - полный CRUD над расписанием и заданиями
                 * - редактирование предмета(...???)
                 * 
                 */
                roleManager.Create(new IdentityRole("Admin"));   //нет дополнительной записи
                roleManager.Create(new IdentityRole("Student")); //запись идет в Student с внешним ключом на GroupStud
                roleManager.Create(new IdentityRole("Teacher")); //запись идет в Teacher  
            }
            // сразу создаем Админа
            if (!this.Users.Any())
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this));
                var user = new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    Name = "Я",
                };
                var result = userManager.Create(user, "djdf123");
                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }
        }
        public DbSet<Cabinet> Cabinets { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<GroupStud> GroupStuds { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<ItemLesson> ItemLessons { get; set; }
        public DbSet<Rasp> Rasps { get; set; }
        public DbSet<Homework> Homeworks { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}