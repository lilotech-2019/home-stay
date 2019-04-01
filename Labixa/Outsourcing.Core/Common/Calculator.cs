using Outsourcing.Data.Models.HMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Core.Common
{
    public class Calculator
    {
        public static double SumOfCostOrder(CostOrder costOrder)
        {
            double result = 0.0;
            foreach (var item in costOrder.CostOrderItems)
            {
                result = result + (item.Quantity * item.Price);
            }
            return result;
        }
        public static double CalcNumOfDay(DateTime CheckIn, DateTime CheckOut)
        {
            var result = CheckOut.Date.Subtract(CheckIn.Date);
            return result.TotalDays;
        }
        public static double CalcSumOfCheckInCheckOut(DateTime CheckIn, DateTime CheckOut,double price)
        {
            var days = CheckOut.Date.Subtract(CheckIn.Date).TotalDays;
            var result = days * price;
            return result;
        }
    }
}
