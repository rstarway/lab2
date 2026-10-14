namespace Model
{
    public class Record : IDomainObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Date { get; set; }
        public string Cause { get; set; }
        public int Cell { get; set; }

        int IDomainObject.ID
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}
