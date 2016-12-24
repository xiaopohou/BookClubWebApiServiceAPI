using FinalProjectAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FinalProjectAPI.Controllers
{

    public class ReviewController : ApiController
    {
        ReviewDBDataContext db = new ReviewDBDataContext();

        // GET: api/Review
        public IEnumerable<Review> Get()
        {
            var reviews = db.Reviews;
            return reviews;
        }

        // GET: api/Review/5
        public Review Get(int id)
        {
            var queryReview = from a in db.Reviews
                            where a.Id == id
                            select a;

            Review review = queryReview.FirstOrDefault();
            return review;
        }

        // POST: api/Review
        public int Post([FromBody] Review newReview)
        {
            newReview.Date = DateTime.Now;
            db.Reviews.InsertOnSubmit(newReview);
            db.SubmitChanges();
            return newReview.Id;
        }

        // PUT: api/Review/5
        public int Put(int id, [FromBody]Review updatedReview)
        {
            var reviews = db.Reviews;
            Review result = new Review();
            var v = from a in reviews where a.Id == id select a;
            foreach (var i in v)
            {
                result = i;
            }
            result.Reviewer = updatedReview.Reviewer;
            result.Review1 = updatedReview.Review1;
            result.Date = DateTime.Now;
            result.Rating = updatedReview.Rating;
            result.BookID = updatedReview.BookID;
            db.SubmitChanges();
            return result.Id;
        }

        // DELETE: api/Review/5
        public void Delete(int id)
        {
            var review = from a in db.Reviews
                       where a.Id == id
                       select a;
            Review target = review.FirstOrDefault();

            db.Reviews.DeleteOnSubmit(target);
            db.SubmitChanges();
        }
    }
}
