using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Contract
{
    public interface IRepositoryManager
    {
        IAuthRepository AuthRepository { get; }
        ICourseRepository CourseRepository { get; }
        IDiscussionRepository DiscussionRepository { get; }
        IRatingRepository RatingRepository { get; }
    }
}
