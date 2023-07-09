namespace nextbit.Models
{
    public class Notice
    {
        public class Request
        {
            public string Title { get; set; }
            public string Content { get; set; }
            public bool IsImportant { get; set; }
        }

        public class Item
        { 
            public string Title { get; set; }
            public string Description { get; set; }

            public Item() 
            { 
            }

            public Item(string title, string description)
            {
                Title = title;  
                Description = description;
            }
        }
    }
}
