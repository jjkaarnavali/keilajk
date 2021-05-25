namespace DTO.App
{
    public class LangResources
    {
        public Views Views { get; set; } = new Views();
    }

    public class Views
    {
        public Shared Shared { get; set; } = new Shared();
    }

    public class Shared
    {
        public Layout Layout { get; set; } = new Layout();
    }

    public class Layout
    {
        public string Languages { get; set; } = Resources.Views.Shared._Layout.Languages;
    }
    
}