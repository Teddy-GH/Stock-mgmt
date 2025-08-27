using FinShark.Dtos.Comment;
using FinShark.Interfaces;
using FinShark.Mappers;
using FinShark.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinShark.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepository.GetAllCommentAsync();

            var commentDto = comments.Select(c => c.ToCommentDto());

            return Ok(commentDto);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var comment = await _commentRepository.GetCommentAsync(id);

            if (comment == null)
            {
                return null;
            }

            return Ok(comment.ToCommentDto());

        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentRequestDto createCommentRequestDto)
        {
            var stock = _stockRepository.stockExists(stockId);

            if (stock == null)
            {
                return BadRequest("Stock doesn't exist");
            }

            var newComment = createCommentRequestDto.ToCommentFromCreate(stockId);

            await _commentRepository.CreateAsync(newComment);

            return CreatedAtAction(nameof(Get), new { id = newComment.Id }, newComment.ToCommentDto());


        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateCommentDto)
        {
            var commentModel = await _commentRepository.UpdateComment(id, updateCommentDto.ToCommentFromUpdate());

            if (commentModel == null)
            {
                return NotFound();
            }


            return Ok(commentModel.ToCommentDto());
        }

    

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var comment = await  _commentRepository.DeleteCommentAsync(id);

            if (comment == null)
            {
                return NotFound("Comment does not exist!");

            }

            return Ok(comment);
        }

    }
}
