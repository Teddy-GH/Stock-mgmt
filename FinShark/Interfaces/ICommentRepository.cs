using FinShark.Dtos.Comment;
using FinShark.Models;

namespace FinShark.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>>  GetAllCommentAsync();
        Task<Comment?> GetCommentAsync(int id);
        Task<Comment> CreateAsync(Comment comment);

        Task<Comment> UpdateComment(int id, Comment comment);

        Task<Comment> DeleteCommentAsync(int id);

    }
}
