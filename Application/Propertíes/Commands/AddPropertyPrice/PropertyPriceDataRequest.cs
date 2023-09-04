namespace Immowert4You.Application.Properties.Commands.AddPropertyPrice
{
    public class PropertyPriceDataRequest
    {
        public PropertyPriceDataRequest(int price)
        {
            Price = price;
        }

        public int Price { get; }
    }
}
