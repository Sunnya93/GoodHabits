namespace GoodHabits.Data.Service
{
    public class PrayService
    {
        #region Private members
        private readonly ISqliteWasmDbContextFactory<AppDbContext> dbContext;
        #endregion
        #region Constructor
        public PrayService(ISqliteWasmDbContextFactory<AppDbContext> dbContext)
        {
            this.dbContext = dbContext;
        }
        #endregion
        #region Public methods
        /// <summary>
        /// This method returns the list of product
        /// </summary>
        /// <returns></returns>
        public async Task<List<PrayModel>> GetAll()
        {
            using var ctx = await dbContext.CreateDbContextAsync();

            return ctx.Pray!.ToList();
        }
        /// <summary>
        /// This method add a new product to the DbContext and saves it
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<PrayModel> AddAsync(PrayModel model)
        {
            try
            {
                using var ctx = await dbContext.CreateDbContextAsync();

                ctx.Pray!.Add(model);

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
        public async Task<PrayModel> UpdateAsync(PrayModel model)
        {
            try
            {
                using var ctx = await dbContext.CreateDbContextAsync();

                var modelExist = ctx.Pray!.FirstOrDefault(p => p.Id == model.Id);
                if (modelExist != null)
                {
                    modelExist.Type = model.Type;
                    modelExist.Content = model.Content;
                    modelExist.PrayDate = model.PrayDate;
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
        public async Task DeleteAsync(PrayModel model)
        {
            try
            {
                using var ctx = await dbContext.CreateDbContextAsync();
                ctx.Pray!.Remove(model);
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
