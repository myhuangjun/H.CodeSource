namespace User.WebApi
{
    [AttributeUsage(AttributeTargets.Method)]
    public class UnitOfWorkAttribute:Attribute
    {
        public Type[] DbContext { get; init; }
        public UnitOfWorkAttribute(params Type[] dbContext)
        {
            DbContext = dbContext;
        }
    }
}
