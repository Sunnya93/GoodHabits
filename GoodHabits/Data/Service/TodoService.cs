namespace GoodHabits.Data.Service
{
    public class TodoService
    {
        #region Private members
        private readonly ISqliteWasmDbContextFactory<AppDbContext> dbContext;
        #endregion
        #region Constructor
        public TodoService(ISqliteWasmDbContextFactory<AppDbContext> dbContext)
        {
            this.dbContext = dbContext;
        }
        #endregion
        #region Public methods
        /// <summary>
        /// This method returns the list of product
        /// </summary>
        /// <returns></returns>
        public async Task<List<TodoModel>> GetAll()
        {
            using var ctx = await dbContext.CreateDbContextAsync();

            //ctx.Todo.Add(new TodoModel { Title = "test", Content = "content test", TodoDate = DateTime.Now, InsertTime = DateTime.Now, UpdateTime = DateTime.Now });
            //ctx.SaveChanges();

            return ctx.Todo!.ToList();
        }
        /// <summary>
        /// This method add a new product to the DbContext and saves it
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<TodoModel> AddAsync(TodoModel model)
        {
            try
            {
                using var ctx = await dbContext.CreateDbContextAsync();
                ctx.Todo!.Add(model);

                await ctx.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }
        /// <summary>
        /// This method update and existing product and saves the changes
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<TodoModel> UpdateAsync(TodoModel model)
        {
            try
            {
                using var ctx = await dbContext.CreateDbContextAsync();

                var modelExist = ctx.Todo!.FirstOrDefault(p => p.Id == model.Id);
                if (modelExist != null)
                {
                    modelExist.Title = model.Title;
                    modelExist.Content = model.Content;
                    modelExist.TodoDate = model.TodoDate;
                    modelExist.InsertTime = model.InsertTime;
                    modelExist.UpdateTime = DateTime.Now;
                    ctx.Update(modelExist);
                    await ctx.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }
        /// <summary>
        /// This method removes and existing product from the DbContext and saves it
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task DeleteAsync(TodoModel model)
        {
            try
            {
                using var ctx = await dbContext.CreateDbContextAsync();
                ctx.Todo!.Remove(model);
                await ctx.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
