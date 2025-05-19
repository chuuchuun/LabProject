using LabProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LabProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private static List<Review> Reviews = new List<Review>
        {
            new Review
            {
                Id = 1,
                ClientId = 1,
                ProviderId = 1,
                Rating = 5,
                Comment = "Excellent service!",
                DatePosted = DateTime.UtcNow
            },
            new Review
            {
                Id = 2,
                ClientId = 2,
                ProviderId = 1,
                Rating = 4,
                Comment = "Very good experience.",
                DatePosted = DateTime.UtcNow.AddDays(-1)
            }
        };

        /// <summary>
        /// Gets all reviews in the system.
        /// </summary>
        /// <returns>A list of all reviews.</returns>
        /// <response code="200">Returns all reviews.</response>
        [HttpGet]
        public ActionResult<IEnumerable<Review>> GetReviews()
        {
            return Ok(Reviews);
        }

        /// <summary>
        /// Gets a specific review by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the review.</param>
        /// <returns>The review matching the ID.</returns>
        /// <response code="200">Returns the review with the specified ID.</response>
        /// <response code="404">No review found with the specified ID.</response>
        [HttpGet("{id}")]
        public ActionResult<Review> GetReviewById([FromRoute] int id)
        {
            var review = Reviews.FirstOrDefault(r => r.Id == id);
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
        public ActionResult<Review> CreateReview([FromBody] Review review)
        {
            review.Id = Reviews.Any() ? Reviews.Max(r => r.Id) + 1 : 1;
            review.DatePosted = DateTime.UtcNow;
            Reviews.Add(review);
            return CreatedAtAction(nameof(GetReviewById), new { id = review.Id }, review);
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
        public ActionResult UpdateReview([FromRoute] int id, [FromBody] Review review)
        {
            var reviewToUpdate = Reviews.FirstOrDefault(r => r.Id == id);
            if (reviewToUpdate is null)
            {
                return NotFound();
            }

            reviewToUpdate.ClientId = review.ClientId;
            reviewToUpdate.ProviderId = review.ProviderId;
            reviewToUpdate.Rating = review.Rating;
            reviewToUpdate.Comment = review.Comment;
            return Ok(reviewToUpdate);
        }

        /// <summary>
        /// Deletes a review by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the review to delete.</param>
        /// <returns>Status of the deletion.</returns>
        /// <response code="200">The review was deleted successfully.</response>
        /// <response code="404">No review found with the specified ID.</response>
        [HttpDelete("{id}")]
        public ActionResult DeleteReview([FromRoute] int id)
        {
            var review = Reviews.FirstOrDefault(r => r.Id == id);
            if (review is null)
            {
                return NotFound();
            }

            Reviews.Remove(review);
            return Ok();
        }
    }
}
