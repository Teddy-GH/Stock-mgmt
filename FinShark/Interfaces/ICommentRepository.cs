using FinShark.Models;

namespace FinShark.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>>  GetAllCommentAsync();
        Task<Comment?> GetCommentAsync(int id);
        Task<Comment> CreateAsync(Comment comment);

    }
}
