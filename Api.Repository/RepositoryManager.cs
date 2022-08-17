using Api.Contract;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        Lazy<IAuthRepository> _authRepository;
        Lazy<ICourseRepository> _courseRepository;
        Lazy<IDiscussionRepository> _discussionRepository;
        Lazy<IRatingRepository> _ratingRepository;    

        public IAuthRepository AuthRepository => _authRepository.Value;
        public ICourseRepository CourseRepository => _courseRepository.Value;
        public IDiscussionRepository DiscussionRepository => _discussionRepository.Value;
        public IRatingRepository RatingRepository => _ratingRepository.Value;

        public RepositoryManager(MySqlConnection sqlConnection)
        {
            _authRepository = new Lazy<IAuthRepository>(() => new AuthRepository(sqlConnection));
            _courseRepository = new Lazy<ICourseRepository>(() => new CourseRepository(sqlConnection)); 
            _discussionRepository = new Lazy<IDiscussionRepository>(() => new DiscussionRepository(sqlConnection));
            _ratingRepository = new Lazy<IRatingRepository>(() => new RatingRepository(sqlConnection));
        }
    }
}
