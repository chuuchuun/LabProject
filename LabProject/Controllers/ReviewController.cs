using LabProject.Application.Dtos.ReviewDtos;
using LabProject.Application.Interfaces;
using LabProject.Application.Services;
using LabProject.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabProject.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController(IReviewService reviewService) : ControllerBase
    {
        private readonly IReviewService _reviewService = reviewService;

        /// <summary>
        /// Gets all reviews in the system.
        /// </summary>
        /// <returns>A list of all reviews.</returns>
        /// <response code="200">Returns all reviews.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews()
        {
            return Ok(await _reviewService.GetAllAsync());
        }

        /// <summary>
        /// Gets a specific review by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the review.</param>
        /// <returns>The review matching the ID.</returns>
        /// <response code="200">Returns the review with the specified ID.</response>
        /// <response code="404">No review found with the specified ID.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDto>> GetReviewById([FromRoute] int id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            if (review is null)
            {
                return NotFound();
            }

            return Ok(review);
        }

        /// <summary>
        /// Creates a new review.
        /// </summary>
        /// <param name="review">The review data to create.</param>
        /// <returns>The created review with its assigned ID.</returns>
        /// <response code="201">The review was created successfully.</response>
        [HttpPost]
        public async Task<ActionResult<ReviewDto>> CreateReview([FromBody] ReviewCreateDto review)
        {
            if(review is null)
            {
                return BadRequest();
            }
            var newId = await _reviewService.AddAsync(review);
            if (newId <= 0)
            {
                return BadRequest("Failed to create review.");
            }
            var createdReview = await _reviewService.GetByIdAsync(newId);
            return CreatedAtAction(nameof(GetReviewById), new { id = newId }, createdReview);
        }

        /// <summary>
        /// Updates a specific review by ID.
        /// </summary>
        /// <param name="id">The ID of the review to update.</param>
        /// <param name="review">The updated review data.</param>
        /// <returns>The updated review.</returns>
        /// <response code="200">The review was updated successfully.</response>
        /// <response code="404">No review found with the specified ID.</response>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReview([FromRoute] int id, [FromBody] ReviewUpdateDto review)
        {
            if(review is null)
            {
                return BadRequest();
            }

            var success = await _reviewService.UpdateAsync(id, review);
            if (!success)
            {
                return NotFound();
            }

           var updatedReview = await _reviewService.GetByIdAsync(id);
            return Ok(updatedReview);
        }

        /// <summary>
        /// Deletes a review by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the review to delete.</param>
        /// <returns>Status of the deletion.</returns>
        /// <response code="200">The review was deleted successfully.</response>
        /// <response code="404">No review found with the specified ID.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReview([FromRoute] int id)
        {
            var success = await _reviewService.DeleteAsync(id);
            if (!success)
            {
                return NotFound($"Review with ID {id} not found.");
            }
            return Ok();
        }
    }
}
