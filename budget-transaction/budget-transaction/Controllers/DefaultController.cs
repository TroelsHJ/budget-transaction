using budget_transaction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;


namespace budget_transaction.Controllers
{
    public class DefaultController : ApiController
    {
        [HttpGet]
        public IHttpActionResult ListTransaction()
        {
            List<Transaction> transactions = Database.SelectAllTransactions(true);

            return Ok(transactions);
        }

        [Route("api/Default/GeneralInformation/{id}")]
        [HttpGet]
        public IHttpActionResult GeneralInformation(int id)
        {
            Transaction d = Database.SelectTransaction(id, false);

            string json = JsonConvert.SerializeObject(d);
            
            return Ok(json);

        }


    }


}
