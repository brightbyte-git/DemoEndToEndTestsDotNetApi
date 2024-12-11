namespace E2EDemoUserRegistration;

public class DbContext : DbContext
{
    public KanbanDbContext(DbContextOptions<DbContext> options) : base(options) { }
    
    // TODO: Download the relevant nuget pakcages for using DBContext and EF Core

}