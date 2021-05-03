namespace AdapterSample
{
    public class Transport : ITransport
    {
        private Bicycle _bike => new Bicycle();
        public void Commute()
        {
           _bike.Pedal();
        }
    }
}