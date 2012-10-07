using Raven.Client;

namespace RepoT.RavenDb.ConsoleSample
{
    public class UserEntityRepository : EntityRepository<User>, IUserRepository
    {
        public UserEntityRepository(IDatabaseFactory<IDocumentSession> databaseFactory)
            : base(databaseFactory)
        {
        }

        public override void Update(User entity)
        {
            //TODO:
        }
    }
}