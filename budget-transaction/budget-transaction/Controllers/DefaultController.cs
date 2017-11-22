using budget_transaction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;

namespace budget_transaction.Controllers
{
    public class DefaultController : ApiController
    {

        [Route("api/Default/ListTransaction")]
        [HttpGet]
        public IHttpActionResult ListTransaction()
        {
            List<Transaction> transactions = Database.SelectAllTransactions(true);
            return Ok(transactions);
        }


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
        // SE DET HER!!! 
        // https://www.rabbitmq.com/tutorials/tutorial-four-dotnet.html
        public static void Send(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                UserName = "1doFhxuC",
                Password = "WGgk9kXy_wFIFEO0gwB_JiDuZm2-PrlO",
                Port = 10802,
                VirtualHost = "SDU53lDhKShK",
                HostName = "black-ragwort-810.bigwig.lshift.net",
                //Rabbit path amqp://1doFhxuC:WGgk9kXy_wFIFEO0gwB_JiDuZm2-PrlO@black-ragwort-810.bigwig.lshift.net:10802/SDU53lDhKShK
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "graph_transaction", durable: false, exclusive: false, autoDelete: false);

                var message = "Hello this is a test";
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "Rapid", routingKey: "graph_transaction_result", basicProperties: null, body: body);

                Console.WriteLine(" [x] Sent '{0}'", message);
            }

            //Console.WriteLine(" Press [enter] to exit.");
            //Console.ReadLine();
        }


        public static void Consume()
        {
            var factory = new ConnectionFactory()
            {
                UserName = "1doFhxuC",
                Password = "WGgk9kXy_wFIFEO0gwB_JiDuZm2-PrlO",
                Port = 10803,
                VirtualHost = "SDU53lDhKShK",
                HostName = "black-ragwort-810.bigwig.lshift.net",
                //Rabbit path "amqp://1doFhxuC:WGgk9kXy_wFIFEO0gwB_JiDuZm2-PrlO@black-ragwort-810.bigwig.lshift.net:10803/SDU53lDhKShK"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "graph_transaction", durable: false, exclusive: false, autoDelete: false);

                channel.QueueBind(queue: "graph_transaction", exchange: "Rapid", routingKey: "graph_transaction_request");


                Console.WriteLine(" [*] Waiting for messages.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                };
                channel.BasicConsume(queue: "graph_transaction", consumer: consumer);
            }
        }
    }
}
