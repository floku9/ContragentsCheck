using ContragentsCheck.Uipath.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace ContragentsCheck.Uipath
{
    public class OrchestratorConnection
    {
        internal readonly string Token;

        internal RestClient client;
        public OrchestratorConnection(Uri orchestratorUrl, string username, string pass, string tenant, bool disableSSL = false)
        {
            client = new RestClient(orchestratorUrl);
            if (disableSSL) 
            {
                //Чтоб не ругался на ssl сертификаты
                ServicePointManager.ServerCertificateValidationCallback +=
            (sender, certificate, chain, sslPolicyErrors) => true;
            }

            //Получение Bearer токена
            var request = new RestRequest("api/Account/Authenticate/", Method.POST);
            request.AddJsonBody(new 
            { 
                tenancyName = tenant, 
                usernameOrEmailAddress = username, 
                password = pass 
            });
            request.RequestFormat = DataFormat.Json;

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                SuccessResponse succ = JsonConvert.DeserializeObject<SuccessResponse>(response.Content);
                Token = succ.result;
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                ErrorResponse err = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
                throw new Exception($"{response.StatusCode}\n{err.errorCode} : {err.message}");
            }

            else 
                throw new Exception(response.ErrorException.Message);

        }
    }
}
