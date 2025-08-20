using FinShark.Interfaces;
using FinShark.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinShark.Controllers
{
    [Route("api/[comment]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
         var comments = await _commentRepository.GetAllCommentAsync();

         var commentDto = comments.Select(c => c.ToCommentDto());

         return Ok(commentDto);

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var comment = await _commentRepository.GetCommentAsync(id);

            if (comment == null)
            {
                return null;
            }

            return Ok(comment.ToCommentDto());

        }

    }
}
