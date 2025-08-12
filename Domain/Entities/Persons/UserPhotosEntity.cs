using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Persons
{
    public class UserPhotosEntity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public string photoPath { get; private set; }

        public Guid UserId { get; private set; }

        [ForeignKey("UserId")]
        public UserEntity User { get; set; }

        private UserPhotosEntity() { }

        public UserPhotosEntity(Guid userId, string photoPath)
        {
            UserId = userId;
            this.photoPath = photoPath.Trim();
            User = new UserEntity();
        }

    }
}
