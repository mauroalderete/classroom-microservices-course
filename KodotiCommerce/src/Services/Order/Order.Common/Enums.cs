using System;

namespace Order.Common
{
    public class Enums
    {
        public enum OrderStatus
        {
            Pending = 1,
            Completed = 2,
            Cancelled = 3
        }

        public enum OrderPayment
        {
            CreditCard = 1,
            Paypal = 2,
            Cash = 3
        }
    }
}
