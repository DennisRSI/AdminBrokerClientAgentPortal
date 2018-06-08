namespace Codes.Service.ViewModels
{
    //
    // A generic model that works for all account types
    //
    public class AccountViewModel
    {
        public AccountViewModel(int id, string text)
        {
            Id = id;
            Text = text;
        }

        public int Id { get; set; }
        public string Text { get; set; }
    }
}
