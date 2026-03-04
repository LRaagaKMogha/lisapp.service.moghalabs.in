using Service.Win.Common;
using System;

namespace Service.Win.Repository
{
    public class InterfaceHelper
    {
        public int PushOrders(ExternalOrderDTO order)
        {
            int result = 0;
            try
            {
                using (YMEntities context = new YMEntities())
                {
                    context.pro_InsertExternalOrders(order.PatientID, order.PatientName, order.Barcodeno, order.Sex, order.DOB, order.TestId, order.TestName);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
    }
}
