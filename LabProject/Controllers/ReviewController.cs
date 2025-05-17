using LabProject.Models;
using Microsoft.AspNetCore.Mvc;

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
        /// <returns>A list of reviews.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Review>> GetReviews()
        {
            return Ok(Reviews);
        }
        /// <summary>
        /// Gets a specific review by ID.
        /// </summary>
        /// <param name="id">Review's unique identifier.</param>
        /// <returns>The requested review object, or 404 if not found.</returns>
        [HttpGet("{id}")]
        public ActionResult<Review> GetReviewById(int id)
        {
            var review = Reviews.FirstOrDefault(r => r.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            return Ok(review);
        }
        /// <summary>
        /// Creates a new review.
        /// </summary>
        /// <param name="review">The review object to create.</param>
        /// <returns>The created review with generated ID.</returns>
        [HttpPost]
        public ActionResult<Review> CreateReview(Review review)
        {
            review.Id = Reviews.Any() ? Reviews.Max(r => r.Id) + 1 : 1;
            review.DatePosted = DateTime.UtcNow;
            Reviews.Add(review);
            return CreatedAtAction(nameof(GetReviewById), new { id = review.Id }, review);
        }
        /// <summary>
        /// Updates a specific review by ID.
        /// </summary>
        /// <param name="id">Review's unique identifier.</param>
        /// <param name="review">The review object to use for update.</param>
        /// <returns>The updated review object.</returns>
        [HttpPut("{id}")]
        public ActionResult UpdateReview(int id, Review review)
        {
            var reviewToUpdate = Reviews.FirstOrDefault(r => r.Id == id);
            if (reviewToUpdate == null)
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
        /// <param name="id">Review's unique identifier.</param>
        /// <returns>No content if deletion is successful.</returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteReview(int id)
        {
            var review = Reviews.FirstOrDefault(r => r.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            Reviews.Remove(review);
            return NoContent();
        }
    }
}
