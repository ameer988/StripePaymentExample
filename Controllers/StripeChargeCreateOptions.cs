namespace StripePaymentExample.Controllers
{
    internal class StripeChargeCreateOptions
    {
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string SourceTokenOrExistingSourceId { get; set; }
        public string Description { get; set; }
    }
}