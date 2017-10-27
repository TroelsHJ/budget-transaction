using budget_transaction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace budget_transaction.Controllers
{
    public class DefaultController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetTransaction()
        {
            List<Transaction> transactions = Database.SelectAllTransactions(true);

            return Ok(transactions);
        }

    }
}
