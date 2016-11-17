using NsdBooksTrainingApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NsdBooksTrainingApi.App_Start {
    public class ResetController : ApiController {
        private DB db = new DB();

        // GET: api/reset
        public string GetReset() {
            db.Database.ExecuteSqlCommand("DELETE FROM Books");
            return "Books table cleared.";
        }

    }
}
