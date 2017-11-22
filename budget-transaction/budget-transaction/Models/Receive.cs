using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;

namespace budget_transaction.Models
{
    public class Receive
    {
        public Receive()
        {
            Consume();
        }
        public void Send(string message)
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

                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "Rapid", routingKey: "graph_transaction_result", basicProperties: null, body: body);

                Console.WriteLine(" [x] Sent '{0}'", message);
            }
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
        public void Consume()
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
                    Console.WriteLine(body);
                    string message = Encoding.UTF8.GetString(body);
                    if (message.ToLower().Contains("listtransaction"))
                    {
                        List<Transaction> transactions = Database.SelectAllTransactions(true);
                        Send(transactions.ToString());
                    }
                };
                channel.BasicConsume(queue: "graph_transaction", consumer: consumer);
            }
        }
    }
}