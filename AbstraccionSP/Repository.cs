namespace EDOSwit.AbstraccionSP
{
    public abstract class Repository<T> : BaseRepository<T> where T : class, new()
    {
        protected Repository()
        {
        }

        protected Repository(string nameConnection)
            : base(nameConnection)
        {
        }

        protected string ConnectionString
        {
            get { return Database.ConnectionString; }
        }
    }
}