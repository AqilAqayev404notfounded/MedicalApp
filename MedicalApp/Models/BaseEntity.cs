namespace MedicalApp.Models
{
    public abstract class BaseEntity
    {
        private static int _id;
        public int Id { get; set; }


        public BaseEntity( )
        {
            Id = ++_id;
        }
    }
}
