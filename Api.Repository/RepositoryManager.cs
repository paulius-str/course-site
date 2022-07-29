using Api.Contract;
using MySqlConnector;
using System.Data;

namespace Api.Repository
{
    public class RepositoryManager : IRepositoryManager, IDisposable
    {
        private readonly Lazy<IUserRepository> _authRepository;
        private readonly Lazy<ICourseRepository> _courseRepository;
        private readonly Lazy<IDiscussionRepository> _discussionRepository;
        private readonly Lazy<IRatingRepository> _ratingRepository;
        private readonly MySqlConnection _mySqlConnection;

        private MySqlTransaction? _transaction;
        private bool _transactionStarted;
        private bool _transactionCommited;

        public IUserRepository UserRepository => _authRepository.Value;
        public ICourseRepository CourseRepository => _courseRepository.Value;
        public IDiscussionRepository DiscussionRepository => _discussionRepository.Value;
        public IRatingRepository RatingRepository => _ratingRepository.Value;

        public RepositoryManager(MySqlConnection mySqlConnection)
        {
            _mySqlConnection = mySqlConnection;
            _authRepository = new Lazy<IUserRepository>(() => new UserRepository(_mySqlConnection, _transaction));
            _courseRepository = new Lazy<ICourseRepository>(() => new CourseRepository(_mySqlConnection, _transaction));
            _discussionRepository = new Lazy<IDiscussionRepository>(() => new DiscussionRepository(_mySqlConnection, _transaction));
            _ratingRepository = new Lazy<IRatingRepository>(() => new RatingRepository(_mySqlConnection, _transaction));
        }

        public async Task StartTransactionAsync()
        {
            if (_mySqlConnection.State != ConnectionState.Open)
                await _mySqlConnection.OpenAsync();
            
            _transaction = await _mySqlConnection.BeginTransactionAsync();
            _transactionStarted = true;
        }

        public async Task CommitTransactionAsync()
        {
            await _transaction.CommitAsync();
            await _mySqlConnection.CloseAsync();
            await _transaction.DisposeAsync();
            _transactionCommited = true;
        }

        public void Dispose()
        {
            if (_transactionStarted && !_transactionCommited)
            {
                _transaction?.Rollback();
                _mySqlConnection?.Close();
                _transaction?.Dispose();
                return;
            }

            _transaction?.Dispose();
        }
    }
}
