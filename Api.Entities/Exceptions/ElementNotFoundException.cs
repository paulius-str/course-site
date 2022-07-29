namespace Api.Entities.Exceptions
{
    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException() : base("Element Not Found")
        {

        }
    }
}
