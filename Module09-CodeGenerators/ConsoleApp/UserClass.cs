using PrintableFields;

namespace ConsoleApp
{
    public partial class UserClass
    {
        [Printable]
        private int _field;
        public void UserMethod()
        {            
            Print_field();
        }

        public partial void PrintAllFields();
    }
}
