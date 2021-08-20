using System;
using System.Collections.Generic;
using System.Text;

namespace ContragentsCheck.Uipath.Models
{
    public class QueueItem
    {
        /// <summary>
        /// Название очереди в UiPath
        /// </summary>
        public string QueueName { get; set; }

        /// <summary>
        /// Приоритет джобы
        /// </summary>
        public string Priority { get; set; }
        /// <summary>
        /// Объект, который нужно добавить в очередь
        /// </summary>
        public object QueueObject { get; set; }
    }
}
