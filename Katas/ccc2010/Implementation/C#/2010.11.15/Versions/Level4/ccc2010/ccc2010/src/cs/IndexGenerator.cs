namespace ccc2010
{
    public abstract class IndexGenerator
    {
        public abstract int StartValue { get; }
        public abstract bool HasNext { get; }
        public abstract int Next();

        public abstract IndexGenerator CreateSwaped();
    }
}
