public struct FillingPlates
{
    private readonly int _number;
    private readonly bool _isBomb;
    public int Number => _number;
    public bool IsBomb => _isBomb;
    public FillingPlates(int number,bool isBomb)
    {
        _number = number;
        _isBomb = isBomb;
    }
}
