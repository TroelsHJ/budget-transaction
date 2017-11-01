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
        [Route("api/Default/ListTransaction")]
        [HttpGet]
        public IHttpActionResult ListTransaction()
        {
            List<Transaction> transactions = Database.SelectAllTransactions(true);
            Console.WriteLine("Bob");
            return Ok(transactions);
        }

        /*
        [Route("api/Default/ListTransaction/{start}/{end}")]
        [HttpGet]
        public IHttpActionResult ListTransaction(string start, string end)
        {
            //input format of start and end string = ddmmyyyy

            Console.WriteLine(start);
            Console.WriteLine(end);
            int startDate = Convert.ToInt32(start.Substring(0, 2));
            int startMonth = Convert.ToInt32(start.Substring(2, 2));
            int startYear = Convert.ToInt32(start.Substring(4, 4));
            DateTime s = new DateTime(startYear, startMonth, startDate);
            Console.WriteLine(s);

            int endDate = Convert.ToInt32(end.Substring(0, 2));
            int endMonth = Convert.ToInt32(end.Substring(2, 2));
            int endYear = Convert.ToInt32(end.Substring(4, 4));
            DateTime e = new DateTime(endYear, endMonth, endDate);
            Console.WriteLine(e);


            List<Transaction> transactions = Database.SelectAllTransactions(true, s, e);

            return Ok(transactions);
        }
        */
        [Route("api/Default/ListTransaction")]
        [HttpGet]
        public IHttpActionResult ListTransaction(string start, string end)
        {
            //input format of start and end string = ddmmyyyy

            Console.WriteLine(start);
            Console.WriteLine(end);
            int startDate = Convert.ToInt32(start.Substring(0, 2));
            int startMonth = Convert.ToInt32(start.Substring(2, 2));
            int startYear = Convert.ToInt32(start.Substring(4, 4));
            DateTime s = new DateTime(startYear, startMonth, startDate);
            Console.WriteLine(s);

            int endDate = Convert.ToInt32(end.Substring(0, 2));
            int endMonth = Convert.ToInt32(end.Substring(2, 2));
            int endYear = Convert.ToInt32(end.Substring(4, 4));
            DateTime e = new DateTime(endYear, endMonth, endDate);
            Console.WriteLine(e);


            List<Transaction> transactions = Database.SelectAllTransactions(true, s, e);

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
