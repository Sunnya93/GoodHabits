namespace GoodHabits.Data.Context
{
    public class AppDbContext : DbContext
    {
        #region Contructor
        public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
        {
           
        }
        #endregion

        #region Public properties
        public DbSet<PrayModel> Pray { get; set; } = null!;
        public DbSet<TodoModel> Todo { get; set; } = null!;

        #endregion

        #region Overidden methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PrayModel>().ToTable("Pray");
            modelBuilder.Entity<PrayModel>().HasKey(p => p.Id);


            modelBuilder.Entity<TodoModel>().ToTable("Todo");
            modelBuilder.Entity<TodoModel>().HasKey(p => p.Id);

            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
