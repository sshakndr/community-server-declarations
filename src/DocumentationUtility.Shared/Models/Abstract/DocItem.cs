namespace DocumentationUtility.Shared.Models.Abstract
{
    public abstract class DocItem
    {
        public string Name { get; protected set; }
        public string Link { get; protected set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
