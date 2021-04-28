namespace AdapterSample
{
    public class Transport : ITransport
    {
        private Bicycle bike = new Bicycle();
        
        public void Commute()
        {
           bike.Pedal();
           bike.Ring();
        }
    }
}