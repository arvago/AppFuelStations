using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppFuelStations.Triggers
{
    public class PriceTrigger : TriggerAction<Entry>
    {
        protected override void Invoke(Entry sender)
        {
            double n;
            bool isNumeric = double.TryParse(sender.Text, out n);
            if (string.IsNullOrWhiteSpace(sender.Text) || !isNumeric)
            {
                sender.Text = "";
            }
            else
            {
                if (n < 0)
                {
                    sender.Text = "0";
                }
            }
        }
    }
}
