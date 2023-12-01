namespace Project.Core
{
    public class Counter
    {
        public int Value => _value;
        private int _value = 0;

        public void Increment()
        {
            _value++;
        }

        public void Decrement()
        {
            _value--;
        }
    }
}
