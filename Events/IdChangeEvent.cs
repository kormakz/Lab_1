namespace Events
{
    public sealed class IdChangeEventArgs : EventArgs
    {
        public int OldId { get; init; }
        public int NewId { get; init; }
        public IdChangeEventArgs(int oldId, int newId)
        {
            OldId = oldId;
            NewId = newId;
        }
    }
}
