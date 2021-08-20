using ContragentsCheck.Uipath.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContragentsCheck.Uipath
{
    public class OrchestratorQueueActivity: IOrchestratorActivitiy<QueueItem>
    {
        public OrchestratorConnection Connection { get; }
        public OrchestratorQueueActivity(OrchestratorConnection con)
        {
            Connection = con;
        }
        /// <summary>
        /// Отсылает в очередь Queue Item
        /// </summary>
        /// <param name="queueItem"></param>
        /// <returns>Возвращает Response от запроса</returns>
        public async Task<IRestResponse> SendAsync(QueueItem queueItem)
        {
            var request = new RestRequest("odata/Queues/UiPathODataSvc.AddQueueItem", Method.POST);
            request.AddHeader("Authorization", string.Format("Bearer {0}", Connection.Token));
            request.AddJsonBody(new
            {
                itemData = new
                {
                    Name = queueItem.QueueName,
                    Priority = queueItem.Priority,
                    SpecificContent = queueItem.QueueObject
                }
            }); 

            var response = await Connection.client.ExecuteAsync(request);
            return response;
        }
    }
}
