using Domain.Interfaces;

namespace Domain
{
    public abstract class Repository<TContext> where TContext: IDbRepository
    {
        protected TContext Context { get; }

        protected Repository(IDbRepository context)
        {
            Context = (TContext)context;
        }
    }
}
