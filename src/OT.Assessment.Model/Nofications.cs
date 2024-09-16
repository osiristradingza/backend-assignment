using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Model
{
    public static class Nofications
    {
        public static string SuccessfulPublishedAccountMessage = "Account message sent to RabbitMQ";
        public static string GeneralExceptionMessage = "An unexpected error occurred. Please try again later.";
        public static string MessagingIsDisabled = "Please make sure you send a 'true' parameter to enable messaging.";
    }
}
