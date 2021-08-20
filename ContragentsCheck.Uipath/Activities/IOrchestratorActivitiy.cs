using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContragentsCheck.Uipath
{
    /// <summary>
    ///  Интерфейс для определения новых активностей.
    ///  Потом необходимо будет добавить метод GetAsync.
    /// </summary>
    /// <typeparam name="T">Тип объекта который необходимо послать в методе SendAsync</typeparam>
    public interface IOrchestratorActivitiy<T>
    {
        OrchestratorConnection Connection { get; }
        Task<IRestResponse> SendAsync(T objectToSend);
    }
}
