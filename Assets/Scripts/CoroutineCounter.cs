public class CoroutineCounter
{
    private int _count;

    public bool IsZeroCoroutineEnebled => _count == 0;

    public void OneStarted()
    {
        _count++;
    }
    public void OneEnded()
    {
        _count--;
    }   
    
    public void Reset()
    {
        _count = 0;
    }
}
